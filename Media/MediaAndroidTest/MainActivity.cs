﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Content.PM;

namespace MediaAndroidTest
{
    [Activity(Label = "MediaAndroidTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            var image = FindViewById<ImageView>(Resource.Id.imageView1);
            button.Click += async delegate
            {
                var media = new Plugin.Media.MediaImplementation();
                var file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });
                if (file == null)
                    return;

                image.SetImageBitmap(BitmapFactory.DecodeFile(file.Path));
                file.Dispose();
            };

            var pick = FindViewById<Button>(Resource.Id.button1);
            pick.Click += async (sender, args) =>
              {
                  var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();
                  if (file == null)
                      return;
                  image.SetImageBitmap(BitmapFactory.DecodeFile(file.Path));
                  file.Dispose();
              };

            FindViewById<Button>(Resource.Id.button2).Click += async (sender, args) =>
              {
                  var media = new Plugin.Media.MediaImplementation();
                  var file = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
                  {
                      Directory = "Sample",
                      Name = "test.jpg"
                  });
                  if (file == null)
                      return;

                  

                  file.Dispose();
                  
              };


            FindViewById<Button>(Resource.Id.button3).Click += async (sender, args) =>
            {
                var media = new Plugin.Media.MediaImplementation();
                var file = await Plugin.Media.CrossMedia.Current.PickVideoAsync();
                if (file == null)
                    return;


                file.Dispose();
            };

        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

