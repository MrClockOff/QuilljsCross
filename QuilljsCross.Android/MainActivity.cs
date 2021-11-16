using Android.Widget;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.App;
using Android.Runtime;
using System;
using QuilljsCross.Android.Quilljs;

namespace QuilljsCross.Android
{
    [Activity(Label = "WYSIWYG.Test", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        public MainActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(Application);

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainActivity);
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                var frameLayout = FindViewById<FrameLayout>(Resource.Id.ContentFrameLayout);
                frameLayout.RemoveAllViews();

                var quilljsFragmentClass = Java.Lang.Class.FromType(typeof(QuilljsFragment)); 
                var quilljsFragment = SupportFragmentManager.FragmentFactory.Instantiate(quilljsFragmentClass.ClassLoader, quilljsFragmentClass.Name);
                var fragmentTransaction = SupportFragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.ContentFrameLayout, quilljsFragment, nameof(QuilljsFragment));
                fragmentTransaction.Commit();
            };
        }

        protected MainActivity(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}

