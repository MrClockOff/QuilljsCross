using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QuilljsCross.Shared.Quilljs;
using static Android.Views.ViewGroup;

namespace QuilljsCross.Android.Quilljs
{
    public class QuilljsToolbar
        : LinearLayout, IQuilljsToolbar, IOnHierarchyChangeListener
    {
        private bool _disposed;

        public QuilljsToolbar(Context context, IQuilljsEditor quilljsEditor)
            : base(context)
        {
            if (quilljsEditor == null)
            {
                throw new ArgumentNullException();
            }

            QuilljsEditor = quilljsEditor;
            QuilljsEditor.SelectionChanged += QuilljsEditor_SelectionChanged;
            SetOnHierarchyChangeListener(this);
        }

        ~ QuilljsToolbar()
        {
            SetOnHierarchyChangeListener(null);
        }

        #region IQuilljsToolbar implementation
        public IQuilljsEditor QuilljsEditor { get; }

        public IEnumerable<IQuilljsToolbarItem> ToolbarItems
        {
            get
            {
                var toolbarItems = new List<IQuilljsToolbarItem>();

                for (int i = 0; i < ChildCount; i++)
                {
                    if (GetChildAt(i) is IQuilljsToolbarItem toolbarItem)
                    {
                        toolbarItems.Add(toolbarItem);
                    }
                }

                return toolbarItems;
            }
        }
        #endregion

        #region IOnHierarchyChangeListener implementation
        public void OnChildViewAdded(View parent, View child)
        {
            if (child is IQuilljsToolbarItem toolbarItem)
            {
                toolbarItem.Clicked += QuilljsToolbar_Clicked;
            }
        }

        public void OnChildViewRemoved(View parent, View child)
        {
            if (child is IQuilljsToolbarItem toolbarItem)
            {
                toolbarItem.Clicked -= QuilljsToolbar_Clicked;
            }
        }
        #endregion

        protected QuilljsToolbar(IntPtr javaReference, JniHandleOwnership transfer)
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
                foreach (var item in ToolbarItems)
                {
                    item.Clicked -= QuilljsToolbar_Clicked;
                }

                QuilljsEditor.SelectionChanged -= QuilljsEditor_SelectionChanged;
            }

            base.Dispose(disposing);
        }

        private void QuilljsToolbar_Clicked(object sender, EventArgs e)
        {
            var toolbarItem = sender as IQuilljsToolbarItem;
            var apply = toolbarItem.IsActive = !toolbarItem.IsActive;

            switch (toolbarItem.ActionGroup)
            {
                case QuilljsToolbarItemActionGroup.Formatting:
                    QuilljsEditor.SetFormat(toolbarItem.QuilljsFormattingAttribute, apply);
                    break;
                case QuilljsToolbarItemActionGroup.List:
                    QuilljsEditor.SetList(toolbarItem.QuilljsFormattingAttribute, apply);
                    break;
                case QuilljsToolbarItemActionGroup.Alignment:
                    QuilljsEditor.SetAlignment(toolbarItem.QuilljsFormattingAttribute);
                    break;
                default:
                    break;
            }
        }

        private void QuilljsEditor_SelectionChanged(object sender, QuilljsSelectionChangedEventArgs e)
        {
            foreach (var toolbarItem in ToolbarItems)
            {
                if (e.SelectionFormattingAttributes.Contains(toolbarItem.QuilljsFormattingAttribute))
                {
                    toolbarItem.IsActive = true;
                }
                else
                {
                    toolbarItem.IsActive = false;
                }
            }
        }
    }
}
