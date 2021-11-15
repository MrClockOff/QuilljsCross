using System.Linq;
using QuilljsCross.Shared.Quilljs;
using UIKit;

namespace QuilljsCross.iOS.Quilljs
{
    public class QuilljsToolbarBuilder
        : QuilljsToolbarBuilderBase<QuilljsToolbar>
    {
        public static QuilljsToolbarBuilder Start()
        {
            return new QuilljsToolbarBuilder();
        }

        public override QuilljsToolbar Create(IQuilljsEditor quilljsEditor)
        {
            var quilljsToolbar = new QuilljsToolbar(quilljsEditor);
            var quilljsToolbarItems = ToolbarItemModels
                .Select(model => new QuilljsToolbarItem(model.ActionGroup, model.QuilljsFormattingAttribute)
                {
                    Image = UIImage.FromBundle(model.Icon).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate)
                })
                .ToArray();

            quilljsToolbar.Items = quilljsToolbarItems;
            quilljsToolbar.SizeToFit();
            return quilljsToolbar;
        }
    }
}
