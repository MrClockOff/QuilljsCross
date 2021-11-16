using System;

namespace QuilljsCross.Android.Quilljs
{
    public class WebViewClientPageFinishedEventArgs : EventArgs
    {
        public WebViewClientPageFinishedEventArgs(string url)
        {
            Url = url;
        }

        public string Url { get; }
    }
}
