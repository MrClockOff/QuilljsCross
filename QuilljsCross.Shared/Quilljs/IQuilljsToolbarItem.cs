using System;

namespace QuilljsCross.Shared.Quilljs
{
    public interface IQuilljsToolbarItem
    {
        event EventHandler Clicked;

        QuilljsToolbarItemActionGroup ActionGroup { get; }

        string QuilljsFormattingAttribute { get; }

        bool IsActive { get; set; }
    }
}
