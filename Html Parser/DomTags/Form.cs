namespace Html_Parser.DomTags
{
    public class Form:Element
    {
        public Form(string atributes)
        {
            Name = Tag.form;
            Attributes = atributes;
            FindSelectors(atributes);       
        }
    }
}
