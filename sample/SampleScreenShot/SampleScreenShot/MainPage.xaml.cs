using Plugin.Screenshot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleScreenShot
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var stream1 = new MemoryStream(await CrossScreenshot.Current.CaptureAsync());
            IncidentImageData.Source = ImageSource.FromStream(() => stream1);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                string path = await CrossScreenshot.Current.CaptureAndSaveAsync();
                label.Text = "Location: " + path;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
            }
            
        }
    }
}
