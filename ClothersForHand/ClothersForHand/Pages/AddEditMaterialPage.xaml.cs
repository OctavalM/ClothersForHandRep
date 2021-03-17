using ClothersForHand.Date;
using System;
using System.Collections.Generic;
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
	/// Логика взаимодействия для AddEditMaterialPage.xaml
	/// </summary>
	public partial class AddEditMaterialPage : Page
	{
		private Material contextMaterial = new Material();
		
		public AddEditMaterialPage(Material postMaterial)
		{
			InitializeComponent();
			contextMaterial = postMaterial;
			DataContext = contextMaterial;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			MaterialTypeCB.ItemsSource = ClothersForHandDBEntities.GetContext().MaterialType.ToList();
		}
	}
}
