using System;
using System.Collections.Generic;
using System.Linq;
using Html_Parser.DomTags;
namespace Html_Parser
{
    public class DomManipulation:IDomManipulation
    {
        private readonly Stack<Element> OutputOrder = new();
        private readonly Stack<Element> DomEls = new ();
        public List<Element> elements = new(); // for debug only 
        private Element root = null;
        //private readonly List<Tag> tags = Enum.GetValues(typeof(Tag)).Cast<Tag>().ToList(); ///list of possible tags

        public void StartSerialization()
        {
            if (root == null) Console.WriteLine("No items available");
            OutputOrder.Push(root);
            foreach (var element in elements)
                element.closingMotion = false;
        }
        public bool ElementsToWrite()
        {
            return OutputOrder.Count != 0;         
        }
        public (string, bool, bool, bool, string, bool) ConvertElement()
        {
            //name closingMotion Closed Special Raw WithoutSpaces...
            (string, bool, bool, bool, string, bool) returnValue = ("", false, false, false, "", false);
            Element el = OutputOrder.Peek();
            if(el.Name == Tag.Default)
                returnValue.Item1 = el.rawContent;
            else
            returnValue.Item1 = el.Name.ToString();
            if(returnValue.Item1 == "h")
            {
                returnValue.Item1 += el.rawContent;
            }

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
                returnValue.Item6 = el.WithoutSpace;
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
            if (element.Selectors.Count == 0)
                return atributes;
            foreach(var selector in element.Selectors)
            {
                string values = "";
                foreach(var elem in selector.Value)
                {
                    values += $" {elem}";
                }
                values = values.Substring(startIndex: 1, values.Length - 1);

                atributes += $" {selector.Key}={apos}{values}{apos}";
            }
            atributes = atributes.Substring(1, atributes.Length - 1);
            return atributes;
        }
        public void CreateTextElement(string fullContext)
        {
            Element newEl = new();
            newEl.Name = Tag.text;
            if (!(fullContext.StartsWith("<!--") && fullContext.EndsWith("-->")))
                newEl.rawContent = fullContext.Substring(0, fullContext.Length - 1).Trim();
            else
                newEl.rawContent = fullContext.Trim();
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
            if(DomEls.Count != 0 )
            {
                DomEls.Peek().Children.Add(element);
                DomEls.Push(element);
            }
            else
                if(DomEls.Count == 0 )
            {
                if (root == null) root = element;
                DomEls.Push(element);
            }
            elements.Add(element);
        }
        
        private Element WhichTagIsIt(string name, string info)
        {
            if (name[0] == '/') name = name.Substring(1, name.Length - 1);
            if (name.Length == 2 && name[0] == 'h')
            {
                info += name[index: name.Length - 1];
                name = name.Substring(0, name.Length - 1);
            }
            
            return name switch
            {
                "html" => new Html(info),
                "div" => new Div(info),
                "button" => new Button(info),
                "link" => new Link(info),
                "br" => new Br(info),
                "img" => new Img(info),
                "a" => new A(info),
                "body" => new Body(info),
                "head" => new Head(info),
                "article" =>new Article(info),
                "aside" => new Aside(info),
                "header" => new Header(info),
                "nav" => new Nav(info),
                "title" => new Title(info),
                "meta" => new Meta(info),
                "ul" => new Ul(info),
                "li" => new Li(info),
                "form" => new Form(info),
                "input" => new Input(info),
                "main" => new Main(info),
                "p" => new P(info),
                "span" => new Span(info),
                "strong" => new Strong(info),
                "footer" => new Footer(info),
                 "h" => new H(info),
                _ => new Default(name, info),
            };
        }
     
    }
}
