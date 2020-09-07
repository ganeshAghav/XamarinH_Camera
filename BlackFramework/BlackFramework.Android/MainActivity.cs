using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.Support.V4.App;
using Android.Util;
using Android.Webkit;
using BlackFramework.Droid.Controller;
using ModelViewUI;
using Android.Support.V4.Content;
using Android.Content;
using Android.Provider;
using Android.Graphics;
using System.IO;

namespace BlackFramework.Droid
{
    [Activity(Label = "Black Framework", MainLauncher = true, NoHistory = false, Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.AdjustPan)]
    public class MainActivity : Activity
    {

        string AppName = "Black Framework";
        const int REQUEST_WRITE_PERMISSION = 1;
        const int REQUEST_READ_PERMISSION = 2;
        const int REQUEST_LOCATION_PERMISSION = 3;
        const int REQUEST_CAMERA = 4;
        const int REQUEST_RECORD_AUDIO_PERMISSION = 5;
        const int REQUEST_SMSSENT = 6;
        const int REQUEST_PHONESTATE = 7;

        static string[] PERMISSIONS_PHONESTATE = {
        Manifest.Permission.ReadPhoneState
        };

        static string[] PERMISSIONS_SMS = {
        Manifest.Permission.SendSms
        };

        static string[] PERMISSIONS_LOCATION = {
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessCoarseLocation
        };

        static string[] PERMISSIONS_CAMARA = {
            Manifest.Permission.Camera
        };

        static string[] PERMISSIONS_AUDIO = {
            Manifest.Permission.RecordAudio
        };

        static string[] PERMISSIONS_Read = {

            Manifest.Permission.ReadExternalStorage
        };
        static string[] PERMISSIONS_WRIGHT = {

            Manifest.Permission.WriteExternalStorage,

        };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            string PhoneModel = Android.OS.Build.Model;

            SetContentView(Resource.Layout.Main);

            ResourceManager.EnsureResources(
                     typeof(IDataAccess).Assembly,
                     String.Format("/data/data/{0}/files", Application.Context.PackageName));

            RequestSMSPermission();

          
        }
        void RequestSMSPermission()
        {
            Log.Info(AppName, "SMS permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.SendSms) == (int)Permission.Granted)
            {
                RequestWrightPermission();
            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_SMS, REQUEST_SMSSENT);
            }
        }

        void RequestWrightPermission()
        {
            Log.Info(AppName, "CAMERA permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted)
            {
                RequestReadPermission();

            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_WRIGHT, REQUEST_WRITE_PERMISSION);
            }
        }

        void RequestReadPermission()
        {
            Log.Info(AppName, "Read permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
            {
                RequestLocationPermission();

            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_Read, REQUEST_READ_PERMISSION);
            }
        }

        void RequestLocationPermission()
        {
            Log.Info(AppName, "Location permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == (int)Permission.Granted)
            {
                RequestCameraPermission();

            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_LOCATION, REQUEST_LOCATION_PERMISSION);
            }
        }

        void RequestCameraPermission()
        {
            Log.Info(AppName, "CAMERA permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted)
            {
                RequestAudioPermission();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, PERMISSIONS_CAMARA, REQUEST_CAMERA);

            }
        }

