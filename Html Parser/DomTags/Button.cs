namespace Html_Parser.DomTags
{
    public class Button:Element
    {

        public Button(string atributes)
       {
            Name = Tag.button;
            Attributes = atributes;
            FindSelectors(atributes);
        }
    
    }
}
