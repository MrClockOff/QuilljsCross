using System;

namespace QuilljsCross.Shared.Quilljs
{
    public interface IQuilljsEditor
    {
        event EventHandler<QuilljsSelectionChangedEventArgs> SelectionChanged;

        string Html { get; set; }

        string Placeholder { get; set; }

        void SetFormat(string formattingAttribute, bool apply);

        void SetList(string formattingAttribute, bool apply);

        void SetAlignment(string formattingAttribute);
    }
}
