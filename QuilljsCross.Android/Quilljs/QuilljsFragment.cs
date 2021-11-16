using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AndroidX.Fragment.App;
using QuilljsCross.Android.Extensions;
using QuilljsCross.Shared.Quilljs;

namespace QuilljsCross.Android.Quilljs
{
    /// <summary>
    /// https://github.com/jkidi/quill-android
    /// </summary>
    public class QuilljsFragment
        : Fragment, IQuilljsEditor
    {
        private const string RunQuilljsUrl = "file:///android_asset/run-quilljs.html";
        private EventSourceWebViewClient _webViewClient;
        private QuilljsJavascriptInterface _javaInterface;
        private WebView _webView;
        private string _html;
        private string _placeholder;
        private bool _quilljsInitialised;
        private bool _disposed;

        public QuilljsFragment()
        {
        }

        #region IQuilljsEditor implementation
        public string Html
        {
            get
            {
                return _html;
            }

            set
            {
                _html = value;
                ExecuteJavascript(_webView, $"setHtml('{value}');");
            }
        }

        public string Placeholder
        {
            get
            {
                return _placeholder;
            }

            set
            {
                _placeholder = value;
                ExecuteJavascript(_webView, $"setPlaceholder('{value}');");
            }
        }

        public event EventHandler<QuilljsSelectionChangedEventArgs> SelectionChanged;

        public void SetFormat(string formattingAttribute, bool apply)
        {
            ExecuteJavascript(_webView, $"setFormat('{formattingAttribute}', {apply.ToString().ToLower()});");
        }

        public void SetList(string formattingAttribute, bool apply)
        {
            ExecuteJavascript(_webView, $"setList('{formattingAttribute}', {apply.ToString().ToLower()});");
        }

        public void SetAlignment(string formattingAttribute)
        {
            ExecuteJavascript(_webView, $"setAlignment('{formattingAttribute}');");
        }
        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.QuilljsFragment, container, false);
            var toolbarContainer = view.FindViewById<FrameLayout>(Resource.Id.ToolbarFrameLayout);

            _webView = view.FindViewById<WebView>(Resource.Id.WebView);
            ConfigureWebView(_webView);
            LoadQuill(_webView);
            LoadQuillToolbar(toolbarContainer);

            return view;
        }

        protected QuilljsFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
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
                if (_webViewClient != null)
                {
                    _webViewClient.PageFinished -= WebViewClient_PageFinished;
                    _webViewClient.Dispose();
                    _webViewClient = null;
                }

                if (_javaInterface != null)
                {
                    _javaInterface.SelectionChanged -= JavaInterface_SelectionChanged;
                    _javaInterface.TextChanged -= JavaInterface_TextChanged;
                    _javaInterface.ContentSizeChanged -= JavaInterface_ContentSizeChanged;
                    _javaInterface.Dispose();
                    _javaInterface = null;
                }

                _webView?.Dispose();
                _webView = null;
            }

            base.Dispose(disposing);
        }

        protected void ExecuteJavascript(WebView webView, string javascript)
        {
            if (_quilljsInitialised)
            {
                View.Post(() =>
                {
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                    {
                        webView.EvaluateJavascript(javascript, new ValueCallback(JavascriptValueCallback));
                        return;
                    }

                    // TODO: add callback logic for version < KITKAT
                    webView.LoadUrl("javascript:" + javascript);
                });

                return;
            }

            View.PostDelayed(() =>
            {
                ExecuteJavascript(webView, javascript);
            },
            (long)TimeSpan.FromMilliseconds(100).TotalMilliseconds);
        }

        private void ConfigureWebView(WebView webView)
        {
            _webViewClient = new EventSourceWebViewClient();
            _webViewClient.PageFinished += WebViewClient_PageFinished;

            _javaInterface = new QuilljsJavascriptInterface();
            _javaInterface.SelectionChanged += JavaInterface_SelectionChanged;
            _javaInterface.TextChanged += JavaInterface_TextChanged;
            _javaInterface.ContentSizeChanged += JavaInterface_ContentSizeChanged;

            webView.VerticalScrollBarEnabled = false;
            webView.HorizontalScrollBarEnabled = false;
            webView.Settings.JavaScriptEnabled = true;
            webView.SetWebChromeClient(new WebChromeClient());
            webView.SetWebViewClient(_webViewClient);
            webView.AddJavascriptInterface(_javaInterface, "JavaScriptInterface");

#if DEBUG
            WebView.SetWebContentsDebuggingEnabled(true);
#endif
        }

        private void JavaInterface_ContentSizeChanged(object sender, QuilljsContentSizeChangedEventArgs e)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                var webViewLayoutParams = _webView.LayoutParameters;
                webViewLayoutParams.Height = (int)Context.ConvertDpToPx(e.NewValue);
                _webView.LayoutParameters = webViewLayoutParams;
            });
        }

        private void JavaInterface_TextChanged(object sender, QuilljsTextChangeEventArgs e)
        {
            _html = e.Html;
        }

        private void JavaInterface_SelectionChanged(object sender, QuilljsSelectionChangedEventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        private void WebViewClient_PageFinished(object sender, WebViewClientPageFinishedEventArgs e)
        {
            _quilljsInitialised = e.Url.Equals(RunQuilljsUrl, StringComparison.InvariantCultureIgnoreCase);
            Placeholder = "Type your text here...";
        }

        private void LoadQuill(WebView webView)
        {
            webView.LoadUrl(RunQuilljsUrl);
        }

        private void LoadQuillToolbar(ViewGroup container)
        {
            var toolbarLayout = QuilljsToolbarBuilder
                .Start(Context)
                .AddBoldTextButton("outline_format_bold_black_24.png")
                .AddItalicTextButton("outline_format_italic_black_24.png")
                .AddUnderlineTextButton("outline_format_underlined_black_24.png")
                .AddAlignLeftButton("outline_format_align_left_black_24.png")
                .AddAlignCenterButton("outline_format_align_center_black_24.png")
                .AddAlignRightButton("outline_format_align_right_black_24.png")
                .AddBulletListButton("outline_format_list_bulleted_black_24.png")
                .AddNumberListButton("outline_format_list_numbered_black_24.png")
                .Create(this);
            var toolbarLayoutParams = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            container.AddView(toolbarLayout, toolbarLayoutParams);
        }

        private void JavascriptValueCallback(string value)
        {
            System.Diagnostics.Debug.WriteLine($"Javascript execution callback value: {value}");
        }
    }
}
