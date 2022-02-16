

namespace Html_Parser.DomTags
{
    public class Body:Element
    {
    
        public Body(string atributes)
        {
            Name = Tag.body;
            Attributes = atributes;
            FindSelectors(atributes);
            
        }

  
    }
}

