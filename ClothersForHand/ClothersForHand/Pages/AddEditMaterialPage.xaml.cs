using ClothersForHand.Date;
using Microsoft.Win32;
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
			UnitMeasureCB.ItemsSource = ClothersForHandDBEntities.GetContext().UnitMeasure.ToList();
			SupliersDG.ItemsSource = ClothersForHandDBEntities.GetContext().PossibleSupliersMaterial.Where(x => x.MaterialID == contextMaterial.MaterialID).ToList();
			SupliersCB.ItemsSource = ClothersForHandDBEntities.GetContext().Suplier.ToList();
		}

		private void MaterialNameTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!(Char.IsDigit(e.Text, 0) || Char.IsLetter(e.Text, 0)))
			{
				e.Handled = true;
			}
		}

		private void CountInStockTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}

		private void CountInPackageTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}

		private void MinCountTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}

		private void CostTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!(Char.IsDigit(e.Text, 0) || e.Text == "."))
			{
				e.Handled = true;
			}
		}

		private void DescriptionTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!(Char.IsDigit(e.Text, 0) || e.Text == "." || e.Text == "," || Char.IsLetter(e.Text, 0)))
			{
				e.Handled = true;
			}
		}

		private void LoadImageBtn_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog()
			{
				Filter = "*.jpeg|*.jpeg"
			};

			if (dialog.ShowDialog().GetValueOrDefault())
			{
				contextMaterial.Image = File.ReadAllBytes(dialog.FileName);
				MaterialImg.Source = new BitmapImage(new Uri(dialog.FileName));
			}
		}

		
		private void AddSuplierBtn_Click(object sender, RoutedEventArgs e)
		{
			if (SupliersCB.SelectedIndex >= 0)
			{
				var suplierID = (SupliersCB.SelectedItem as Suplier).SuplierID;
				var isSuplier = ClothersForHandDBEntities.GetContext().PossibleSupliersMaterial.Where(x => x.SuplierID == suplierID && x.MaterialID == contextMaterial.MaterialID).Count();

				if (isSuplier == 0)
				{
					PossibleSupliersMaterial possibleSupliers = new PossibleSupliersMaterial();
					possibleSupliers.MaterialID = contextMaterial.MaterialID;
					possibleSupliers.SuplierID = suplierID;

					ClothersForHandDBEntities.GetContext().PossibleSupliersMaterial.Add(possibleSupliers);
					ClothersForHandDBEntities.GetContext().SaveChanges();

					SupliersDG.ItemsSource = ClothersForHandDBEntities.GetContext().PossibleSupliersMaterial.Where(x => x.MaterialID == contextMaterial.MaterialID).ToList();
				}
			}
			else
				MessageBox.Show("Выберите поставщика для добавления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
		}

		private void DeleteSuplierBtn_Click(object sender, RoutedEventArgs e)
		{
			if (SupliersDG.SelectedItem != null)
			{
				var suplierID = (SupliersDG.SelectedItem as PossibleSupliersMaterial).SuplierID;
				var removeSuplier = ClothersForHandDBEntities.GetContext().PossibleSupliersMaterial.Where(x => x.SuplierID == suplierID && x.MaterialID == contextMaterial.MaterialID).Single();

				ClothersForHandDBEntities.GetContext().PossibleSupliersMaterial.Remove(removeSuplier);
				ClothersForHandDBEntities.GetContext().SaveChanges();

				SupliersDG.ItemsSource = ClothersForHandDBEntities.GetContext().PossibleSupliersMaterial.Where(x => x.MaterialID == contextMaterial.MaterialID).ToList();
		    }
			else
				MessageBox.Show("Выберите поставщика для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
		}

		private void DeleteBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AddSaveBtn_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
