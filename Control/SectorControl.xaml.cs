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
using com.oracle.webservices.@internal.api.message;
using com.sun.xml.@internal.bind.v2.model.core;
using jdk.@internal.dynalink.beans;
using RaonShortchaser_UI;

namespace RaonShortchaser_UI.Control
{
    /// <summary>
    /// SectorControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SectorControl : UserControl
    {
        SectorInfo _SectorInfo = null;

        public SectorControl()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _SectorInfo = DataContext as SectorInfo;
            if (_SectorInfo != null)
            {
                System.Diagnostics.Debug.Print("UserControl_DataContextChanged success");
            }
        }
    }
}
