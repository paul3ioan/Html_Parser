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
        br,
        link
    }
    public class Element
    {
        public Tag Name { get; set; }
        public bool Closed { get; set; }
        public string Atributes { get; set; }
        public List<Element> Children = new List<Element>();
    }
}
