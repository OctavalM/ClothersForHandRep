using ClothersForHand.Date;
using ClothersForHand.Tools;
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
		int actualPage;

		public MaterialsPage()
		{
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			MaterialTypeCB.ItemsSource = InsertCombobox.CreatedCombobox(ClothersForHandDBEntities.GetContext().MaterialType.ToList(), new MaterialType { MaterialTypeName = "Все типы" });
		    RefreshMaterials();
		}

		public void RefreshMaterials()
		{
			var filteredMaterials = ClothersForHandDBEntities.GetContext().Material.ToList();

			if (!string.IsNullOrWhiteSpace(MaterialameTB.Text))
			{
				var materialName = MaterialameTB.Text;
				filteredMaterials = filteredMaterials.Where(x => x.MaterialName.ToLower().Contains(materialName.ToLower())).ToList();
			}

            if (MaterialNameSortCB.SelectedIndex == 1)
			{
				CountInStockSortCB.IsEnabled = false;
				CostSortCB.IsEnabled = false;

				filteredMaterials = filteredMaterials.OrderBy(x => x.MaterialName).ToList();
			}
			else if (MaterialNameSortCB.SelectedIndex == 2)
			{
				CountInStockSortCB.IsEnabled = false;
				CostSortCB.IsEnabled = false;

				filteredMaterials = filteredMaterials.OrderByDescending(x => x.MaterialName).ToList();
			}

			if (CountInStockSortCB.SelectedIndex == 0)
			{
				MaterialNameSortCB.IsEnabled = true;
				CostSortCB.IsEnabled = true;

				filteredMaterials = filteredMaterials.OrderBy(x => x.CountInStock).ToList();
			}
			else if (CountInStockSortCB.SelectedIndex == 1)
			{
				MaterialNameSortCB.IsEnabled = false;
				CostSortCB.IsEnabled = false;

				filteredMaterials = filteredMaterials.OrderByDescending(x => x.CountInStock).ToList();
			}

			if (CostSortCB.SelectedIndex == 0)
			{
				MaterialNameSortCB.IsEnabled = true;
				CountInStockSortCB.IsEnabled = true;

				filteredMaterials = filteredMaterials.OrderBy(x => x.Cost).ToList();
			}
			else if (CostSortCB.SelectedIndex == 1)
			{
				MaterialNameSortCB.IsEnabled = false;
				CountInStockSortCB.IsEnabled = false;

				filteredMaterials = filteredMaterials.OrderByDescending(x => x.Cost).ToList();
			}

			if (MaterialTypeCB.SelectedIndex > 0)
			{
				var selectedType = MaterialTypeCB.SelectedItem as MaterialType;
				filteredMaterials = filteredMaterials.Where(x => x.MaterialTypeID == selectedType.MaterialTypeID).ToList();
			}

			MaterialsLV.ItemsSource = filteredMaterials.Take(15).ToList(); 
		}

		private void MaterialNameSortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{ 
			RefreshMaterials();
		}

		private void CountInStockSortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RefreshMaterials();

		}

		private void CostSortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RefreshMaterials();

		}

		private void MaterialTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RefreshMaterials();

		}
	}
}