        void RequestAudioPermission()
        {
            Log.Info(AppName, "Read Phone State permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.RecordAudio) == (int)Permission.Granted)
            {
                RequestReadPhoneStatePermission();

            }
            else
            {
                ActivityCompat.RequestPermissions(this, PERMISSIONS_AUDIO, REQUEST_RECORD_AUDIO_PERMISSION);
            }
        }

        void RequestReadPhoneStatePermission()
        {
            Log.Info(AppName, "SMS permission has NOT been granted. Requesting permission.");
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.ReadPhoneState) == (int)Permission.Granted)
            {
                RegisterControllersToPortableRazor();
            }
            else
            {
                // Camera permission has not been granted yet. Request it directly.
                ActivityCompat.RequestPermissions(this, PERMISSIONS_PHONESTATE, REQUEST_PHONESTATE);
            }
        }

        void RegisterControllersToPortableRazor()
        {
            startCamera();

            //var webView = FindViewById<WebView>(Resource.Id.webView);
            //PortableRazor.RouteHandler.Controllers.Clear();
            //var LoginController = new LoginController(new HybridWebView(webView),this);
            //PortableRazor.RouteHandler.RegisterController("LoginController", LoginController);
            //LoginController.OnCreate();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case REQUEST_READ_PERMISSION:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {
                            //Permission granted
                            RequestLocationPermission();

                        }
                        else
                        {
                            Toast.MakeText(this, "Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_WRITE_PERMISSION:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {
                            //Permission granted
                            RequestReadPermission();

                        }
                        else
                        {
                            Toast.MakeText(this, "Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_LOCATION_PERMISSION:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {
                            RequestCameraPermission();
                        }
                        else
                        {
                            Toast.MakeText(this, " LOCATION Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_RECORD_AUDIO_PERMISSION:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {

                            RegisterControllersToPortableRazor();
                        }
                        else
                        {
                            Toast.MakeText(this, " LOCATION Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_CAMERA:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {
                            //Permission granted
                            RequestAudioPermission();
                        }
                        else
                        {
                            Toast.MakeText(this, " Camera Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_SMSSENT:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {
                            RequestWrightPermission();
                        }
                        else
                        {
                            Toast.MakeText(this, " SMS Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;
                case REQUEST_PHONESTATE:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {
                            RegisterControllersToPortableRazor();
                        }
                        else
                        {
                            Toast.MakeText(this, " PHONESTATE Permission Denied", ToastLength.Long).Show();
                        }
                    }
                    break;

            }



        }

        public static string FileProviderAuthority = "com.magmahdi.preinspection.fileprovider";

        public void startCamera()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == Permission.Granted)
                {
                    
                    Intent intent = new Intent(MediaStore.ActionImageCapture);
                    //Android.Net.Uri photoURI = FileProvider.GetUriForFile(this.ApplicationContext, FileProviderAuthority, CreateImage());
                    //intent.PutExtra(MediaStore.ExtraOutput, photoURI);
                    Activity activity = (Activity)this;
                    intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                    activity.StartActivityForResult(intent, REQUEST_CAMERA);
                }
                else
                {
                    ActivityCompat.RequestPermissions((Activity)this, PERMISSIONS_CAMARA, REQUEST_CAMERA);
                }
            }
        }

        public static Java.IO.File CreateImage()
        {
            // Create an image file name
            String timeStamp = new Java.Text.SimpleDateFormat("yyyyMMdd_HHmmss").Format(new Java.Util.Date());
            String imageFileName = "JPEG_" + timeStamp + "_";
            Java.IO.File _dir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryDownloads), "iTempFiles");

            if (_dir.Exists())
            {
                foreach (string file in _dir.List())
                {
                    new Java.IO.File(_dir, file).Delete();
                }
            }
            else
            {
                _dir.Mkdirs();
            }

            Java.IO.File image = Java.IO.File.CreateTempFile(
                    imageFileName,  /* prefix */
                    ".jpeg",         /* suffix */
                    _dir      /* directory */
            );

            // Save a file: path for use with ACTION_VIEW intents
            string CaptureImage = null;
            CaptureImage = "file:" + image.AbsolutePath;
            return image;
        }

        public string CaptureImage { get; set; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            try
            {
                if (requestCode == REQUEST_CAMERA && resultCode == Result.Ok)
                {
                    var bitmap = (Bitmap)data.Extras.Get("data");                   
                   // var bitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, Android.Net.Uri.Parse(CaptureImage));
                    string imageString = string.Empty;
                    using (var stream = new MemoryStream())
                    {
                        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 55, stream);
                        var bytes = stream.ToArray();
                        imageString = Convert.ToBase64String(bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("OnActivityResult", ex.StackTrace.ToString());
            }
        }
    }
}

