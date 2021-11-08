
namespace Html_Parser.DomTags
{
    public class A:Element
    {
      
        public A(string atributes)
        {
            Name = Tag.a;
            Attributes = atributes;
            WithoutSpace = true;
            FindSelectors(atributes);
 
        }

    }
}

