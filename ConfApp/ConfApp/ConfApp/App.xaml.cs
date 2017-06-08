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

namespace ConfApp
{
    public partial class App : Application
    {
        public static ObservableCollection<SessionViewModel> Favoritesessions { get; set; }
        public static string ResourcePrefix { get; set; }

        public App()
        {
            InitializeComponent();
                 MainPage = new ConfApp.MainPage();

            //MainPage = new NavigationPage(new ConfApp.MainPage());
            //NavigationPage.SetHasNavigationBar(this, true);
            

            //var nav = new NavigationPage(new ConfApp.Welcome());
            //nav.BarBackgroundColor = new Color(218.0 / 255.0, 80.0 / 255.0, 152.0 / 255.0);
            //nav.BarTextColor = Color.White;

            // MainPage = nav;
            // MainPage = new NavigationPage(new ConfApp.Welcome()) { BarBackgroundColor = new Color(218.0/255.0,80.0/255.0,152.0/255.0), BarTextColor = Color.White};

        }

        protected override void OnStart()
        {
            Favoritesessions = new ObservableCollection<SessionViewModel>();           
        }

        public static async Task<string> FetchData()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("ConfApp.session.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }

        public static async Task SaveFavorites()
        {
            FileHelper file = new FileHelper();
            string values = JsonConvert.SerializeObject(Favoritesessions);
            await file.CreateFile("favorites.txt", values);            
        }


        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
