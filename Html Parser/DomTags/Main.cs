
namespace Html_Parser.DomTags
{
    public class Main:Element
    {
        public Main(string atributes)
        {
            Name = Tag.main;
            Attributes = atributes;
            FindSelectors(atributes);
           
        }
    }
}
