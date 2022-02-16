namespace Html_Parser.DomTags
{
    public class H:Element
    {
        public H(string atributes)
        {
            Name = Tag.h;
            // in attributes the last item is the number of h :h1,h2,h3
            rawContent += atributes[atributes.Length - 1];
            atributes = atributes.Substring(0, atributes.Length - 1);
            WithoutSpace = true;
            Attributes = atributes;
            FindSelectors(atributes);
    
        }
    }
}
