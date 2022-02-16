namespace Html_Parser.DomTags
{
    public class Br:Element
    {
        public Br(string atributes)
        {
            Special = true;
            Name = Tag.br;
            Attributes = atributes;
            FindSelectors(atributes);
        }
    }

}
