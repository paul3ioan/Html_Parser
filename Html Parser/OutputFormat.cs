using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public class OutputFormat
    {
        
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
        public void WriteInFile((string, bool, bool, bool, string, bool) info)
        {
            string NameTag = info.Item1;
            bool IsClosing = info.Item2;
            bool ClosedTag = info.Item3;
            //bool IsSpecial = info.Item4;
            string Attributes = info.Item5;
            //bool WithoutSpaces = info.Item6;
            if (NameTag == "text")
                AddText(Attributes);
            else if(ClosedTag)
            {
                if (Attributes == "")
                {
                    output += $"<{NameTag}>";
                }
                else
                    output += $"<{NameTag} {Attributes}>";
            }
            else
            if(!IsClosing)
            {           
                StringBuilder str = new();
                if (Attributes != "")
                    str.Append($"<{NameTag} {Attributes}>");
                else
                    str.Append($"<{NameTag}>");
                output += str;
            }
            else
            {
                output += $"</{NameTag}>";
            }

        }
        private void AddText(string content)
        {
            output += content;
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
