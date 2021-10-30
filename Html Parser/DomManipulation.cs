using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Html_Parser.DomTags;
namespace Html_Parser
{
    public class DomManipulation
    {
        private Stack<Element> OutputOrder = new Stack<Element>();
        private Stack<Element> DomEls = new Stack<Element>();
        public List<Element> elements = new List<Element>(); // for debug only 
        private Element root = null;
        private List<Tag> tags = Enum.GetValues(typeof(Tag)).Cast<Tag>().ToList(); ///list of possible tags
      //  private List<NotClosedTag> notClosedTags = Enum.GetValues(typeof(NotClosedTag)).Cast<NotClosedTag>().ToList();
        //private List<SpecialTag> specialTags = Enum.GetValues(typeof(SpecialTag)).Cast<SpecialTag>().ToList();
        // tags is a list with normal Tags as html, body, div ...
        // notClosedTags is a list with self-closing tags : link, br ...

        public void StartSerialization()
        {
            if (root == null) Console.WriteLine("No items available");
            OutputOrder.Push(root);
            foreach (var element in elements)
                element.closingMotion = false;
        }
        public bool ElementsToWrite()
        {
            return OutputOrder.Count() != 0;         
        }
        public (string, bool, bool, bool, string) ConvertElement()
        {
            //name closingMotion Closed Special Raw...
            (string, bool, bool, bool, string) returnValue = ("", false, false, false, "");
            Element el = OutputOrder.Peek();
            returnValue.Item1 = el.Name.ToString();


            if (returnValue.Item1 == "text")
            {
                returnValue.Item5 = el.rawContent;
                OutputOrder.Pop();
                return returnValue;
            }
            else
            {
                returnValue.Item2 = el.closingMotion;
                returnValue.Item3 = el.Closed;
                returnValue.Item4 = el.Special;
                returnValue.Item5 = ConvertAtributes(el);
            }
            if (el.Closed || el.Special)
            {
                OutputOrder.Pop();
                return returnValue;
            }
            if(el.closingMotion )
            {
                OutputOrder.Pop();
                return returnValue;
            }
            else
            {
                el.closingMotion = true;
            }
            foreach (var child in el.Children.Reverse<Element>())
            {
                OutputOrder.Push(child);
            }
            return returnValue;
        }
        private string ConvertAtributes(Element element)
        {
            char apos = '"';
            string atributes = "";
            if (element.Selectors.Count() == 0)
                return atributes;
            foreach(var selector in element.Selectors)
            {
                string values = "";
                foreach(var elem in selector.Value)
                {
                    values += $" {elem}";
                }
                values = values.Substring(1, values.Length - 1);
                atributes += $" {selector.Key} = {apos}{values}{apos}";
            }
            atributes = atributes.Substring(1, atributes.Length - 1);
            return atributes;
        }
        public void CreateTextElement(string fullContext)
        {
            Element newEl = new Element();
            newEl.Name = Tag.text;
            newEl.rawContent = fullContext.Substring(0, fullContext.Count() - 1);
            newEl.Closed = true;
            AddElementToStack(newEl);

        }
        public void CreateElement(string fullTag)
        {

            string additionalInfo = "";
            var nameTag = fullTag.Split(' ')[0]; // the name of tag

            if (nameTag.Last() == '>')
            {
                nameTag = nameTag.Substring(1, nameTag.Length - 2).Trim();
            }
            else
            {
               nameTag =  nameTag.Substring(1, nameTag.Length - 1).Trim();
             
                additionalInfo = fullTag
                    .Substring(nameTag.Length + 1, fullTag.Length - nameTag.Length -2)
                    .Trim();
            }
            Element newDomEl = WhichTagIsIt(nameTag, additionalInfo);
            if (newDomEl == null) throw new ArgumentNullException();
            //get the Dom element we need as an object
            if (nameTag[0] == '/' && !newDomEl.Special)
            {
                    DomEls.Pop();                
            }//if it's a close tag the "top" element is closed
            else
            { 
                //to be done a check if a elem is correct
                AddElementToStack(newDomEl);
            }
        }
        private void AddElementToStack(Element element)
        {
           
            if(element.Closed || element.Special)
            {
                DomEls.Peek().Children.Add(element);
                elements.Add(element);
                return;
            }
            if(DomEls.Count() != 0 )
            {
                DomEls.Peek().Children.Add(element);
                DomEls.Push(element);
            }
            else
                if(DomEls.Count() == 0 )
            {
                if (root == null) root = element;
                DomEls.Push(element);
            }
            elements.Add(element);
        }
        
    
       /* private T createTag<T>(string info) where T:new()   
        {
            T newDom = new T();
            var selectors = newDom.FindSelectors(info);
            newDom.CreateSelectors(selectors);
            return newDom;
       }
       */ 
        private Element WhichTagIsIt(string name, string info)
        {
            if (name[0] == '/') name = name.Substring(1, name.Length - 1);
            switch(name)
            {
                case "html":
                    
                    return new Html(info);
                     
                 case "div":
                     return new Div(info);
                 case "button":
                     return new Button(info);
                 case "link":
                     return new Link(info);
                 case "br":
                     return new Br(info);
                 case "img":
                     return new Img(info);
                 case "a":
                    return new A(info);
                case "body":
                    return new Body(info);
                case "head":
                    return new Head(info);
                default: return null;
            }
        }
     
    }
}
