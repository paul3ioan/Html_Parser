namespace Html_Parser.DomTags
{
    public class Input:Element
    {
        public Input(string atributes)
        {
            Name = Tag.input;
            Attributes = atributes;
            Closed = true;
            FindSelectors(atributes);        
        }
    }
}
