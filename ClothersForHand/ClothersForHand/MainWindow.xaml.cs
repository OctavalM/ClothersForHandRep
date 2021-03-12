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

namespace ClothersForHand
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var materialsList = ClothersForHandDBEntities.GetContext().Material.Where(x => x.Img != null).ToList();

			foreach (var material in materialsList)
			{
				var m = ClothersForHandDBEntities.GetContext().Material.Where(x => x.MaterialID == material.MaterialID).Single();

				m.Image = Encoding.UTF8.GetBytes(m.Img);
				ClothersForHandDBEntities.GetContext().SaveChanges();

			}
		}
	}
}
