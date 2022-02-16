namespace Html_Parser.DomTags
{
    public class Div:Element
    {
       
        public Div(string atributes)
        {
            Name = Tag.div;
            Attributes = atributes;
            FindSelectors(atributes);
        }
    }
}

