namespace QuilljsCross.Shared.Quilljs
{
    public interface IQuilljsToolbarItem
    {
        QuilljsToolbarItemActionGroup ActionGroup { get; }

        string QuilljsFormattingAttribute { get; }

        bool IsActive { get; set; }
    }
}
