using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Speakers : ContentPage
	{
        public ObservableCollection<SpeakerViewModel> speakers { get; set; }
        public Speakers ()
		{
			InitializeComponent ();
            GetData();

        }

        private async Task GetData()
        {
            var assembly = typeof(Speakers).GetTypeInfo().Assembly;
            string resourceprefix = string.Empty;
#if __IOS__
                  resourceprefix = "ConfApp.iOS.";
#endif
#if __ANDROID__
            resourceprefix = "ConfApp.Droid.";
#endif
#if WINDOWS_PHONE
                  resourceprefix= "ConfApp.WinPhone.";
#endif

            Stream stream = assembly.GetManifestResourceStream(resourceprefix + "speakers.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            speakers = JsonConvert.DeserializeObject<ObservableCollection<SpeakerViewModel>>(text);
            lstSpeakersView.ItemsSource = speakers;
        }
    }
}
