namespace Html_Parser.DomTags
{
    public class Head:Element
    {
       
        public Head(string atributes)
        {
            Name = Tag.head;
            Attributes = atributes;
            FindSelectors(atributes);
        }
    }
}

