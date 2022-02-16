using Microsoft.VisualStudio.TestTools.UnitTesting;
using Html_Parser;
using System;
using AngleSharp;
using System.IO;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public static string mainString, text;
        [TestMethod]
        public void TestMethod1()
        {
            
            startProgram();
            //System.Console.WriteLine(code);
            IGeneralServices _generalServices = new GeneralServices();
            _generalServices.Parser(mainString);
            string output = _generalServices.Show();
            output = BeautifyText(output);
            Assert.AreEqual(mainString, output);
        }
        static void startProgram()
        {
           
            mainString = System.IO.File.ReadAllText("../../../input.txt");
            mainString = BeautifyText(mainString);
            text = System.IO.File.ReadAllText("../../../input.txt")
                .Replace(Environment.NewLine, "");

            /// transform the input in a single line
   

        }
        public static string BeautifyText(string input)
        {
            var parser = new AngleSharp.Html.Parser.HtmlParser();  
            var document = parser.ParseDocument(input);
            var sw = new StringWriter();
            document.ToHtml(sw, new AngleSharp.Html.PrettyMarkupFormatter());
            return sw.ToString();
        }
    }
}
    

    

