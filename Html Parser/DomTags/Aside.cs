namespace Html_Parser.DomTags
{
    public class Aside:Element
    {
        public Aside(string atributes)
        {
            Name = Tag.aside;
            Attributes = atributes;      
            FindSelectors(atributes);
           
        }
    }
}
