using System;

namespace QuilljsCross.Android.Quilljs
{
    public class QuilljsTextChangeEventArgs : EventArgs
    {
        public QuilljsTextChangeEventArgs(string html)
        {
            Html = html;
        }

        public string Html { get; }
    }
}
