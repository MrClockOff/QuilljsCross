using System.Collections.Generic;

namespace QuilljsCross.Shared.Quilljs
{
    public interface IQuilljsToolbar
    {
        IQuilljsEditor QuilljsEditor { get; }

        IEnumerable<IQuilljsToolbarItem> ToolbarItems { get; }
    }
}
