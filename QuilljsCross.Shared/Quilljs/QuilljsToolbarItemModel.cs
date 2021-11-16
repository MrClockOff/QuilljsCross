namespace QuilljsCross.Shared.Quilljs
{
    public static class QuilljsFormattingAttribute
    {
        public static readonly string Bold = "bold";
        public static readonly string Italic = "italic";
        public static readonly string Underline = "underline";
        public static readonly string BulletList = "bullet";
        public static readonly string NumberList = "ordered";
        public static readonly string LeftAlignment = string.Empty;
        public static readonly string CenterAlignment = "center";
        public static readonly string RightAlignment = "right";
        public static readonly string Link = "";
    }

    public enum QuilljsToolbarItemActionGroup
    {
        Formatting,
        Alignment,
        List,
        Separator
    }

    public class QuilljsToolbarItemModel
    {
        public QuilljsToolbarItemModel(string itemIcon, QuilljsToolbarItemActionGroup itemActionGroup, string itemFormattingAttribute)
        {
            ActionGroup = itemActionGroup;
            QuilljsFormattingAttribute = itemFormattingAttribute;
            Icon = itemIcon;
        }

        public QuilljsToolbarItemActionGroup ActionGroup { get; }

        public string QuilljsFormattingAttribute { get; }

        public string Icon { get; set; }
    }
}
