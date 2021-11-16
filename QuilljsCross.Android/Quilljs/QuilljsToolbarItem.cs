using System;
using Android.Content;
using Android.Runtime;
using AndroidX.AppCompat.Widget;
using QuilljsCross.Shared.Quilljs;

namespace QuilljsCross.Android.Quilljs
{    
    public class QuilljsToolbarItem
        : AppCompatImageButton, IQuilljsToolbarItem
    {       
        public QuilljsToolbarItem(Context context, QuilljsToolbarItemActionGroup actionGroup, string formattingAttribute)
            : base(context)
        {
            ActionGroup = actionGroup;
            QuilljsFormattingAttribute = formattingAttribute;
            Click += QuilljsToolbarItem_Click;
        }

        ~ QuilljsToolbarItem()
        {
            Click -= QuilljsToolbarItem_Click;
        }

        #region IQuilljsToolbarItem implementation
        public event EventHandler Clicked;

        public QuilljsToolbarItemActionGroup ActionGroup { get; }

        public string QuilljsFormattingAttribute { get; }

        public bool IsActive { get; set; }
        #endregion

        public QuilljsToolbarItem(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        private void QuilljsToolbarItem_Click(object sender, EventArgs e)
        {
            Clicked?.Invoke(sender, e);
        }
    }
}

