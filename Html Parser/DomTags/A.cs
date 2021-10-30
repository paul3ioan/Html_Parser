using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser.DomTags
{
    public class A:Element
    {
        private List<string> personalTags = new List<string>
            {
                "href"
            };
        public A(string atributes)
        {
            Name = Tag.a;
            Attributes = atributes;
            personalSelectors = personalTags;
            var selectors = FindSelectors(atributes);
            CreateSelectors(selectors);
        }

        public override void CreateSelectors(Dictionary<string, List<string>> selectors)
        {
            if (selectors == null)
                return;
            foreach (var selector in selectors)
            {
                if (SelectorIsPersonal(selector.Key))
                    Selectors.Add(selector.Key, selector.Value);
            }
        }
    }
}

