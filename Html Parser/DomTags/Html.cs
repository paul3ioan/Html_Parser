namespace Html_Parser.DomTags
{

    public class Html:Element
    {
        
        public Html(string atributes)
        {
            Name = Tag.html;
            Attributes = atributes;
            FindSelectors(atributes);
            
        }
       
    }
}
