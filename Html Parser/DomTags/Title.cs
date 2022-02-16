namespace Html_Parser.DomTags
{
    public class Title:Element
    {
        public Title(string atributes)
        {
            Name = Tag.title;
            Attributes = atributes;
            WithoutSpace = true;
            FindSelectors(atributes);
        }
    }
}
