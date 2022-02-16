using System; 
namespace Html_Parser
{
    
    class Program
    {
        public static string mainString;
        static void Main(string[] args)
        {
            string code = StartProgram();
          
            IGeneralServices _generalServices = new GeneralServices();
            _generalServices.Parser(code);
            _generalServices.Show();

        }
        static string StartProgram()
        {
            mainString = System.IO.File.ReadAllText("../../../Input.txt");
            string text = System.IO.File.ReadAllText("../../../Input.txt")
                .Replace(Environment.NewLine,"");
            return text;

        }
    }
}
