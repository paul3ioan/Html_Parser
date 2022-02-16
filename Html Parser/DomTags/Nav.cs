namespace Html_Parser.DomTags
{
    public class Nav:Element
    {
        public Nav(string atributes)
        {
            Name = Tag.nav;
            Attributes = atributes;
            FindSelectors(atributes);
        }
    }
}
