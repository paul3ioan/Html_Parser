namespace Html_Parser.DomTags
{
    public class P:Element
    {
        public P(string atributes)
        {
            Name = Tag.p;
            Attributes = atributes;
            WithoutSpace = true;
            FindSelectors(atributes);
        }
    }
}
