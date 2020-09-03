using com.sun.org.apache.regexp.@internal;
using javax.xml.crypto;
using RaonShortchaser_UI.Control;
using RaonShortchaser_UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.Server;

namespace RaonShortchaser_UI
{




    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DataViewModel dataviewmodel;

        List<DutControl> duts = new List<DutControl>();

        private delegate int Returndutid(ProejctInfo proejct);

        List<DutInfolist> dutInfolists;
        public MainWindow()
        {
            dataviewmodel = new DataViewModel();
            //this.DataContext = dataviewmodel;
            InitializeComponent();

            
        }
        ProejctInfo project;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            project = new ProejctInfo();
            project = makeRandom();
            project.ProjectUpdate();//project자체에는 setter를 할수가없어서 비슷한 함수를만들었다
            dutInfolists = new List<DutInfolist>();
            DutlistaddEvent(project, dutInfolists);

            this.DataContext = project;
        }

        private void UpdateStatus(object sender, ProgressEventArgs e)
        {
            int nowSelectedDut=(sender as DutInfolist).Dutid ;
            int nowSelectedIndex = (sender as DutInfolist).SelectedIndex ;


            //선택된 하나의 combobox만 on 나머지 off
            if (dutInfolists != null)
            {
                List<DutInfolist> returnlist = new List<DutInfolist>();
                foreach (DutInfolist dutlist in dutInfolists)
                {
                    if (dutlist.Dutid == nowSelectedDut)
                    {
                        dutlist.SettingIndex(nowSelectedIndex);
                    }
                    else
                    {
                        dutlist.SettingIndex(dutlist.ItemsSource.Count-1);
                    }
                    returnlist.Add(dutlist);
                }
                dutInfolists = returnlist;
            }

            List<PinInfo> pins=dutInfolists[nowSelectedDut].dutinfolist[nowSelectedIndex].pins;
            List<MainDataGrid_class> displayclass = new List<MainDataGrid_class>();
            if(pins.Count>0 && pins.Count <= 500)
            {
                MainDataGrid_class openvalue = new MainDataGrid_class();
                foreach (PinInfo pin in pins)
                {
                    
                    if (pin.id / 100 == 0)
                    {
                        openvalue =new MainDataGrid_class();
                        openvalue.setPin(pin);
                        System.Diagnostics.Debug.Print("");
                        displayclass.Add(openvalue);
                    }
                    else
                    {
                        openvalue = displayclass[pin.id % 100];
                        openvalue.setPin(pin);
                        displayclass[pin.id%100] = openvalue;
                    }

                }
                project.Data_Grid_ItemsSource = displayclass;
            }


            System.Diagnostics.Debug.Print("Dut_"+ nowSelectedDut.ToString()+" DutIndex"+nowSelectedIndex.ToString()+ " dutlist_Selection_Changed");
        }
        private void DutlistaddEvent(ProejctInfo project, List<DutInfolist> dutinfolists)
        {
            dutinfolists.Add(project.sect_1.dutlist_1);
            dutinfolists.Add(project.sect_1.dutlist_2);
            dutinfolists.Add(project.sect_1.dutlist_3);
            dutinfolists.Add(project.sect_1.dutlist_4);

            dutinfolists.Add(project.sect_2.dutlist_1);
            dutinfolists.Add(project.sect_2.dutlist_2);
            dutinfolists.Add(project.sect_2.dutlist_3);
            dutinfolists.Add(project.sect_2.dutlist_4);

            dutinfolists.Add(project.sect_3.dutlist_1);
            dutinfolists.Add(project.sect_3.dutlist_2);
            dutinfolists.Add(project.sect_3.dutlist_3);
            dutinfolists.Add(project.sect_3.dutlist_4);

            dutinfolists.Add(project.sect_4.dutlist_1);
            dutinfolists.Add(project.sect_4.dutlist_2);
            dutinfolists.Add(project.sect_4.dutlist_3);
            dutinfolists.Add(project.sect_4.dutlist_4);

            dutinfolists.Add(project.sect_5.dutlist_1);
            dutinfolists.Add(project.sect_5.dutlist_2);
            dutinfolists.Add(project.sect_5.dutlist_3);
            dutinfolists.Add(project.sect_5.dutlist_4);

            foreach(DutInfolist dutInfo in dutinfolists)
            {
                dutInfo.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            }
            //project.sect_1.dutlist_1.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_1.dutlist_2.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_1.dutlist_3.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_1.dutlist_4.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_2.dutlist_1.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_2.dutlist_2.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_2.dutlist_3.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_2.dutlist_4.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_3.dutlist_1.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_3.dutlist_2.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_3.dutlist_3.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_3.dutlist_4.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_4.dutlist_1.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_4.dutlist_2.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_4.dutlist_3.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_4.dutlist_4.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_5.dutlist_1.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_5.dutlist_2.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_5.dutlist_3.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);
            //project.sect_5.dutlist_4.OnUpdateStatus += new DutInfolist.StatusUpdatehadler(UpdateStatus);

        }

        public ProejctInfo makeRandom()
        {
            ProejctInfo ret = new ProejctInfo();
            Random rd = new Random();
            List<DutInfo> dutlist = new List<DutInfo>();
            for (int j = 0; j < 20; j++)
            {
                DutInfo dut = new DutInfo();
                List<PinInfo> pinlist = new List<PinInfo>();
                for (int i = 0; i < 500; i++)
                {

                    pinlist.Add(new PinInfo(i, rd.Next(0,1000)));
                }
                dut.setDut(j, pinlist);
                dutlist.Add(dut);
            }
            foreach(DutInfo di in dutlist)
            {
                int div = di.id / 4;
                int rest = di.id % 4;
                makeRandomProject(ret, div, rest, di);
            }
            return ret;
        }
        public void makeRandomProject(ProejctInfo projectInfo, int div, int rest, DutInfo dutinfo)
        {
            switch (div)
            {
                case 0:
                    switch (rest)
                    {
                        case 0: projectInfo.sect_1.dutlist_1.dutinfolist.Add(dutinfo); break;
                        case 1: projectInfo.sect_1.dutlist_2.dutinfolist.Add(dutinfo); break;
                        case 2: projectInfo.sect_1.dutlist_3.dutinfolist.Add(dutinfo); break;
                        case 3: projectInfo.sect_1.dutlist_4.dutinfolist.Add(dutinfo); break;
                    }break;
                case 1:
                    switch (rest)
                    {
                        case 0: projectInfo.sect_2.dutlist_1.dutinfolist.Add(dutinfo); break;
                        case 1: projectInfo.sect_2.dutlist_2.dutinfolist.Add(dutinfo); break;
                        case 2: projectInfo.sect_2.dutlist_3.dutinfolist.Add(dutinfo); break;
                        case 3: projectInfo.sect_2.dutlist_4.dutinfolist.Add(dutinfo); break;
                    }
                    break;
                case 2:
                    switch (rest)
                    {
                        case 0: projectInfo.sect_3.dutlist_1.dutinfolist.Add(dutinfo); break;
                        case 1: projectInfo.sect_3.dutlist_2.dutinfolist.Add(dutinfo); break;
                        case 2: projectInfo.sect_3.dutlist_3.dutinfolist.Add(dutinfo); break;
                        case 3: projectInfo.sect_3.dutlist_4.dutinfolist.Add(dutinfo); break;
                    }
                    break;
                case 3:
                    switch (rest)
                    {
                        case 0: projectInfo.sect_4.dutlist_1.dutinfolist.Add(dutinfo); break;
                        case 1: projectInfo.sect_4.dutlist_2.dutinfolist.Add(dutinfo); break;
                        case 2: projectInfo.sect_4.dutlist_3.dutinfolist.Add(dutinfo); break;
                        case 3: projectInfo.sect_4.dutlist_4.dutinfolist.Add(dutinfo); break;
                    }
                    break;
                case 4:
                    switch (rest)
                    {
                        case 0: projectInfo.sect_5.dutlist_1.dutinfolist.Add(dutinfo); break;
                        case 1: projectInfo.sect_5.dutlist_2.dutinfolist.Add(dutinfo); break;
                        case 2: projectInfo.sect_5.dutlist_3.dutinfolist.Add(dutinfo); break;
                        case 3: projectInfo.sect_5.dutlist_4.dutinfolist.Add(dutinfo); break;
                    }
                    break;

            }
        }
        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ProejctInfo project = this.DataContext as ProejctInfo;
            if (project != null)
            {
                System.Diagnostics.Debug.Print("Window_DataContextChanged success");
                //project.doUpdateProperty();
            }
        }











        private void cbPath_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        string ProjectDir = "./";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = ProjectDir;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    cbPath.Items.Add(dialog.SelectedPath);
                    cbPath.SelectedIndex = cbPath.Items.Count - 1;
                }
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //double ret=0;
            //object test=Datagrid.SelectedCells[0].Item;
            //int index=((sender as DataGrid).CurrentColumn as DataGridTextColumn).DisplayIndex;
            //MainDataGrid_class nowselectedline = (sender as DataGrid).CurrentItem as MainDataGrid_class;
            //switch(index)
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        ret = nowselectedline.Measures_Row1;
            //        nowselectedline.getNum();
            //        break;
            //    case 2:
            //        ret = nowselectedline.Measures_Row2;break;
            //    case 3:
            //        ret = nowselectedline.Measures_Row3;break;
            //    case 4:
            //        ret = nowselectedline.Measures_Row4;break;
            //    case 5:
            //        ret = nowselectedline.Measures_Row5;break;
            //    default:
            //         break;
            //}
            //dataviewmodel.setSelectedItem_Binding(ret);


        }
        


        public void dut_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ////Dut의 리스트 선택시 뜨는 Main Grid 변경 
            //List<Pin_Value> PinList=((sender as DataGrid).CurrentItem as Dut).GetPinList();
            //ChangeMainGrid(PinList);
        }

        private void Save_Pr_Button(object sender, RoutedEventArgs e)
        {
            dataviewmodel.ProjectSave();
        }
        
       


    }
}
