using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ConfApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Favorites : ContentPage
	{
		public Favorites ()
		{
			InitializeComponent ();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
            GetData();
		}

        private async Task GetData()
        {
            FileHelper file = new FileHelper();     
            string text =  await file.LoadFile("favorites.txt");         
            ObservableCollection<SessionViewModel> models = JsonConvert.DeserializeObject<ObservableCollection<SessionViewModel>>(text);
            App.Favoritesessions = models;
            lstfavoriteView.ItemsSource = App.Favoritesessions;
        }
    }
}
