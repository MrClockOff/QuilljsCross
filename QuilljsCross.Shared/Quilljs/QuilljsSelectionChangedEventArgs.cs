using System;
using System.Collections.Generic;

namespace QuilljsCross.Shared.Quilljs
{
    public class QuilljsSelectionChangedEventArgs : EventArgs
    {
        public QuilljsSelectionChangedEventArgs(IEnumerable<string> selectionFormattingsAttributes, int startIndex, int lenght, string selection)
        {
            SelectionFormattingAttributes = selectionFormattingsAttributes;
            StartIndex = startIndex;
            Lenght = lenght;
            Selection = selection;
        }

        public IEnumerable<string> SelectionFormattingAttributes { get; }

        public int StartIndex { get; }

        public int Lenght { get; }

        public string Selection { get; }
    }
}
