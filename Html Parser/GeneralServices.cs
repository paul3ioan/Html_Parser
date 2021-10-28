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
        public GeneralServices()
        {
            tags =  Enum.GetValues(typeof(Tag)).Cast<Tag>().ToList();
            notClosedTags = Enum.GetValues(typeof(NotClosedTag)).Cast<NotClosedTag>().ToList();
        }
       public void Parser(string input)
        {
            Stack<Element> DomEls = new Stack<Element> ();
            for(int i = 0;i < input.Length; i ++)
            {
                if(input[i] == '<')
                {
                    int final = input.IndexOf(">", i + 1);
                    if(i == 0)
                    {
                        if (input.Substring(i, final - i + 1) == "<!DOCTYPE html>")
                            docType = true;
                        i = final;
                        continue;
                    }
                       if (final != -1 )
                    {
                        string tag = input.Substring(i, final - i + 1);
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
                        if (nameT[0] !='/')
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
                            foreach(var closedTags in notClosedTags)
                            {
                                if (closedTags.ToString() == nameT)
                                    newEl.Closed = true;
                            }
                            newEl.Atributes = tag;
                            if (DomEls.Count() != 0)
                            {
                                if (root == null) root = newEl;
                                DomEls.Peek().Children.Add(newEl);
                            }
                            if (!newEl.Closed) DomEls.Push(newEl);
                            elements.Add(newEl);
                           
                        }
                            else
                            {
                                DomEls.Pop();
                            }
                        
                        i = final;
                     }
                    //System.Console.WriteLine(final);
                }
            }
            Console.WriteLine(docType);
            int x = 0;
        }
        public void Show()
        {

        }
    }
}
