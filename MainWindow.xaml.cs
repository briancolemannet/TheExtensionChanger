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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace TheExtensionChanger
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DirectoryInfo directoryInfo;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnSelectDirectory_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new FolderBrowserDialog();
			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				directoryInfo = new DirectoryInfo(dialog.SelectedPath);
				if (!directoryInfo.Exists)
				{
					directoryInfo = null;
				}
				else
				{
					txtLog.AppendText("Directory Selected: " + directoryInfo.FullName + "\n");
				}
			}
		}

		private void btnChangeExtension_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtNewExtension.Text) && !string.IsNullOrWhiteSpace(txtOriginalExtension.Text))
			{
				var newDirectory = directoryInfo.CreateSubdirectory((new Random()).Next(0, 100000).ToString("000000"));
				txtLog.AppendText("New Directory: " + newDirectory.Name + "\n");
				int x = 0;
				foreach (var fileInfo in directoryInfo.GetFiles("*." + txtOriginalExtension.Text))
				{
					fileInfo.CopyTo(Path.Combine(newDirectory.FullName, Path.GetFileNameWithoutExtension(fileInfo.Name) + "." + txtNewExtension.Text));
					x++;
					txtLog.AppendText(".");
					if (x % 10 == 0)
					{
						txtLog.AppendText("\n");
					}
				}
				txtLog.AppendText("\nDone.\n");
				txtLog.AppendText(x + " file(s) with extension " + txtOriginalExtension.Text + " were copied with the extension " +
								  txtNewExtension.Text + "\n");
			}
		}
	}
}
