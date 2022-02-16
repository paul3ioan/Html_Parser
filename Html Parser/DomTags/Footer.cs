namespace Html_Parser.DomTags
{
    public class Footer:Element
    {
        public Footer(string atributes)
        {
            Name = Tag.footer;
            Attributes = atributes;
            FindSelectors(atributes);
           
        }
    }
}
