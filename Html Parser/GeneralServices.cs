using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public class GeneralServices : IGeneralServices
    {
        private bool docType = false;
        public List<Element> elements = new List<Element>();
        private bool inTag = false;
        private Element root = null;
        private List<Tag> tags; ///list of possible tags
        private List<NotClosedTag> notClosedTags;
        private TextFormat _textFormat = new TextFormat();
        private OutputFormat _outputFormat = new OutputFormat();
        private DomManipulation _domManipulation = new DomManipulation();
        public GeneralServices()
        {
            tags = Enum.GetValues(typeof(Tag)).Cast<Tag>().ToList();
            notClosedTags = Enum.GetValues(typeof(NotClosedTag)).Cast<NotClosedTag>().ToList();
        }
        public void Parser(string input)
        {
            _textFormat.StartParser(input);
            Stack<Element> DomEls = new Stack<Element>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '<')
                {
                    int final = input.IndexOf(">", i + 1);
                    if (i == 0)
                    {
                        if (input.Substring(i, final - i + 1) == "<!DOCTYPE html>")
                        {
                            docType = true;
                            i = final;
                            continue;
                        }
                    }
                    if (final != -1)
                    {
                        string tag = input.Substring(i, final - i + 1);
                        // _domManipulation.CreateElement(tag);
                        //continue;
                        var nameTag = tag.Split(' ')[0];

                        //nameTag.Substring(2, nameTag.Length - 4);
                        string nameT;
                        if (nameTag.Last() == '>')
                        {
                            nameT = nameTag.Substring(1, nameTag.Length - 2);
                            tag = "";
                        }
                        else
                        {
                            nameT = nameTag.Substring(1, nameTag.Length - 1);
                            tag = tag.Substring(nameTag.Length, tag.Length - nameTag.Length).TrimStart();
                        }
                        if (nameT[0] != '/')
                        {
                            Element newEl = new Element();
                            foreach (var name in tags)
                            {
                                if (name.ToString() == nameT)
                                {
                                    newEl.Name = name;
                                    break;
                                }
                            }
                            foreach (var closedTags in notClosedTags)
                            {
                                if (closedTags.ToString() == nameT)
                                    newEl.Closed = true;
                            }
                            if (tag != "")
                                newEl.Atributes = tag.Substring(0, tag.Length - 2);
                            else
                                newEl.Atributes = tag;
                            if (DomEls.Count() != 0)
                            {

                                DomEls.Peek().Children.Add(newEl);
                                DomEls.Push(newEl);
                            }

                            if (DomEls.Count() == 0 && !newEl.Closed)
                            {
                                if (root == null) root = newEl;
                                DomEls.Push(newEl);
                            }
                            if (newEl.Closed == true) DomEls.Pop();
                            elements.Add(newEl);

                        }
                        else
                        {
                            //    Console.WriteLine($"closed{DomEls.Peek().Name.ToString()}");
                            DomEls.Pop();
                        }

                        i = final;
                    }
                    //System.Console.WriteLine(final);
                }
            }
            //Console.WriteLine(docType);
            int x = 0;
        }
        public void parser(string input)
        {
            _textFormat.StartParser(input);
            _textFormat.checkForDOCTYPE();
            if(_textFormat.IsDOCTYPE())
            {
                _textFormat.MoveSubstring("<!DOCTYPE html>".Length - 1);
                var ch = _textFormat.GetElementAt();
            }
            while(!_textFormat.IsEnd())
            {
                _textFormat.SkipSpaces();
                var nowChar = _textFormat.GetElementAt();
                if (nowChar == '<')
                {
                    var idx = _textFormat.FindNextCloseTag();
                    var tag = _textFormat.GetContext(idx);
                    _domManipulation.CreateElement(tag);
                    _textFormat.MoveSubstring(tag.Count() - 1);
                }
                else if (nowChar != ' ')
                {
                    // if we iterate over a letter => text between tags
                    // we create a special tag for this kind of text
                    var idx = _textFormat.FindNextOpeningTag();
                    var tag = _textFormat.GetContext(idx);
                    _domManipulation.CreateTextElement(tag);
                    _textFormat.MoveSubstring(tag.Count() - 2);
                }
                else _textFormat.MoveOnce();
            }
            
        }
        public void Show()
        {
            
            _domManipulation.StartSerialization();
            _outputFormat.WriteDOCTYPE(_textFormat.IsDOCTYPE());
            while(_domManipulation.ElementsToWrite())
            {
                var information = _domManipulation.ConvertElement();
                _outputFormat.WriteInFile(information);
            }
            _outputFormat.Write();
          /*  Stack<Element> order = new Stack<Element>();
            order.Push(_domManipulation.GetRoot());
            int numberOfTabs = 0;
            while(order.Count() != 0)
            {
               
                string closeSyntax = "";
                Element top = order.Peek();
                if(top.Name == Tag.text)
                {
                    Console.WriteLine(top.rawContent);
                    order.Pop();
                    continue;
                }
                if (top.closingMotion)
                {
                    CloseTag(top);
                    order.Pop();
                    continue;
                }
                if (top.Closed) closeSyntax = "/>";
                else closeSyntax = ">";
                if(top.Atributes !="")
                Console.WriteLine($"<{top.Name.ToString()} {top.Atributes}{closeSyntax}");
                else
                    Console.WriteLine($"<{top.Name.ToString()}{closeSyntax}");
                top.closingMotion = true;
                ///starting tag
                
                foreach(var child in top.Children.Reverse<Element>())
                {
                    order.Push(child);
                }
                if(order.Peek() == top)
                {
                    if(!top.Closed)
                         CloseTag(top);
                    order.Pop();
                }

            }*/

        }
        private void CloseTag(Element element)
        {
            Console.WriteLine($"</{element.Name.ToString()}>");
        }
    }
}
