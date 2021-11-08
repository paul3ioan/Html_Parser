namespace Html_Parser.DomTags
{
    public class Article:Element
    {
        public Article(string atributes)
        {
            Name = Tag.article;
            Attributes = atributes;      
           FindSelectors(atributes);

        }
    }
}
