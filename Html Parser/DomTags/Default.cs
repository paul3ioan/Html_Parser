using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser.DomTags
{
    public class Default:Element
    {
        public Default(string name, string attributes)
        {
            Name = Tag.Default;
            rawContent = name;
            Attributes = attributes;
            FindSelectors(Attributes);
        }
    }
}
