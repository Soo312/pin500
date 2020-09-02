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

        public MainWindow()
        {
            dataviewmodel = new DataViewModel();
            this.DataContext = dataviewmodel;
            InitializeComponent();

            DutDefinition();

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProejctInfo project = new ProejctInfo();
            //project.sect_1.dutlist_1.ItmesSource.Columns.Add("Key", typeof(string));
            //project.sect_1.dutlist_1.ItmesSource.Columns.Add("Value", typeof(string));

            //project.sect_1.dutlist_1.ItmesSource.Rows.Add(new string[] { "1", "a" });
            //project.sect_1.dutlist_1.ItmesSource.Rows.Add(new string[] { "2", "b" });

            project = makeRandom();
            project.ProjectUpdate();//project자체에는 setter를 할수가없어서 비슷한 함수를만들었다


            this.DataContext = project;
        }


        public ProejctInfo makeRandom()
        {
            ProejctInfo ret = new ProejctInfo();

            List<DutInfo> dutlist = new List<DutInfo>();
            for (int j = 0; j < 20; j++)
            {
                DutInfo dut = new DutInfo();
                List<PinInfo> pinlist = new List<PinInfo>();
                for (int i = 0; i < 500; i++)
                {

                    pinlist.Add(new PinInfo(i, new Random().Next()));
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
        
        private void ChangeMainGrid(List<Pin_Value> pinList)
        {
            dataviewmodel.Binding_DataGrid = ChangePinList_To_MainDataGrid(pinList);//Pin_Value 를 출력용 MainDataGrid_class로 바꿔야함
        }
        
        public List<MainDataGrid_class> ChangePinList_To_MainDataGrid(List<Pin_Value> pin_value)
        {
            List<MainDataGrid_class> ret= new List<MainDataGrid_class>();
            MainDataGrid_class willdelete=new MainDataGrid_class();
            foreach(Pin_Value pinlist in pin_value)
            {
                willdelete.setKey_Value(pinlist);
                if(pinlist.getKey()/100 >1)
                {
                    willdelete = ret[pinlist.getKey() % 100];
                    willdelete.setMeasures(pinlist);
                }
                else
                {
                    willdelete.setMeasures(new Pin_Value(pinlist.getKey(), pinlist.getValue()));

                }
                ret.Add(willdelete);
            }


            return ret;
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
        
        private void DutDefinition()
        {
            //duts.Add(dut00);
            //duts.Add(dut01);
            //duts.Add(dut02);
            //duts.Add(dut03);

            //duts.Add(dut10);
            //duts.Add(dut11);
            //duts.Add(dut12);
            //duts.Add(dut13);

            //duts.Add(dut20);
            //duts.Add(dut21);
            //duts.Add(dut22);
            //duts.Add(dut23);

            //duts.Add(dut30);
            //duts.Add(dut31);
            //duts.Add(dut32);
            //duts.Add(dut33);

            //duts.Add(dut40);
            //duts.Add(dut41);
            //duts.Add(dut42);
            //duts.Add(dut43);

            foreach(DutControl dut in duts)
            {

            }

        }

        public DutClass test(int num)
        {
            DutClass ret=new DutClass();
            List<Dut> listdut = new List<Dut>();

            listdut.Add(new Dut(num,retpin()));

            ret.setDutList(listdut);
            return ret;
        }
        public List<Pin_Value> retpin()
        {
            List<Pin_Value> pinvalue = new List<Pin_Value>();
            Random rd = new Random();
            for (int i = 0; i < 500; i++)
            {
                pinvalue.Add(new Pin_Value(i, rd.Next()));
            }
            return pinvalue;
        }

    }
}
