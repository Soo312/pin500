using System.Xml.Serialization;
using Microsoft.TeamFoundation.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Microsoft.IdentityModel.Tokens;
using RaonShortchaser_UI.Control;
using Microsoft.TeamFoundation.TestImpact.Client;
using Microsoft.VisualStudio.Services.Common;
using com.sun.xml.@internal.bind.v2.model.core;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using com.sun.jdi;
using java.util;
using System.Collections;
using System.Windows.Forms;
using com.sun.org.apache.bcel.@internal.generic;
using System.Windows.Input;
using Microsoft.TeamFoundation.Build.WebApi;
using java.beans;
using Microsoft.VisualStudio.Services.WebApi;

namespace RaonShortchaser_UI
{

    public class ProgressEventArgs : EventArgs
    {
        public int Status { get; private set; }
        public ProgressEventArgs(int status)
        {
            Status = status;
        }
    }

    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange(string propertyName)
        {
            try
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("BM", "PropertyChange : " + ex.Message);
            }
        }
    }

    public class MyEventArgs : EventArgs
    {
        private string msg;

        public MyEventArgs(string messageData)
        {
            msg = messageData;
        }
        public string Message
        {
            get { return msg; }
            set { msg = value; }
        }

    }



    public class PinInfo : BaseModel
    {
        public int id { get; set; }
        public double reg { get; set; }
        public object TAG;
        public PinInfo() { }
        public PinInfo(int id, double value)
        {
            this.id = id;
            this.reg = value;
        }
    }

    public class DutInfo : BaseModel
    {
        public string Name { get { return "DUT_" + id.ToString(); } }

        public List<PinInfo> pins;
        public int id { get; set; }
        public object TAG;

        public string Time { get; set; }

        public DutInfo()
        {
            pins = new List<PinInfo>();


            for (int i = 0; i < 500; i++)
            {
                pins.Add(new PinInfo());
            }

        }

        public void setDut(int num, List<PinInfo> pinInfos)
        {
            Time = DateTime.Now.ToString("yyMMssddmm");
            id = num;
            this.pins = pinInfos;
        }


        internal void doUpdateProperty()
        {
            OnPropertyChange("Name");
        }
    }

    public class DutInfolist : BaseModel
    {

        public int Dutid { get; set; }


        private List<DutInfo> _dutinfolist = new List<DutInfo>();
        public string Name { get { return "DUT_" + Dutid.ToString(); } }

        private CollectionView _ItemsSource;
        private string _SelectedItem;
        public bool isSelected;

        public string SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (value != null)
                {
                    if (value != "notSelected")
                    {
                        _SelectedItem = value;
                        isSelected = true;


                    }
                    else
                    {
                        isSelected = false;
                    }
                    OnPropertyChange("SelectedItem");
                }
            }


        }
        private int _SelectedIndex;
        public int SelectedIndex
        {
            get
            { return _SelectedIndex; }
            set
            {
                //if(value != _SelectedIndex)
                if (value != _SelectedIndex)
                {
                    _SelectedIndex = value;
                    UpdateStatus(Dutid);
                    OnPropertyChange("SelectedIndex");

                }
                else
                {
                    _SelectedIndex = value;
                    OnPropertyChange("SelectedIndex");
                }
                //else
                //{

                //    OnPropertyChange("SelectedIndex");
                //}

            }
        }

        public void SettingIndex(int num)
        {
            _SelectedIndex = num;
            OnPropertyChange("SelectedIndex");
        }

        public string Time;



        public CollectionView ItemsSource
        {
            get { return _ItemsSource; }
            set
            {
                if (value != null)
                {
                    _ItemsSource = value;
                    Time = DateTime.Now.ToString("yyMMddhhmmss");
                    OnPropertyChange("ItemsSource");
                }
            }

        }


        public List<DutInfo> dutinfolist
        {
            get { return _dutinfolist; }
            set
            { if (value != null)
                {
                    _dutinfolist = value;
                }
            }
        }

        public DutInfolist()
        {

        }


        ReturnID returnId;
        public DutInfolist(int num, List<DutInfo> dutinfolist, ReturnID returnId)
        {
            Dutid = num;
            this.dutinfolist = dutinfolist;

            _SelectedIndex = _dutinfolist.Count + 1;

            //    List<string> testlist = new List<string>();
            //    for (int i = 0; i < 5; i++)
            //    {
            //        //testlist.Add);
            //    }
            //    _ItemsSource = new CollectionView(testlist);
        }

        public delegate void StatusUpdatehadler(object sender, ProgressEventArgs e);
        public event StatusUpdatehadler OnUpdateStatus;


        private void UpdateStatus(int status)
        {
            if (OnUpdateStatus == null) return;

            ProgressEventArgs args = new ProgressEventArgs(status);
            OnUpdateStatus(this, args);
        }


        public void addDutinfolist(DutInfo dutinfo)
        {
            if (dutinfo != null)
            {
                this.dutinfolist.Add(dutinfo);
                System.Diagnostics.Debug.Print("Dutinfo in Dutinfolist Add Dutinfolist");
            }
        }
        public void UpdateDutinfolist()
        {
            int count = _dutinfolist.Count;

            List<string> namelist = new List<string>();
            for (int i = 0; i < count; i++)
            {
                namelist.Add(_dutinfolist[i].id.ToString());
            }
            namelist.Add("notSelected");

            _ItemsSource = new CollectionView(namelist);
        }

        public void setDutinfolist(DutInfolist dutInfolist)
        {
            if (dutinfolist != null)
            {
                this.dutinfolist = dutinfolist;
            }
        }

        public int returnComboboxid()
        {
            if (isSelected)
                return Dutid;
            return 9999;
        }
    }

    public class SectorInfo : BaseModel
    {

        public int sectorid;

        protected DutInfolist _dutlist_1;
        protected DutInfolist _dutlist_2;
        protected DutInfolist _dutlist_3;
        protected DutInfolist _dutlist_4;




        public DutInfo dut_1 { get; set; }
        public DutInfo dut_2 { get; set; }
        public DutInfo dut_3 { get; set; }
        public DutInfo dut_4 { get; set; }
        public object TAG;

        public DutInfolist dutlist_1
        {
            get { return _dutlist_1; }
            set { _dutlist_1 = value; }
        }
        public DutInfolist dutlist_2
        {
            get { return _dutlist_2; }
            set { _dutlist_2 = value; }
        }
        public DutInfolist dutlist_3
        {
            get { return _dutlist_3; }
            set { _dutlist_3 = value; }
        }
        public DutInfolist dutlist_4
        {
            get { return _dutlist_4; }
            set { _dutlist_4 = value; }
        }

        public SectorInfo(int num, ReturnID returnid)
        {
            this.sectorid = num;
            _dutlist_1 = new DutInfolist(sectorid * 4 + 1, null, returnid);
            _dutlist_2 = new DutInfolist(sectorid * 4 + 2, null, returnid);
            _dutlist_3 = new DutInfolist(sectorid * 4 + 3, null, returnid);
            _dutlist_4 = new DutInfolist(sectorid * 4 + 4, null, returnid);

            //dut_1 = new DutInfo();
            //dut_2 = new DutInfo();
            //dut_3 = new DutInfo();
            //dut_4 = new DutInfo();
        }

        public void UpdateSector()
        {
            dutlist_1.UpdateDutinfolist();
            dutlist_2.UpdateDutinfolist();
            dutlist_3.UpdateDutinfolist();
            dutlist_4.UpdateDutinfolist();
        }

        internal void doUpdateProperty()
        {
            OnPropertyChange("dut_1");
            OnPropertyChange("dut_2");
            OnPropertyChange("dut_3");
            OnPropertyChange("dut_4");

            dut_1.doUpdateProperty();
            dut_2.doUpdateProperty();
            dut_3.doUpdateProperty();
            dut_4.doUpdateProperty();
        }
    }

    public class ProejctInfo : BaseModel
    {

        public SectorInfo sect_1 { get; set; }
        public SectorInfo sect_2 { get; set; }
        public SectorInfo sect_3 { get; set; }
        public SectorInfo sect_4 { get; set; }
        public SectorInfo sect_5 { get; set; }

        public ReturnID returnid;

        public object TAG;

        private List<MainDataGrid_class> _ItmesSource;

        public ProejctInfo()
        {
            returnid = new ReturnID();

            sect_1 = new SectorInfo(0, returnid);
            sect_2 = new SectorInfo(1, returnid);
            sect_3 = new SectorInfo(2, returnid);
            sect_4 = new SectorInfo(3, returnid);
            sect_5 = new SectorInfo(4, returnid);

        }
        public void setSector(int num, SectorInfo value)
        {
            switch (num)
            {
                case 1: sect_1 = value; sect_1.sectorid = num; break;
                case 2: sect_2 = value; sect_2.sectorid = num; break;
                case 3: sect_3 = value; sect_3.sectorid = num; break;
                case 4: sect_4 = value; sect_4.sectorid = num; break;
                case 5: sect_5 = value; sect_5.sectorid = num; break;
                default: break;
            }
        }

        public List<MainDataGrid_class> Data_Grid_ItemsSource
        {
            get { return _ItmesSource; }
            set
            {
                if(value != null)
                {
                    _ItmesSource=value;
                    OnPropertyChange("Data_Grid_ItemsSource");
                }
            }
        }


        public void ProjectUpdate()
        {
            sect_1.UpdateSector();
            sect_2.UpdateSector();
            sect_3.UpdateSector();
            sect_4.UpdateSector();
            sect_5.UpdateSector();

        }


        internal void doUpdateProperty()
        {
            OnPropertyChange("sect_1");
            OnPropertyChange("sect_2");
            OnPropertyChange("sect_3");
            OnPropertyChange("sect_4");
            OnPropertyChange("sect_5");

            sect_1.doUpdateProperty();
            sect_2.doUpdateProperty();
            sect_3.doUpdateProperty();
            sect_4.doUpdateProperty();
            sect_5.doUpdateProperty();
        }

        public int returnComboboxid()
        {
            List<SectorInfo> sectorInfos = new List<SectorInfo>();
            sectorInfos.Add(sect_1);
            sectorInfos.Add(sect_2);
            sectorInfos.Add(sect_3);
            sectorInfos.Add(sect_4);
            sectorInfos.Add(sect_5);

            foreach (SectorInfo sec in sectorInfos.ToArray())
            {
                List<DutInfolist> duts = new List<DutInfolist>();
                duts.Add(sec.dutlist_1);
                duts.Add(sec.dutlist_2);
                duts.Add(sec.dutlist_3);
                duts.Add(sec.dutlist_4);
                foreach (DutInfolist dutinfo in duts)
                {
                    if (dutinfo.isSelected == true)
                        return dutinfo.Dutid;
                }
            }
            return 9999;
        }

    }

    public class ReturnID : INotifyPropertyChanged
    {
        private int _setDutid;
        private List<PinInfo> pins = new List<PinInfo>();


        public int getReturnID()
        {
            return _setDutid;
        }

        public int setDutid
        {
            get { return _setDutid; }
            set
            {
                if (value != _setDutid)
                {
                    _setDutid = value;
                    INotifyPropertyChanged("setDutid");
                }
            }
        }
        private void INotifyPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void setPins(List<PinInfo> value)
        {
            this.pins = value;
        }
        public void setReturnID(int num)
        {
            setDutid = num;
        }
    }








    public class DataViewModel : INotifyPropertyChanged
    {

        public DataViewModel()
        {

            dutlist = new List<DutInfo>();
            DutInfo dutinfo = new DutInfo();
            dutlist.Add(dutinfo);
        }

        public List<DutInfo> dutlist { get; set; }

        
        private List<MainDataGrid_class> _Data_Grid_ItemsSource;
        [XmlElement("Project")] private ProejctInfo project = new ProejctInfo();
        public void ProjectSave()
        {
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter(@".\project.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ProejctInfo));
                xs.Serialize(wr, project);
            }
        }
        public List<MainDataGrid_class> Data_Grid_ItemsSource
        {
            //입력만 받는거
            get
            {
                return _Data_Grid_ItemsSource;
            }
            set
            {
                if (value != null)
                {
                    _Data_Grid_ItemsSource = value;
                    OnPropertyChanged("Data_Grid_ItemsSource");
                }
            }
        }
        private double _SelectedItem;
        public double SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    lbResistance = value.ToString(); ;
                }

            }
        }
        private string _lbPin;
        public string lbPin
        {
            get
            {
                return _lbPin;
            }
            set
            {
                if (value != null)
                    _lbPin = value;
            }
        }

        private string _lbResistance;
        public string lbResistance
        {
            get
            {
                return _lbResistance;
            }
            set
            {
                if (value != null)
                {
                    _lbResistance = value;
                    OnPropertyChanged("lbResistance");
                }
            }


        }
        public void setSelectedItem_Binding(double selected)
        {
            if (!(selected == 0))
            {
                SelectedItem = selected;
            }
            else
            {
                SelectedItem = 0;
            }
        }


 
        //public List<DutClass> test()
        //{
        //    List<DutClass> ret = new List<DutClass>();
        //    DutClass dutclass = new DutClass();
        //    Dut dut1 = new Dut(1);
        //    Dut dut2 = new Dut(2);
        //    Dut dut3 = new Dut(3);
        //    Dut dut0 = new Dut(0);

        //    for (int i = 0; i < 500; i++)
        //    {
        //        dut0.AddPin(new Pin_Value(i, i - 100));
        //    }
        //    for (int i = 0; i < 500; i++)
        //    {
        //        dut1.AddPin(new Pin_Value(i, i - 200));
        //    }
        //    for (int i = 0; i < 500; i++)
        //    {
        //        dut2.AddPin(new Pin_Value(i, i - 300));
        //    }
        //    for (int i = 0; i < 500; i++)
        //    {
        //        dut3.AddPin(new Pin_Value(i, i - 400));
        //    }
        //    dutclass.AddDut(dut0);
        //    dutclass.AddDut(dut1);
        //    dutclass.AddDut(dut2);
        //    dutclass.AddDut(dut3);

        //    ret.Add(dutclass);



        //    return ret;

        //}

        //private SectorControl _sector0, _sector1, _sector2, _sector3, _sector4;
        //public RaonShortchaser_UI.Control.SectorControl sector0 
        //{
        //    get
        //    {
        //        return _sector0;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            _sector0 = value;
        //            if(value.getDutClasses() != null)
        //                _sector0.setDutClasses(value.getDutClasses());
        //        }
        //    }

        //}
        //public RaonShortchaser_UI.Control.SectorControl sector1
        //{
        //    get
        //    {
        //        return _sector1;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            _sector1 = value;
        //            if (value.getDutClasses() != null)
        //                _sector1.setDutClasses(value.getDutClasses());
        //        }
        //    }

        //}
        //public RaonShortchaser_UI.Control.SectorControl sector2
        //{
        //    get
        //    {
        //        return _sector2;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            _sector2 = value;
        //            if (value.getDutClasses() != null)
        //                _sector2.setDutClasses(value.getDutClasses());
        //        }
        //    }

        //}
        //public RaonShortchaser_UI.Control.SectorControl sector3
        //{
        //    get
        //    {
        //        return _sector3;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            _sector3 = value;
        //            if (value.getDutClasses() != null)
        //                _sector3.setDutClasses(value.getDutClasses());
        //        }
        //    }

        //}
        //public RaonShortchaser_UI.Control.SectorControl sector4
        //{
        //    get
        //    {
        //        return _sector4;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            _sector4 = value;
        //            if (value.getDutClasses() != null)
        //                _sector4.setDutClasses(value.getDutClasses());
        //        }
        //    }

        //}





        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public class MainDataGrid_class //출력 바인딩됨
    {
        [XmlElement("Measures")] private PinInfo key_result;
        private int _Row;
        private double _Row1;
        private double _Row2;
        private double _Row3;
        private double _Row4;
        private double _Row5;
        public MainDataGrid_class()
        {
        }

        public void setPin(PinInfo pin)
        {
            if (pin.id >= 0 && pin.id < 100)
            {
                _Row1 = pin.reg;
                _Row = pin.id % 100;
                return;
            }
            else if (pin.id >= 100 && pin.id < 200)
            {
                _Row2 = pin.reg;
                return;
            }
            else if (pin.id >= 200 && pin.id < 300)
            {
                _Row3 = pin.reg;
                return;
            }
            else if (pin.id >= 300 && pin.id < 400)
            {
                _Row4 = pin.reg;
                return;
            }
            else if (pin.id >= 400 && pin.id < 500)
            {
                _Row5 = pin.reg;
                return;
            }
            else return; 



        }
        public int Row
        {
            get
            {
                return _Row;
            }
            set
            { }
        }
        public double Row1
        {
            get
            {
                return _Row1;
            }
            set { }
        }
        public double Row2
        {
            get
            {
                return _Row2;
            }
            set { }
        }
        public double Row3
        {
            get
            {
                return _Row3;
            }
            set { }
        }
        public double Row4
        {
            get
            {
                return _Row4;
            }
            set { }
        }
        public double Row5
        {
            get
            {
                return _Row5;
            }
            set { }
        }

        public int getNum()
        {
            return key_result.id;
        }
    }

    public class Semicon_DataGrid_class
    {
        //다른 반도체인가 아닌가 식별하는id
        private int id;
        private int Count;
        private string Time;
        public Semicon_DataGrid_class(int i1, DateTime datetime)
        {
            Count = i1;
            Time = datetime.ToString();
        }
    }


}