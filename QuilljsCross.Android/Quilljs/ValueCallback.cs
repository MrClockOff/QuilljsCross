using System;
using Android.Runtime;
using Android.Webkit;

namespace QuilljsCross.Android.Quilljs
{
    public class ValueCallback
        : Java.Lang.Object, IValueCallback
    {
        private readonly Action<string> _callback;

        public ValueCallback(Action<string> callback)
        {
            _callback = callback;
        }

        public void OnReceiveValue(Java.Lang.Object value)
        {
            _callback?.Invoke(value.ToString());
        }

        protected ValueCallback(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}
