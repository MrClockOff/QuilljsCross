using Android.Content;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;

namespace QuilljsCross.Android.Extensions
{
    public static class ContextExtensions
    {
        public static float ConvertDpToPx(this Context context, float dp)
        {
            var density = context.Resources.DisplayMetrics.Density;
            var px = dp * density;
            return px;
        }

        public static Drawable GetDrawableByName(this Context context, string resourceName)
        {
            return GetDrawableByName(context, resourceName, context.PackageName);
        }

        public static Drawable GetDrawableByName(this Context context, string resourceName, string packageName)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return null;
            }

            var resourceId = GetDrawableIdByName(context, resourceName, packageName);
            var drawable = ContextCompat.GetDrawable(context, resourceId);
            return drawable;
        }

        public static int GetDrawableIdByName(this Context context, string resourceName)
        {
            return GetDrawableIdByName(context, resourceName, context.PackageName);
        }

        public static int GetDrawableIdByName(this Context context, string resourceName, string packageName)
        {
            var resourceId = context.Resources.GetIdentifier(resourceName.Split(".")[0], "drawable", packageName);
            return resourceId;
        }
    }
}
