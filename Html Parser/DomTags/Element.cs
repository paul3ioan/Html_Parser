using System;
using System.Collections.Generic;
using System.Linq;
namespace Html_Parser
{
    public enum Tag
    {
       Default,
       html,
       text,
       p,
       h,
       a,
       img,
       comment,
       body,
       main,
       span,
       strong,
       div,
       ul,
       li,
       head,
       br,
       link,
       button, 
       input,
       form,
       nav, 
       article,
       aside,
       footer,
       meta,
       title,
       header
    } //name of tags


    public class Element
    {   
        public Tag Name { get; set; }
        public bool Closed { get; set; }
        public string rawContent = "";
        public bool WithoutSpace = false;
        public string Attributes { get; set; }
        public List<Element> Children = new ();
        public bool closingMotion = false;
        public bool Special = false;
        public Dictionary<string, List<string>> Selectors = new ();
        public void FindSelectors(string s)
        {  
            if (s == "")
                return ;
            ITextFormat textFormat = new TextFormat();
            textFormat.StartEncodingSelectors(s);
            while(!textFormat.IsEnd())
            {
                textFormat.SkipSpaces();  
                int finalPosSelector = textFormat.FindNextApostrophe();
                if (finalPosSelector == -1) break;
                string Selector = textFormat.GetContext(finalPosSelector); // Selector type class = "
                textFormat.MoveSubstring(Selector.Length - 1);
                Selector = Selector.Substring(0, Selector.Length - 2);
                // now finalPosSelector is the beggining of Selector's characteristics
                // but first we need to move in the textformat over the seletor
                
                int finalPosCharact = textFormat.FindNextApostrophe();
                string characteristics = textFormat.GetContext(finalPosCharact);
                textFormat.MoveSubstring(characteristics.Length - 1);
                characteristics = characteristics.Substring(0, characteristics.Length - 1);
                List<string> chract = SubstractCharact(characteristics);
                Selectors.Add(Selector, chract);
            
            }
            
        }
        private List<string> SubstractCharact(string s)
        {
            // this function takes the string of attributes and put them on a list
            // a split was easier 
            List<string> charact = new();
            ITextFormat textFormat = new TextFormat();
            textFormat.StartEncodingSelectors(s);
            
            while(!textFormat.IsEnd())
            {
                textFormat.SkipSpaces(); 
                int idx = textFormat.FindNextSpace();
                if(idx == -1)
                {
                    string character = textFormat.GetContextToEnd();
                    charact.Add(character);
                    textFormat.MoveSubstring(character.Length - 1);
                    continue;
                }
                string characterr = textFormat.GetContext(idx);
                characterr = characterr.Substring(0, characterr.Length);
                charact.Add(characterr);
                textFormat.MoveSubstring(characterr.Length);
            }
            return charact;
        }
    }
}
