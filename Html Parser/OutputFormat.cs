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
        private string output = "";
        public string ShowOutput()
        {
            return output;
        }
        public void TrimEnd()
        { 
            while(output.Last() == '\n' || output.Last()=='\r')
            {
                output = output.Substring(0, output.Length - 1);
            }
        }
        public void WriteDOCTYPE(bool isDOC)
        {
            if(isDOC)
                output +="<!DOCTYPE html>\r\n";
        }
        public void WriteInFile((string,bool,bool,bool,string, bool) info)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (info.Item1 == "")
                return;
            var name = info.Item1;
          
            if (name == "text")
            {
                // if it s an text element
                string tabs = new String(' ', numberTabs);
                if(!(output.Last() == '>'))
                stringBuilder.Append($"{tabs}");
                stringBuilder.Append(info.Item5);
                //stringBuilder.Append('\r');
                //stringBuilder.Append('\n');
                output +=(stringBuilder.ToString());
            }
            else
            if(info.Item6)
            {
                if(info.Item2)
                {
                    if (CheckIfLastTagIsClosedOrEqual(name))
                    {
                        output += $"</{name}>";
                    }
                    else output += $"</{name}>";
                    numberTabs -= 3;
                }
                else
                {
                    AddStartingTag(info);
                }
            }  
            else 
            if(info.Item3)
            {
                
                string tabs = new String(' ', numberTabs);
               
               // if (stringBuilder.Length != 0 &&stringBuilder[stringBuilder.Length - 1] == '>')
                 //   stringBuilder.Append('\r','\n');
                if(info.Item5 !="")
                    stringBuilder.Append($"{tabs}<{name} {info.Item5}>");
                else
                    stringBuilder.Append($"{tabs}<{name}>");
               // stringBuilder.Append('\r');
              //  stringBuilder.Append('\n');
                output +=(stringBuilder.ToString());
            }
            else
            if(info.Item4)
            {
                stringBuilder.Append($"</{name}>");
                if (output.Last() == '\n')
                {
                    output = output.Substring(0, output.Length - 2);
                }
                output +=(stringBuilder.ToString());
            }
            else
            if(info.Item2)
            {
                string tabs;
                numberTabs -= 3;
                if (CheckLastTag(name))
                {
                    output = output.Substring(0, output.Length - 2);
                    stringBuilder.Append($"</{name}>");
                }
                else
                {
                    tabs = new String(' ', numberTabs);
                    stringBuilder.Append($"{tabs}</{name}>");
                }
               // stringBuilder.Append('\r');
               // stringBuilder.Append('\n');
                output += stringBuilder.ToString();
                
            }
            else      
            {
                AddStartingTag(info);
            }
           
        }
        private void AddStartingTag((string, bool, bool, bool, string, bool) info)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var name = info.Item1;
            string tabs = new String(' ', numberTabs);

            stringBuilder.Append($"{tabs}");
            if (info.Item5 != "")
                stringBuilder.Append($"<{name} {info.Item5}>");
            else
                stringBuilder.Append($"<{name}>");

            if (stringBuilder[stringBuilder.Length - 1] == '>')
            {
               // stringBuilder.Append('\r');
            //stringBuilder.Append('\n');
            }

            output += stringBuilder.ToString();

            numberTabs += 3;
        }
        private bool CheckIfLastTagIsClosedOrEqual(string name)
        {
            string work = new string(output.ToCharArray()
                .Reverse().ToArray());
            var start = work.IndexOf('>');
            var stop = work.IndexOf('<');
            stop -= 3;
            var tag = work.Substring(start + 1, stop);
            bool flag =  tag.Contains('/');
            if (flag) return true;
            else
            {
                if (tag == name)
                    return true;
            }
            return false;
        }
        private bool CheckLastTag(string name)
        {
            name = new string(name.ToCharArray().Reverse().ToArray());
           
            if (name.Contains('\n')) return false;
            string work = new string(output.ToCharArray()
                .Reverse().ToArray());            
            var start = work.IndexOf('>');
            var stop = work.IndexOf('<');
            stop-=3;
            var tag = work.Substring(start + 1, stop);
            
            return tag == name;
        }
        public void Write()
        {
            System.IO.File.WriteAllText("../../../Output.txt", output);
            Console.Write(output);
        }
    }
}
