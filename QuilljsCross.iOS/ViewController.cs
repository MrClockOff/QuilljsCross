using System;
using QuilljsCross.iOS.Quilljs;
using UIKit;

namespace QuilljsCross.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var title = "Start Quilljs";
            Button.SetTitle(title, UIControlState.Normal);
            Button.TouchUpInside += delegate
            {
                var editorViewController = new QuilljsViewController();
                editorViewController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
                editorViewController.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;
                PresentModalViewController(editorViewController, true);
            };
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
