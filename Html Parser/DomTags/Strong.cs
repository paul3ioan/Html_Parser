namespace Html_Parser.DomTags
{
    public class Strong:Element
    {
       public Strong(string atributes)
        {
            Name = Tag.strong;
            Attributes = atributes;
            WithoutSpace = true;
            FindSelectors(atributes);
        }
    }
}
