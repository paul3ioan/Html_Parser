namespace Html_Parser.DomTags
{
    public class Header:Element
    {
        public Header(string atributes)
        {
            Name = Tag.header;
            Attributes = atributes;
            FindSelectors(atributes);
        }
    }
}
