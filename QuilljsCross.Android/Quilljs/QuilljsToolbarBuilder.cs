using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using QuilljsCross.Android.Extensions;
using QuilljsCross.Shared.Quilljs;

namespace QuilljsCross.Android.Quilljs
{
    public class QuilljsToolbarBuilder
        : QuilljsToolbarBuilderBase<QuilljsToolbar>
    {
        private Context _context;

        public static QuilljsToolbarBuilder Start(Context context)
        {
            return new QuilljsToolbarBuilder(context);
        }

        ~ QuilljsToolbarBuilder()
        {
            _context = null;
        }

        public override QuilljsToolbar Create(IQuilljsEditor quilljsEditor)
        {
            var quilljsToolbar = new QuilljsToolbar(_context, quilljsEditor)
            {
                Orientation = Orientation.Horizontal
            };
            var quilljsToolbarItems = ToolbarItemModels
                .Select(model =>
                {
                    var toolbarItem = new QuilljsToolbarItem(_context, model.ActionGroup, model.QuilljsFormattingAttribute);
                    var toolbarItemDrawable = _context.GetDrawableByName(model.Icon);
                    var toolbarItemLayoutParams = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.WrapContent)
                    {
                        Weight = 1
                    };
                    toolbarItem.SetImageDrawable(toolbarItemDrawable);
                    toolbarItem.LayoutParameters = toolbarItemLayoutParams;
                    return toolbarItem;
                })
                .ToArray();

            foreach (var toolbarItem in quilljsToolbarItems)
            {
                quilljsToolbar.AddView(toolbarItem);
            }

            return quilljsToolbar;
        }

        protected QuilljsToolbarBuilder(Context context)
        {
            _context = context;
        }
    }
}
