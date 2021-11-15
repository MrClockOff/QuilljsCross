using System;
using QuilljsCross.Shared.Quilljs;
using UIKit;

namespace QuilljsCross.iOS.Quilljs
{
    public class QuilljsToolbarItem
        : UIBarButtonItem, IQuilljsToolbarItem
    {
        public QuilljsToolbarItem(QuilljsToolbarItemActionGroup actionGroup, string formattingAttribute)
        {
            ActionGroup = actionGroup;
            QuilljsFormattingAttribute = formattingAttribute;
        }

        #region IQuilljsToolbarItem implementation
        public QuilljsToolbarItemActionGroup ActionGroup { get; }

        public string QuilljsFormattingAttribute { get; }

        public bool IsActive { get; set; }
        #endregion

        protected QuilljsToolbarItem(IntPtr handle)
            : base(handle)
        {
        }
    }
}
