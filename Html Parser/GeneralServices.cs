using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public class GeneralServices : IGeneralServices
    {
        private readonly ITextFormat _textFormat = new TextFormat();
        private readonly OutputFormat _outputFormat = new OutputFormat();
        private readonly IDomManipulation _domManipulation = new DomManipulation() ;
        
        public void Parser(string input)
        {
            _textFormat.StartParser(input);
            _textFormat.CheckForDOCTYPE();
            if(_textFormat.IsDOCTYPE())
            {
                _textFormat.MoveSubstring("<!DOCTYPE html>".Length - 1);
               
            }
            while(!_textFormat.IsEnd())
            {
                _textFormat.SkipSpaces();
                var nowChar = _textFormat.GetElementAt();
                if (nowChar == '<')
                {
                    var idx = _textFormat.FindNextCloseTag();
                    var tag = _textFormat.GetContext(idx);
                    if(tag[1] == '!')
                    {
                        //if it's a comment
                        _domManipulation.CreateTextElement(tag);
                    }
                    else
                        _domManipulation.CreateElement(tag);
                    _textFormat.MoveSubstring(tag.Length - 1);
                }
                else if (nowChar != ' ')
                {
                    // if we iterate over a letter => text between tags
                    // we create a special tag for this kind of text
                    var idx = _textFormat.FindNextOpeningTag();
                    var tag = _textFormat.GetContext(idx);
                    _domManipulation.CreateTextElement(tag);
                    _textFormat.MoveSubstring(tag.Length - 2);
                }
                else _textFormat.MoveOnce();
            }
            
        }
        public string Show()
        {

            _domManipulation.StartSerialization();
            _outputFormat.WriteDOCTYPE(_textFormat.IsDOCTYPE());
            while (_domManipulation.ElementsToWrite())
            {
                var information = _domManipulation.ConvertElement();
                _outputFormat.WriteInFile(information);
            }
            _outputFormat.TrimEnd();
            _outputFormat.Write();
            return _outputFormat.ShowOutput();
        }
    }
}
