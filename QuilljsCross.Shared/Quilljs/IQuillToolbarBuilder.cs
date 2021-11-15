namespace QuilljsCross.Shared.Quilljs
{
    public interface IQuillToolbarBuilder<TToolbar>
        where TToolbar : IQuilljsToolbar
    {
        IQuillToolbarBuilder<TToolbar> AddBoldTextButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddBoldTextButton();

        IQuillToolbarBuilder<TToolbar> AddItalicTextButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddItalicTextButton();

        IQuillToolbarBuilder<TToolbar> AddUnderlineTextButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddUnderlineTextButton();

        IQuillToolbarBuilder<TToolbar> AddBulletListButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddBulletListButton();

        IQuillToolbarBuilder<TToolbar> AddNumberListButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddNumberListButton();

        IQuillToolbarBuilder<TToolbar> AddAlignLeftButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddAlignLeftButton();

        IQuillToolbarBuilder<TToolbar> AddAlignCenterButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddAlignCenterButton();

        IQuillToolbarBuilder<TToolbar> AddAlignRightButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddAlignRightButton();

        IQuillToolbarBuilder<TToolbar> AddLinkButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddLinkButton();

        IQuillToolbarBuilder<TToolbar> AddClearFormatButton(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddClearFormatButton();

        IQuillToolbarBuilder<TToolbar> AddSeparator(string buttonIcon);

        IQuillToolbarBuilder<TToolbar> AddSeparator();

        TToolbar Create(IQuilljsEditor quilljsEditor);
    }
}
