using System;

namespace QuilljsCross.Android.Quilljs
{
    public class QuilljsContentSizeChangedEventArgs : EventArgs
    {
        public QuilljsContentSizeChangedEventArgs(int oldValue, int newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public int OldValue { get; }

        public int NewValue { get; }
    }
}
