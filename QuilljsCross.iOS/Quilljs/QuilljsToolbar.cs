using System;
using System.Collections.Generic;
using System.Linq;
using QuilljsCross.Shared.Quilljs;
using UIKit;

namespace QuilljsCross.iOS.Quilljs
{
    public class QuilljsToolbar
        : UIToolbar, IQuilljsToolbar
    {
        private bool _disposed;

        public QuilljsToolbar(IQuilljsEditor quilljsEditor)
        {
            if (quilljsEditor == null)
            {
                throw new ArgumentNullException();
            }

            QuilljsEditor = quilljsEditor;
            QuilljsEditor.SelectionChanged += QuilljsEditor_SelectionChanged;
        }

        #region IQuilljsToolbar implementation
        public IQuilljsEditor QuilljsEditor { get; }

        public IEnumerable<IQuilljsToolbarItem> ToolbarItems
        {
            get
            {
                return Items
                    .Where(item => item is IQuilljsToolbarItem)
                    .Cast<IQuilljsToolbarItem>()
                    .ToList();
            }
        }
        #endregion

        public override UIBarButtonItem[] Items
        {
            get
            {
                return base.Items;
            }

            set
            {
                if (base.Items == value)
                {
                    return;
                }

                base.Items = value;
                UpdateQuillToolbarItemClickHandlers();
            }
        }

        protected QuilljsToolbar(IntPtr handle)
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
                foreach (var item in ToolbarItems)
                {
                    ((UIBarButtonItem)item).Clicked -= QuilljsToolbar_Clicked;
                }

                QuilljsEditor.SelectionChanged -= QuilljsEditor_SelectionChanged;
            }

            base.Dispose(disposing);
        }

        private void UpdateQuillToolbarItemClickHandlers()
        {
            foreach (var item in ToolbarItems)
            {
                ((UIBarButtonItem)item).Clicked -= QuilljsToolbar_Clicked;
                ((UIBarButtonItem)item).Clicked += QuilljsToolbar_Clicked;
            }
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

        private void QuilljsEditor_SelectionChanged(object sender, QuilljsSelectionChangedArgs e)
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
