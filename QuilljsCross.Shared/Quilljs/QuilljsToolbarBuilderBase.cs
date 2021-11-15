using System.Collections.Generic;

namespace QuilljsCross.Shared.Quilljs
{    
    public abstract class QuilljsToolbarBuilderBase<TToolbar>
        : IQuillToolbarBuilder<TToolbar>
        where TToolbar : IQuilljsToolbar
    {
        protected IList<QuilljsToolbarItemModel> ToolbarItemModels { get; }

        protected QuilljsToolbarBuilderBase()
        {
            ToolbarItemModels = new List<QuilljsToolbarItemModel>();
        }

        #region IQuillToolbarBuilder implementation
        public IQuillToolbarBuilder<TToolbar> AddBoldTextButton(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.Formatting, QuilljsFormattingAttribute.Bold));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddBoldTextButton()
        {
            return AddBoldTextButton("outline_format_bold_black_24pt.png");
        }

        public IQuillToolbarBuilder<TToolbar> AddItalicTextButton(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.Formatting, QuilljsFormattingAttribute.Italic));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddItalicTextButton()
        {
            return AddItalicTextButton("outline_format_italic_black_24pt.png");
        }

        public IQuillToolbarBuilder<TToolbar> AddUnderlineTextButton(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.Formatting, QuilljsFormattingAttribute.Underline));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddUnderlineTextButton()
        {
            return AddUnderlineTextButton("outline_format_underlined_black_24pt.png");
        }

        public IQuillToolbarBuilder<TToolbar> AddBulletListButton(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.List, QuilljsFormattingAttribute.BulletList));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddBulletListButton()
        {
            return AddBulletListButton("outline_format_list_bulleted_black_24pt.png");
        }

        public IQuillToolbarBuilder<TToolbar> AddNumberListButton(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.List, QuilljsFormattingAttribute.NumberList));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddNumberListButton()
        {
            return AddNumberListButton("outline_format_list_numbered_black_24pt.png");
        }

        public IQuillToolbarBuilder<TToolbar> AddAlignLeftButton(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.Alignment, QuilljsFormattingAttribute.LeftAlignment));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddAlignLeftButton()
        {
            return AddAlignLeftButton("outline_format_align_left_black_24pt.png");
        }

        public IQuillToolbarBuilder<TToolbar> AddAlignCenterButton(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.Alignment, QuilljsFormattingAttribute.CenterAlignment));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddAlignCenterButton()
        {
            return AddAlignCenterButton("outline_format_align_center_black_24pt.png");
        }

        public IQuillToolbarBuilder<TToolbar> AddAlignRightButton(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.Alignment, QuilljsFormattingAttribute.RightAlignment));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddAlignRightButton()
        {
            return AddAlignRightButton("outline_format_align_right_black_24pt.png");
        }

        public IQuillToolbarBuilder<TToolbar> AddLinkButton(string buttonIcon)
        {
            throw new System.NotImplementedException();
        }

        public IQuillToolbarBuilder<TToolbar> AddLinkButton()
        {
            throw new System.NotImplementedException();
        }

        public IQuillToolbarBuilder<TToolbar> AddClearFormatButton(string buttonIcon)
        {
            throw new System.NotImplementedException();
        }

        public IQuillToolbarBuilder<TToolbar> AddClearFormatButton()
        {
            throw new System.NotImplementedException();
        }

        public IQuillToolbarBuilder<TToolbar> AddSeparator(string buttonIcon)
        {
            ToolbarItemModels.Add(new QuilljsToolbarItemModel(buttonIcon, QuilljsToolbarItemActionGroup.Separator, null));
            return this;
        }

        public IQuillToolbarBuilder<TToolbar> AddSeparator()
        {
            return AddSeparator("");
        }

        public abstract TToolbar Create(IQuilljsEditor quilljsEditor);
        #endregion
    }
}
