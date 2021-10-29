using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public class DomManipulation
    {
        private Stack<Element> OutputOrder = new Stack<Element>();
        private Stack<Element> DomEls = new Stack<Element>();
        public List<Element> elements = new List<Element>(); // for debug only 
        private Element root = null;
        private List<Tag> tags = Enum.GetValues(typeof(Tag)).Cast<Tag>().ToList(); ///list of possible tags
        private List<NotClosedTag> notClosedTags = Enum.GetValues(typeof(NotClosedTag)).Cast<NotClosedTag>().ToList();
        private List<SpecialTag> specialTags = Enum.GetValues(typeof(SpecialTag)).Cast<SpecialTag>().ToList();
        // tags is a list with normal Tags as html, body, div ...
        // notClosedTags is a list with self-closing tags : link, br ...

        public void StartSerialization()
        {
            if (root == null) Console.WriteLine("No items available");
            OutputOrder.Push(root);
        }
        public bool ElementsToWrite()
        {
            return OutputOrder.Count() != 0;
            ///numele closing, atributes
        }
        public (string, bool, bool, bool, string) ConvertElement()
        {

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
                returnValue.Item5 = el.Attributes;
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
            /*if (OutputOrder.Peek() == el)
            {
                if (!el.Closed)
                   OutputOrder.Pop();
            }*/
            return returnValue;
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
           
            (Tag, bool, bool) elemValues = checkTag(nameTag);
            Element newDomEl = new Element();

            newDomEl.Name = elemValues.Item1;
            newDomEl.Closed = elemValues.Item2;
            newDomEl.Attributes = additionalInfo;
            newDomEl.Special = elemValues.Item3;
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
        private (Tag,bool, bool) checkTag(string nameTag)
        {
            if (nameTag[0] == '/')
                nameTag = nameTag.Substring(1, nameTag.Length - 1);
            bool special = false;
            bool closed = false;
            Tag foundTag = new Tag();
            foreach(var specialTag in specialTags)
            {
                if(specialTag.ToString() == nameTag)
                {
                    special = true;
                }
            }
            foreach(var closedTag in notClosedTags)
            {
                if (closedTag.ToString() == nameTag)
                {
                    closed = true;
                    break;
                }
            }
            foreach (var tag in tags)
            {
                if (tag.ToString() == nameTag)
                {
                    foundTag = tag;
                    break;
                }
            }
            return ( foundTag,closed, special);
        }
        public Element GetRoot()
        {
            return root;
        }
     
    }
}
