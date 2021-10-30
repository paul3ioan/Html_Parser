using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public class TextFormat :ITextFormat
    {
        private string text;
        private int position = 0;
        private readonly List<char> spaces = new List<char>()
        {
            ' ', '\n','\t'
        };
        private bool DOCTYPE = false;
        public void MoveOnce()
        {
            position++;
        }
        public void MoveSubstring(int x)
        {
            position += x + 1;
        }
        public int FindNextOpeningTag()
        {
            return text.IndexOf("<", position + 1);
        }
        public int FindNextCloseTag()
        {
            return text.IndexOf(">", position + 1);
        }
        public bool IsDOCTYPE()
        {
            return DOCTYPE;
        }

        public string GetContext(int end)
        {
            return text.Substring(position,  end - position + 1).Trim();
        }
        public string GetContextToEnd()
        {
            return text.Substring(position, text.Length - position).Trim();
        }
        public char GetElementAt()
        {
            return text[position];
        }
        public void StartParser(string input)
        {
            position = 0;
            text = input;
            checkForDOCTYPE();
        }
        public bool IsEnd()
        {
            return position >= text.Length;
        }
        private bool IsSpace()
        {
            return spaces.Contains(GetElementAt());
        }
        public void SkipSpaces()
        {
            while(!IsEnd() && IsSpace())
            {
                MoveOnce();
            }
        }
        public int FindNextApostrophe()
        {
            return text.IndexOf('"', position + 1);
        }
        public int FindNextSpace()
        {
            return text.IndexOf(' ', position + 1);
        }
        public void StartEncodingSelectors(string s)
        {
            text = s;
            position = 0;
        }
        public void checkForDOCTYPE()
        {
            DOCTYPE = text.Contains("<!DOCTYPE html>");
        }
    }
}
