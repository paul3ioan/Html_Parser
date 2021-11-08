namespace Html_Parser.DomTags
{
    public class Img : Element 
    {
       
        public Img(string atributes)
        {
            Name = Tag.img;
            Attributes = atributes;
            FindSelectors(atributes);
        }

        
    }
}

