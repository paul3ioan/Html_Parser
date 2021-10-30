using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public enum Tag
    {
       html,
       text,
       p,
       h,
       a,
       img,
       comment,
       body,
       div,
       head,
       br,
       link,
       button, 
       input
    }

    public enum BasicSelectors
    {
       Class,
       id
    }
    public class Element
    {
        public Tag Name { get; set; }
        public bool Closed { get; set; }
        public string rawContent = "";
        public string Attributes { get; set; }
        public List<Element> Children = new List<Element>();
        public bool closingMotion = false;
        public bool Special = false;
        public List<string> personalSelectors = new List<string>();
        public Dictionary<string, List<string>> Selectors = new Dictionary<string, List<string>>();
        public virtual void CreateSelectors(Dictionary<string, List<string>> select) { }
        public Dictionary<string, List<string>> FindSelectors(string s)
        {
            Dictionary<string, List<string>> aux = new Dictionary<string, List<string>>();
            if (s == "")
                return null;
            ITextFormat textFormat = new TextFormat();
            textFormat.StartEncodingSelectors(s);
            while(!textFormat.IsEnd())
            {
                textFormat.SkipSpaces();
                char currentChar = textFormat.GetElementAt(); // get the first char of a selector
                int finalPosSelector = textFormat.FindNextApostrophe();
                if (finalPosSelector == -1) break;
                string Selector = textFormat.GetContext(finalPosSelector); // Selector type class = "
                textFormat.MoveSubstring(Selector.Length - 2);
                Selector = Selector.Substring(0, Selector.Length - 4);
                // now finalPosSelector is the beggining of Selector's characteristics
                // but first we need to move in the textformat over the seletor
                
                int finalPosCharact = textFormat.FindNextApostrophe();
                string characteristics = textFormat.GetContext(finalPosCharact);
                textFormat.MoveSubstring(characteristics.Length - 1);
                characteristics = characteristics.Substring(1, characteristics.Length - 2);
                List<string> chract = SubstractCharact(characteristics);
                if (SelectorIsBasic(Selector)) 
                    Selectors.Add(Selector, chract);
                else
                    aux.Add(Selector, chract);
            }
            return aux;
        }
        protected bool SelectorIsPersonal(string selector)
        {
            foreach (var personalSelector in personalSelectors)
            {
                if (personalSelector == selector)
                {
                    return true;

                }
            }
            return false;
        }
        private bool SelectorIsBasic(string selector)
        {
            foreach (var basicSelector in Enum.GetValues(typeof(BasicSelectors)).Cast<BasicSelectors>().ToList())
            {
                if (basicSelector == BasicSelectors.Class)
                {
                    if (selector == "class")
                    {
                        return true;
                    }

                }
                if (basicSelector.ToString() == selector)
                {
                    return true;
                }
            }
            return false;
        }
        private List<string> SubstractCharact(string s)
        {
            List<string> charact = new List<string>();
            ITextFormat textFormat = new TextFormat();
            textFormat.StartEncodingSelectors(s);
            
            while(!textFormat.IsEnd())
            {
                textFormat.SkipSpaces();
                char current = textFormat.GetElementAt();
                int idx = textFormat.FindNextSpace();
                if(idx == -1)
                {
                    string character = textFormat.GetContextToEnd();
                    charact.Add(character);
                    textFormat.MoveSubstring(character.Length - 1);
                    continue;
                }
                string characterr = textFormat.GetContext(idx);
                characterr = characterr.Substring(0, characterr.Length - 1);
                charact.Add(characterr);
                textFormat.MoveSubstring(characterr.Length);
            }
            return charact;
        }
    }
}
