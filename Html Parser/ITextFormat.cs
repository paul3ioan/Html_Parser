namespace Html_Parser
{
    public interface ITextFormat
    {
        public void MoveOnce();
        public void MoveSubstring(int length);
        public int FindNextOpeningTag();
        public int FindNextCloseTag();
        public bool IsDOCTYPE();
        public string GetContext(int end);
        public string GetContextToEnd();
        public char GetElementAt();
        public void StartParser(string input);
        public bool IsEnd();
        public void SkipSpaces();
        public int FindNextApostrophe();
        public int FindNextSpace();
        public void StartEncodingSelectors(string s);
        public void CheckForDOCTYPE();












    }
}
