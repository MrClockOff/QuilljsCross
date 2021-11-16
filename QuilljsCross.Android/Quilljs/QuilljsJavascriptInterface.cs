using System;
using Android.Runtime;
using Android.Webkit;
using Java.Interop;
using QuilljsCross.Shared.Quilljs;

namespace QuilljsCross.Android.Quilljs
{
    public class QuilljsJavascriptInterface
        : Java.Lang.Object
    {
        public QuilljsJavascriptInterface()
        {
        }

        public event EventHandler<QuilljsSelectionChangedEventArgs> SelectionChanged;

        public event EventHandler<QuilljsTextChangeEventArgs> TextChanged;

        public event EventHandler<QuilljsContentSizeChangedEventArgs> ContentSizeChanged;

        [JavascriptInterface]
        [Export("onSelectionChanged")]
        public virtual void OnSelectionChanged(string text, int index, int lenght, string formattingAttributes)
        {
            var args = new QuilljsSelectionChangedEventArgs(formattingAttributes.Split(","), index, lenght, text);
            SelectionChanged?.Invoke(this, args);
        }

        [JavascriptInterface]
        [Export("onTextChanged")]
        public virtual void OnTextChanged(string html)
        {
            var args = new QuilljsTextChangeEventArgs(html);
            TextChanged?.Invoke(this, args);
        }

        [JavascriptInterface]
        [Export("onContentSizeChanged")]
        public virtual void OnContentSizeChanged(int oldValue, int newValue)
        {
            var args = new QuilljsContentSizeChangedEventArgs(oldValue, newValue);
            ContentSizeChanged?.Invoke(this, args);
        }

        protected QuilljsJavascriptInterface(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }    
    }
}
