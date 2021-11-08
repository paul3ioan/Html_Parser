namespace Html_Parser.DomTags
{
    public class Ul:Element
    {
        public Ul(string atributes)
        {
            Name = Tag.ul;
            Attributes = atributes;
            FindSelectors(atributes);
        }
    }
}
