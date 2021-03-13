using ClothersForHand.Date;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClothersForHand.Pages
{
	/// <summary>
	/// Логика взаимодействия для MaterialsPage.xaml
	/// </summary>
	public partial class MaterialsPage : Page
	{
		public MaterialsPage()
		{
			InitializeComponent();
			MaterialsLV.ItemsSource = ClothersForHandDBEntities.GetContext().Material.ToList();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			 
		}
	}
}
