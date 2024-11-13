using System;
using System.Collections.Generic;
using Core.Main;

namespace SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public TypeLanguage Language;
        public List<string> Operations;
    }
}