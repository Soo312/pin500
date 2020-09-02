using RaonShortchaser_UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RaonShortchaser_UI.Control
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DutControl : UserControl
    {
        DutInfo _DutInfolist = null;

        public DutControl()
        {
            InitializeComponent();
        }

        private void Dutcontrol_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _DutInfolist = DataContext as DutInfo;
            System.Diagnostics.Debug.Print("");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
