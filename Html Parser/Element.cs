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
       comment,
       body,
       div,
       head,
       br,
       link
    }
    public enum NotClosedTag
    {
        
        link
    }
    public enum SpecialTag
    {
        br
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
    }
}
