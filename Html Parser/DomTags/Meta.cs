
namespace Html_Parser.DomTags
{
    public class Meta : Element
    {
        public Meta(string atributes)
        {
            Name = Tag.meta;
            Attributes = atributes;
            Closed = true;
            FindSelectors(atributes);
          
        }
    }
}
