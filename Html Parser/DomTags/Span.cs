namespace Html_Parser.DomTags
{
    public class Span:Element
    {
        public Span(string atributes)
        {
            Name = Tag.span;
            Attributes = atributes;
            WithoutSpace = true;
            FindSelectors(atributes);
            
        }
    }
}
