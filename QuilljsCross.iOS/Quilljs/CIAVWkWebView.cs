using System;
using CoreGraphics;
using Foundation;
using UIKit;
using WebKit;

namespace QuilljsCross.iOS.Quilljs
{
    /// <summary>
    /// WkWebView with customisable Input Accessory View
    /// </summary>
    public class CIAVWkWebView : WKWebView
    {
        private UIView _inputAccessoryView;
        private bool _disposed;

        /// <summary>
        /// View constructor
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="configuration"></param>
        public CIAVWkWebView(CGRect frame, WKWebViewConfiguration configuration)
            : base(frame, configuration)
        {
        }

        /// <summary>
        /// View constructor
        /// </summary>
        /// <param name="coder"></param>
        public CIAVWkWebView(NSCoder coder)
            : base(coder)
        {
        }

        public override UIView InputAccessoryView
        {
            get
            {
                if (_inputAccessoryView == null)
                {
                    return base.InputAccessoryView;                       
                }

                return _inputAccessoryView;
            }
        }

        /// <summary>
        /// Set custom input accessory view
        /// </summary>
        public void SetInputAccessoryView(UIView view)
        {
            _inputAccessoryView = view;
            ReloadInputViews();
        }

        /// <summary>
        /// View constructor
        /// </summary>
        /// <param name="handle"></param>
        protected CIAVWkWebView(IntPtr handle)
            : base(handle)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (disposing)
            {
                _inputAccessoryView?.Dispose();
                _inputAccessoryView = null;
            }

            base.Dispose(disposing);
        }
    }
}
