
namespace Html_Parser.DomTags
{
    public class Li:Element
    {
        public Li(string atributes)
        {
            Name = Tag.li;
            Attributes = atributes;
            WithoutSpace = true;
            FindSelectors(atributes);
           
        }
    }
}
