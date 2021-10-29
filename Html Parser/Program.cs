using System; 
namespace Html_Parser
{
    
    class Program
    {
       
        static void Main(string[] args)
        {
            string code = startProgram();
            //System.Console.WriteLine(code);
            IGeneralServices _generalServices = new GeneralServices();
            _generalServices.parser(code);
            _generalServices.Show();

        }
        static string startProgram()
        {
            string text = System.IO.File.ReadAllText("../../../Input.txt")
                .Replace(Environment.NewLine,"");
            /// transform the input in a single line
            return text;
        }
    }
}
