using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public class OutputFormat
    {
        private int numberTabs = 0;
        string output = "";
        
        public void WriteDOCTYPE(bool isDOC)
        {
            if(isDOC)
                System.Console.WriteLine("<!DOCTYPE html>");
        }
        public void WriteInFile((string,bool,bool,bool,string) info)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (info.Item1 == "")
                return;
            var name = info.Item1;
            if (name == "text")
            {
                string tabs = new String(' ', numberTabs);
                if(!(output.Last() == '>'))
                stringBuilder.Append($"{tabs}");
                stringBuilder.Append(info.Item5);
                stringBuilder.Append('\n');
                output +=(stringBuilder.ToString());
            }
            else
            if(info.Item3)
            {
                
                string tabs = new String(' ', numberTabs);
                numberTabs-=2;
                if (stringBuilder.Length != 0 &&stringBuilder[stringBuilder.Length - 1] == '>')
                    stringBuilder.Append('\n');
                stringBuilder.Append($"{tabs}<{name}>");
                stringBuilder.Append('\n');
                output+=(stringBuilder.ToString());
            }
            else
            if(info.Item4)
            {
                stringBuilder.Append($"</{name}>");
                if (output.Last() == '\n')
                {
                    output = output.Substring(0, output.Length - 1);
                }
                output +=(stringBuilder.ToString());
            }
            else
            if(info.Item2)
            {
                string tabs;
                if (CheckLastTag(name))
                {
                    output = output.Substring(0, output.Length - 1);
                    stringBuilder.Append($"</{name}>");
                }
                else
                {
                    tabs = new String(' ', numberTabs);
                    stringBuilder.Append($"{tabs}</{name}>");
                }
                    stringBuilder.Append('\n');
                output += stringBuilder.ToString();
                numberTabs-=2;
            }
            else
            {
                string tabs = new String(' ', numberTabs);
                
                if (info.Item5 != "")
                    stringBuilder.Append($"{tabs}<{name} {info.Item5}>");
                else
                    stringBuilder.Append($"{tabs}<{name}>");
                if (stringBuilder[stringBuilder.Length - 1] == '>')
                    stringBuilder.Append('\n');

                output += stringBuilder.ToString();
          
                numberTabs+=2;
            }
           
        }
        private bool CheckLastTag(string name)
        {
            name = new string(name.ToCharArray().Reverse().ToArray());
            string work = new string(output.ToCharArray()
                .Reverse().ToArray());            
            var start = work.IndexOf('>');
            var stop = work.IndexOf('<');
            var tag = work.Substring(start + 1, stop - 2);
            return tag == name;
        }
        public void Write()
        {
            Console.Write(output);
        }
    }
}
