using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Droid = Android;

namespace GraphGeneratorApp.Platforms.Android.Handlers
{
    public class BorderlessEntryHandler : EntryHandler
    {
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);

            platformView.Background = null;
            platformView.SetPadding(0, 0, 0, 0);

            platformView.BackgroundTintList = Droid.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid()); 
        }
    }
}