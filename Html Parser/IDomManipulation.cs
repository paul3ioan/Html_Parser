using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Html_Parser
{
    public interface IDomManipulation
    {
        public void StartSerialization();
        public bool ElementsToWrite();
        public void CreateTextElement(string fullContext);
        public void CreateElement(string fullTag);
        public (string, bool, bool, bool, string, bool) ConvertElement();
    }
}
