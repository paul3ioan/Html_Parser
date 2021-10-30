using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public class GeneralServices : IGeneralServices
    {
        
        private ITextFormat _textFormat = new TextFormat();
        private OutputFormat _outputFormat = new OutputFormat();
        private DomManipulation _domManipulation = new DomManipulation();
        
        public void parser(string input)
        {
            _textFormat.StartParser(input);
            _textFormat.checkForDOCTYPE();
            if(_textFormat.IsDOCTYPE())
            {
                _textFormat.MoveSubstring("<!DOCTYPE html>".Length - 1);
                var ch = _textFormat.GetElementAt();
            }
            while(!_textFormat.IsEnd())
            {
                _textFormat.SkipSpaces();
                var nowChar = _textFormat.GetElementAt();
                if (nowChar == '<')
                {
                    var idx = _textFormat.FindNextCloseTag();
                    var tag = _textFormat.GetContext(idx);
                    _domManipulation.CreateElement(tag);
                    _textFormat.MoveSubstring(tag.Count() - 1);
                }
                else if (nowChar != ' ')
                {
                    // if we iterate over a letter => text between tags
                    // we create a special tag for this kind of text
                    var idx = _textFormat.FindNextOpeningTag();
                    var tag = _textFormat.GetContext(idx);
                    _domManipulation.CreateTextElement(tag);
                    _textFormat.MoveSubstring(tag.Count() - 2);
                }
                else _textFormat.MoveOnce();
            }
            
        }
        public void Show()
        {

            _domManipulation.StartSerialization();
            _outputFormat.WriteDOCTYPE(_textFormat.IsDOCTYPE());
            while (_domManipulation.ElementsToWrite())
            {
                var information = _domManipulation.ConvertElement();
                _outputFormat.WriteInFile(information);
            }
            _outputFormat.Write();
        }
    }
}
