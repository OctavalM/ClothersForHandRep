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
using System.Windows.Shapes;

namespace ClothersForHand.Windows
{
	/// <summary>
	/// Логика взаимодействия для EditMinCountWindow.xaml
	/// </summary>
	public partial class EditMinCountWindow : Window
	{
		public List<Material> materialsList = new List<Material>();

		public EditMinCountWindow(List<Material> postMaterialsList)
		{
			InitializeComponent();
			materialsList = postMaterialsList;

			MinCountTB.Text = $"{materialsList.Max(x => x.MinCount)}";
		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{
			int minCount = Convert.ToInt32(MinCountTB.Text);

			foreach (var material in materialsList)
			{
				materialsList.Where(x => x.MaterialID == material.MaterialID).Single().MinCount = minCount;
			}

			DialogResult = true;
		}

		private void MinCountTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}
	}
}
