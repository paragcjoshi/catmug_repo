using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace ConfApp
{
	public partial class MainPage : TabbedPage
	{
        public ObservableCollection<SessionViewModel>  sessList = new ObservableCollection<SessionViewModel>();
        public MainPage()
		{
            InitializeComponent();
			this.Children.Add(new Sessions());
            this.Children.Add(new Speakers());
            this.Children.Add(new Favorites());
        }
	}
}
