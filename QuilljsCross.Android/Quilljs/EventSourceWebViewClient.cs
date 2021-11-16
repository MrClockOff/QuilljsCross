using System;
using Android.Runtime;
using Android.Webkit;

namespace QuilljsCross.Android.Quilljs
{
    public class EventSourceWebViewClient : WebViewClient
    {
        public EventSourceWebViewClient()
        {
        }

        public event EventHandler<WebViewClientPageFinishedEventArgs> PageFinished;

        public override void OnPageFinished(WebView view, string url)
        {
            var args = new WebViewClientPageFinishedEventArgs(url);
            PageFinished?.Invoke(view, args);
        }

        protected EventSourceWebViewClient(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}
