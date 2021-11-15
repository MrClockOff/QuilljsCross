using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CoreGraphics;
using Foundation;
using QuilljsCross.Shared.Quilljs;
using UIKit;
using WebKit;

namespace QuilljsCross.iOS.Quilljs
{
    // Example taken from https://github.com/saggarwal92/iOSQuillEditor
    public class QuilljsViewController
        : UIViewController, IWKNavigationDelegate, IWKUIDelegate,
          IWKScriptMessageHandler, IQuilljsEditor
    {
        private const string OnTextSelectedInRangeMessage = "onTextSelectedInRangeMessage";
        private const string OnContentResizedMessage = "onContentResizedMessage";
        private const string OnTextChangedMessage = "onTextChangedMessage";
        private CIAVWkWebView _webView;
        private NSLayoutConstraint _webViewHeightConstraint;
        private string _placeholder;
        private string _html;
        private bool _disposed;

        public QuilljsViewController()
        {
        }

        public event EventHandler<QuilljsSelectionChangedArgs> SelectionChanged;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _webView = AddWebView();
            AddQuillToolbar();
            LoadQuill();

            View.BackgroundColor = UIColor.White;
        }

        #region IWKNavigationDelegate implementation
        [Export("webView:didFinishNavigation:")]
        public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            Placeholder = "Type your message here..";
        }
        #endregion

        #region IWKScriptMessageHandler implementation
        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            if (message.Name == OnTextSelectedInRangeMessage)
            {
                OnTextSelectedInRangeMessage_Handler(message);
                return;
            }

            if (message.Name == OnContentResizedMessage)
            {
                OnContentResizedMessage_Handler(message);
                return;
            }

            if (message.Name == OnTextChangedMessage)
            {
                OnTextChangedMessage_Handler(message);
                return;
            }
        }
        #endregion

        #region WKUIDelegate impelemtation
        [Export("webView:runJavaScriptAlertPanelWithMessage:initiatedByFrame:completionHandler:")]
        public void RunJavaScriptAlertPanel(WKWebView webView, string message, WKFrameInfo frame, Action completionHandler)
        {
            var alertController = UIAlertController.Create(message, null, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) => completionHandler()));
            PresentViewController(alertController, true, null);
        }
        #endregion

        #region IQuillEditor implementation
        public string Html
        {
            get
            {
                return _html;
            }

            set
            {
                _html = value;
                _webView.EvaluateJavaScript($"setHtml('{value}');", WKJavascriptEvaluation_Handler);
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
                _webView.EvaluateJavaScript($"setPlaceholder('{value}');", WKJavascriptEvaluation_Handler);
            }
        }

        public void SetAlignment(string formattingAttribute)
        {
            _webView.EvaluateJavaScript($"setAlignment('{formattingAttribute}');", WKJavascriptEvaluation_Handler);
        }

        public void SetFormat(string formattingAttribute, bool apply)
        {
            _webView.EvaluateJavaScript($"setFormat('{formattingAttribute}', {apply.ToString().ToLower()});", WKJavascriptEvaluation_Handler);
        }

        public void SetList(string formattingAttribute, bool apply)
        {
            _webView.EvaluateJavaScript($"setList('{formattingAttribute}', {apply.ToString().ToLower()});", WKJavascriptEvaluation_Handler);
        }
        #endregion

        protected QuilljsViewController(IntPtr handle)
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
                _webView?.Dispose();
                _webView = null;
                _webViewHeightConstraint?.Dispose();
                _webViewHeightConstraint = null;
            }
        }

        private CIAVWkWebView AddWebView()
        {
            var webViewConfig = new WKWebViewConfiguration();
            var webView = new CIAVWkWebView(CGRect.Empty, webViewConfig)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                UIDelegate = this,
                NavigationDelegate = this
            };
            webView.ScrollView.ScrollEnabled = false;
            webView.Layer.MasksToBounds = true;
            webView.Layer.BorderWidth = 1.0f;
            webView.Layer.BorderColor = UIColor.Black.CGColor;

            View.Add(webView);

            var bottomConstraint = webView.BottomAnchor.ConstraintGreaterThanOrEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, 20.0f);
            bottomConstraint.Priority = 750.0f;
            var heightConstraint = _webViewHeightConstraint = webView.HeightAnchor.ConstraintEqualTo(0.0f);
            heightConstraint.Priority = 1000.0f;

            var webViewConstraints = new List<NSLayoutConstraint>
            {
                bottomConstraint,
                heightConstraint,
                webView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, 20.0f),
                webView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor, 20.0f),
                View.SafeAreaLayoutGuide.TrailingAnchor.ConstraintEqualTo(webView.TrailingAnchor, 20.0f)
            };

            NSLayoutConstraint.ActivateConstraints(webViewConstraints.ToArray());

            return webView;
        }

        private void AddQuillToolbar()
        {
            var toolbar = QuilljsToolbarBuilder
                .Start()
                .AddBoldTextButton()
                .AddItalicTextButton()
                .AddUnderlineTextButton()
                .AddAlignLeftButton()
                .AddAlignCenterButton()
                .AddAlignRightButton()
                .AddBulletListButton()
                .AddNumberListButton()
                .Create(this);
            _webView.SetInputAccessoryView(toolbar);
        }

        private void LoadQuill()
        {
            var quillJsUrl = NSBundle.MainBundle.GetUrlForResource("quilljs", "js"); ;
            var quillJsData = NSData.FromUrl(quillJsUrl);
            var quillJs = NSString.FromData(quillJsData, NSStringEncoding.UTF8);
            var quillJsScript = new WKUserScript(quillJs, WKUserScriptInjectionTime.AtDocumentEnd, false);            

            var runQuillJsUrl = NSBundle.MainBundle.GetUrlForResource("run-quilljs", "js");
            var runQuillJsData = NSData.FromUrl(runQuillJsUrl);
            var runQuillJs = NSString.FromData(runQuillJsData, NSStringEncoding.UTF8);
            var runQuillJsScript = new WKUserScript(runQuillJs, WKUserScriptInjectionTime.AtDocumentEnd, false);            

            var editorUrl = NSBundle.MainBundle.GetUrlForResource("quilljs-snow", "html");
            var userContentController = new WKUserContentController();

            _webView.Configuration.UserContentController = userContentController;
            _webView.Configuration.UserContentController.AddUserScript(quillJsScript);
            _webView.Configuration.UserContentController.AddUserScript(runQuillJsScript);
            _webView.Configuration.UserContentController.AddScriptMessageHandler(this, OnTextSelectedInRangeMessage);
            _webView.Configuration.UserContentController.AddScriptMessageHandler(this, OnContentResizedMessage);
            _webView.Configuration.UserContentController.AddScriptMessageHandler(this, OnTextChangedMessage);
            _webView.LoadFileUrl(editorUrl, editorUrl);
        }

        private void WKJavascriptEvaluation_Handler(NSObject result, NSError error)
        {
            if (error != null)
            {
                Debug.WriteLine(error.DebugDescription);
            }
        }

        private void OnTextSelectedInRangeMessage_Handler(WKScriptMessage message)
        {
            if (!(message.Body is NSDictionary body))
            {
                Debug.WriteLine($"OnTextSelectedInRangeMessage: {message.Body}");
                return;
            }

            var messageArgs = body.ToDictionary(pair => pair.Key.ToString(), pair => pair.Value.ToString());
            messageArgs.TryGetValue("text", out string text);
            messageArgs.TryGetValue("index", out string index);
            messageArgs.TryGetValue("length", out string length);
            messageArgs.TryGetValue("formattingAttributes", out string formattingAttributes);
                        
            var selectionChangedArgs = new QuilljsSelectionChangedArgs(formattingAttributes.Split(","), int.Parse(index), int.Parse(length), text);
            SelectionChanged?.Invoke(this, selectionChangedArgs);
        }

        private void OnContentResizedMessage_Handler(WKScriptMessage message)
        {
            if (!(message.Body is NSDictionary body))
            {
                Debug.WriteLine($"OnContentResizedMessage: {message.Body}");
                return;
            }

            var messageArgs = body.ToDictionary(pair => pair.Key.ToString(), pair => pair.Value.ToString());
            messageArgs.TryGetValue("newValue", out string newValue);
            messageArgs.TryGetValue("oldValue", out string oldValue);

            var contentHeight = float.Parse(newValue);
            _webViewHeightConstraint.Constant = contentHeight;
        }

        private void OnTextChangedMessage_Handler(WKScriptMessage message)
        {
            if (!(message.Body is NSDictionary body))
            {
                Debug.WriteLine($"OnTextChangedMessage: {message.Body}");
                return;
            }

            var messageArgs = body.ToDictionary(pair => pair.Key.ToString(), pair => pair.Value.ToString());
            messageArgs.TryGetValue("html", out string html);
            _html = html;
        }
    }
}
