using ClothersForHand.Date;
using ClothersForHand.Tools;
using ClothersForHand.Windows;
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
		int pageIndex = 1;
		int itemsOnPage = 15;
		List<Material> selectedMaterialsList = new List<Material>();
		List<Material> filteredMaterials = new List<Material>();
		List<Material> materialsList = new List<Material>();
		List<PageNumber> pageNumbers = new List<PageNumber>();

		public MaterialsPage()
		{
			InitializeComponent();
		}

		private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			selectedMaterialsList = new List<Material>();
			EditMinCountBtn.Visibility = Visibility.Hidden;

			if (Visibility == Visibility.Visible)
			{
				ClothersForHandDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
				MaterialTypeCB.ItemsSource = InsertCombobox.CreatedCombobox(ClothersForHandDBEntities.GetContext().MaterialType.ToList(), new MaterialType { MaterialTypeName = "Все типы" });
				RefreshMaterials();
			} 
		}

		private void MaterialNameTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			RefreshMaterials();
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

		private void MaterialsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedMaterial = MaterialsLV.SelectedItem as Material;

			if (selectedMaterial == null)
				return;
			else
			{
				AddEditMaterialPage addEditMaterialPage = new AddEditMaterialPage(selectedMaterial);
				addEditMaterialPage.TitleTB.Text = "Редактирование материала";
				addEditMaterialPage.AddSaveBtn.Content = "Сохранить";

				NavigationService.Navigate(addEditMaterialPage);
			}
		}

		private void SelectedMaterialCB_Checked(object sender, RoutedEventArgs e)
		{
			var check = sender as CheckBox;
			var material = check.DataContext as Material;

			selectedMaterialsList.Add(material);

			if (selectedMaterialsList.Count > 1)
				EditMinCountBtn.Visibility = Visibility.Visible;
		}

		private void SelectedMaterialCB_Unchecked(object sender, RoutedEventArgs e)
		{
			var check = sender as CheckBox;
			var material = check.DataContext as Material;

			selectedMaterialsList.Remove(material);

			if (selectedMaterialsList.Count <= 1)
				EditMinCountBtn.Visibility = Visibility.Hidden;
		}

		private void AddMaterialBtn_Click(object sender, RoutedEventArgs e)
		{
			AddEditMaterialPage addEditMaterialPage = new AddEditMaterialPage(new Material());
			addEditMaterialPage.TitleTB.Text = "Добавление материала";
			addEditMaterialPage.AddSaveBtn.Content = "Добавить";

			NavigationService.Navigate(addEditMaterialPage);
		}

		private void EditMinCountBtn_Click(object sender, RoutedEventArgs e)
		{
			EditMinCountWindow editMinCountWindow = new EditMinCountWindow(selectedMaterialsList);

			if (editMinCountWindow.ShowDialog() == true)
			{
				foreach (var material in editMinCountWindow.materialsList)
				{
					ClothersForHandDBEntities.GetContext().Material.Where(x => x.MaterialID == material.MaterialID).Single().MinCount = material.MinCount;
					ClothersForHandDBEntities.GetContext().SaveChanges();
					RefreshMaterials();
				}
			}
		}

		private void PrevBtn_Click(object sender, RoutedEventArgs e)
		{
			pageIndex--;  
			RefreshMaterials();

			selectedMaterialsList = new List<Material>();
		    EditMinCountBtn.Visibility = Visibility.Hidden;
		}

		private void NextBtn_Click(object sender, RoutedEventArgs e)
		{
			pageIndex++; 
			RefreshMaterials();

			selectedMaterialsList = new List<Material>();
			EditMinCountBtn.Visibility = Visibility.Hidden;
		}

		private void GoPageBtn_Click(object sender, RoutedEventArgs e)
		{
			var enterBtn = sender as Button;
			var numberPage = (enterBtn.DataContext as PageNumber).Number;

			pageIndex = numberPage;
			ViewPage();

			selectedMaterialsList = new List<Material>();
			EditMinCountBtn.Visibility = Visibility.Hidden;
		}

		public void RefreshMaterials()
		{
			filteredMaterials = ClothersForHandDBEntities.GetContext().Material.ToList();

			if (!string.IsNullOrWhiteSpace(MaterialNameTB.Text))
			{
				var materialName = MaterialNameTB.Text;
				filteredMaterials = filteredMaterials.Where(x => x.MaterialName.ToLower().Contains(materialName.ToLower())).ToList();
			}

			if (MaterialNameSortCB.SelectedIndex == 0)
			{
				CountInStockSortCB.IsEnabled = true;
			    CostSortCB.IsEnabled = true;

				filteredMaterials = filteredMaterials.OrderBy(x => x.MaterialID).ToList();
			}
			else if (MaterialNameSortCB.SelectedIndex == 1)
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

				filteredMaterials = filteredMaterials.OrderBy(x => x.MaterialID).ToList();
			}
			else if (CountInStockSortCB.SelectedIndex == 1)
			{
				MaterialNameSortCB.IsEnabled = false;
				CostSortCB.IsEnabled = false;

				filteredMaterials = filteredMaterials.OrderBy(x => x.CountInStock).ToList();
			}
			else if (CountInStockSortCB.SelectedIndex == 2)
			{
				MaterialNameSortCB.IsEnabled = false;
				CostSortCB.IsEnabled = false;

				filteredMaterials = filteredMaterials.OrderByDescending(x => x.CountInStock).ToList();
			} 

			if (CostSortCB.SelectedIndex == 0)
			{ 
				MaterialNameSortCB.IsEnabled = true;
				CountInStockSortCB.IsEnabled = true; 

				filteredMaterials = filteredMaterials.OrderBy(x => x.MaterialID).ToList();
			}
			else if (CostSortCB.SelectedIndex == 0)
			{
				MaterialNameSortCB.IsEnabled = false;
				CountInStockSortCB.IsEnabled = false;

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

			pageNumbers = new List<PageNumber>();
 			UpdatePageNumbers();
			ViewPage();
		}

		public void UpdatePageNumbers()
		{
			var itemsCount = filteredMaterials.Count;
			double pageCount;

			if (itemsCount % itemsOnPage != 0)
			{
				pageCount = itemsCount / itemsOnPage + 1;
			}
			else
				pageCount = itemsCount / itemsOnPage;

			for (int i = 1; i <= pageCount; i++)
			{
				var number = new PageNumber();
				number.Number = i;

				pageNumbers.Add(number);
			}

			PageNumbersLV.ItemsSource = null;
			PageNumbersLV.ItemsSource = pageNumbers;
		}

		private void ViewPage()
		{
			int count;
			if (pageIndex == 1)
			{
				PrevBtn.IsEnabled = false;
				NextBtn.IsEnabled = true;

				materialsList = filteredMaterials.Take(itemsOnPage).ToList();
				CountRecordsTB.Text = $"{itemsOnPage} из {filteredMaterials.Count}";
			}

			if (pageIndex > 1 && filteredMaterials.Count > 0)
			{
				PrevBtn.IsEnabled = true;

				if (filteredMaterials.Skip(itemsOnPage * (pageIndex - 1)).Take(itemsOnPage).Count() < itemsOnPage)
				{
					var itemsCount = filteredMaterials.Skip(itemsOnPage * (pageIndex - 1)).Take(itemsOnPage).Count();
					materialsList = filteredMaterials.Skip(itemsOnPage * (pageIndex - 1)).Take(itemsCount).ToList();
					count = itemsOnPage * (pageIndex - 1) + filteredMaterials.Skip(itemsOnPage * (pageIndex - 1)).Take(itemsCount).Count();
					CountRecordsTB.Text = $"{count} из {filteredMaterials.Count}";

					NextBtn.IsEnabled = false;
				}
				else
				{
					NextBtn.IsEnabled = true;

					materialsList = filteredMaterials.Skip(itemsOnPage * (pageIndex - 1)).Take(itemsOnPage).ToList();
					count = itemsOnPage * (pageIndex - 1) + filteredMaterials.Skip(itemsOnPage * (pageIndex - 1)).Take(itemsOnPage).Count();
					CountRecordsTB.Text = $"{count} из {filteredMaterials.Count}";
				}
			}

			MaterialsLV.ItemsSource = null;
			MaterialsLV.ItemsSource = materialsList;
		} 
	}
}
