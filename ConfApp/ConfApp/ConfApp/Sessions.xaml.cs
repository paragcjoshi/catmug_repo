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
    public partial class Sessions : ContentPage
    {
        public ObservableCollection<SessionViewModel> sessions { get; set; }
        public Sessions()
        {
            InitializeComponent();        
            GetData();
        }
        private async Task GetData()
        {
            var assembly = typeof(Sessions).GetTypeInfo().Assembly;
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

            Stream stream = assembly.GetManifestResourceStream(resourceprefix + "session.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            sessions = JsonConvert.DeserializeObject<ObservableCollection<SessionViewModel>>(text);
            lstView.ItemsSource = sessions;        
        }

        public async void Favorites_Clicked(object sender, EventArgs e)
        {
            var id = (int)(((Button)sender).CommandParameter);
            if (App.Favoritesessions != null)
            {
                var fav = (from item in App.Favoritesessions
                           where item.SessionID == id
                           select item).FirstOrDefault();

                if (fav == null)
                {
                    var session = (from item in sessions
                                   where item.SessionID == id
                                   select item).FirstOrDefault();

                    if (session != null)
                    {
                        App.Favoritesessions.Add((SessionViewModel)session);
                        App.SaveFavorites();
                    }
                }
            }         
        }

        public async void Favorites_UnClicked(object sender, EventArgs e)
        {
            var id = (int)(((Button)sender).CommandParameter);

            if(App.Favoritesessions != null)
            {
                var session = (from item in App.Favoritesessions
                               where item.SessionID == id
                               select item).FirstOrDefault();

                if (session != null)
                {
                    App.Favoritesessions.Remove((SessionViewModel)session);
                    App.SaveFavorites();
                }
            }
           
        }
    }
}
