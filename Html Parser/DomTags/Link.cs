namespace Html_Parser.DomTags
{
    public class Link:Element
    {

        public Link(string atributes)
        {
            Closed = true;
            Name = Tag.link;
            Attributes = atributes;
            FindSelectors(atributes);
           
        }

    }
}

