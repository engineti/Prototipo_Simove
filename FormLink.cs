using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace Prototipo
{
    public partial class FormLink : Form
    {
        internal devicectrl DevCtrl = null;
        internal Thread SearchThread = null;
        FormLink thisForm = null;

        public FormLink()
        {
            InitializeComponent();

            // remember for search thread
            thisForm = this;
            DevCtrl = new devicectrl(this);

            // open bluetooth manager
            DevCtrl.m_hBt = DevCtrl.BthCreate();

            DevCtrl.varProc = threadSearchProcCallback;

            // list active comports
            selectedport = (int)DevCtrl.DevGetDefaultPort();
            ListActiveComports();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchThread = new Thread(DevCtrl.threadSearchProc);
            SearchThread.Start();
        }

        private void buttonBind_Click(object sender, EventArgs e)
        {
            BindDevice();
        }

        private void comboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxFreeComs.Enabled = true;
            buttonBind.Enabled = true;
        }

        public void threadSearchProcCallback(int reason)
        {
            switch (reason)
            {
                case 1:
                    buttonSearch.Enabled = false;
                    buttonBind.Enabled = false;
                    comboBoxFreeComs.Enabled = false;
                    comboBoxDevices.Enabled = false;
                    comboBoxDevices.Items.Clear();
                    break;
                case 2:
                    buttonSearch.Enabled = true;
                    break;
                case 3:
                    RegisterBthDevices();
                    break;
                case 4:
                    ListBluetoothDevices();
                    break;
                case 5:
                    unsafe
                    {
                        char* ptr = DevCtrl.pScanDeviceInfo->szDeviceNameAddr;
                        {
                            comboBoxDevices.Items.Add(new string(ptr));
                            comboBoxDevices.SelectedIndex = comboBoxDevices.Items.Count - 1;
                        }
                    }
                    break;
            }
                   
        }

        unsafe uint RegisterBthDevices()
        {
            devicectrl.DeviceInfo[] PeerDevicesInfo;

            int iNumDevices = DevCtrl.BthGetNumDevices(DevCtrl.m_hBt);
            if (iNumDevices == 0)
                return 0;

            Module mod = Assembly.GetExecutingAssembly().GetModules()[0];
            string appPath = Path.GetDirectoryName(mod.FullyQualifiedName) + "\\scanlist.dat";
            
            using (StreamWriter sw = new StreamWriter(appPath))
            {
		        PeerDevicesInfo = new devicectrl.DeviceInfo[iNumDevices];

                DevCtrl.BthGetDeviceInfo(DevCtrl.m_hBt, PeerDevicesInfo);

		        for(int iCount = 0; iCount < iNumDevices;iCount++)
		        {

                    char[] outstr = new char[devicectrl.MAX_NAME_SIZE];

                    fixed (char* addr = PeerDevicesInfo[iCount].szDeviceNameAddr)
                    {
                        string id = new string(addr);
                        sw.WriteLine(id);
                    }
		        }
	        }

            return (uint)iNumDevices;
        }

        internal int activeports = 0;
        internal devicectrl.COMInfo[] COMInfoList;
        internal devicectrl.DeviceInfo[] DeviceInfoList;
        internal int selectedport = 0;

        unsafe uint ListBluetoothDevices()
        {
	        int iNumDevices=0;

	        // reset device list
            comboBoxDevices.Items.Clear();

            iNumDevices = DevCtrl.BthGetNumDevices(DevCtrl.m_hBt);

            if (iNumDevices == 0)
                return 0;

	        {
                DeviceInfoList = new devicectrl.DeviceInfo[iNumDevices];

                DevCtrl.BthGetDeviceInfo(DevCtrl.m_hBt, DeviceInfoList);

		        for(int iCount = 0; iCount < iNumDevices;iCount++)
		        {
                    fixed (char* ptr = DeviceInfoList[iCount].szDeviceNameAddr)
                    {
                        comboBoxDevices.Items.Add(new string(ptr));
                        comboBoxDevices.SelectedIndex = comboBoxDevices.Items.Count - 1;
                    }
		        }
	        }
            comboBoxDevices.SelectedIndex = 0;

	        //
            if (DevCtrl.BthEnumCOMPorts(DevCtrl.m_hBt))
	        {
                activeports = (int)DevCtrl.BthGetNumCOMs(DevCtrl.m_hBt);

                COMInfoList = new devicectrl.COMInfo[activeports];

                DevCtrl.BthGetCOMInfo(DevCtrl.m_hBt, COMInfoList);

	        }

            comboBoxFreeComs.Items.Clear();
	        string COMPORT = new string(new char [10]);

            for ( int i = 1 ; i < 10 ; i++ )
	        {
		        int j;

		        COMPORT = "COM" + i + ":";

		        for ( j = 0 ; j < activeports ; j++ )
		        {
                    fixed (char* ptr = COMInfoList[j].Name)
                    {
                        if (COMPORT == new string(ptr)) // match
                            break;
                    }
		        }

		        if ( j >= activeports ) // not found
		        {
			        //int index;
                    comboBoxFreeComs.Items.Add(COMPORT);
                    comboBoxFreeComs.SelectedIndex = comboBoxFreeComs.Items.IndexOf(COMPORT);
		        }
	        }
            comboBoxFreeComs.SelectedIndex = 0; 

	        comboBoxDevices.Enabled = true;
	        comboBoxFreeComs.Enabled = true;
	        buttonBind.Enabled = true;
	        buttonSearch.Enabled = true;

	        return 0;
        }

        unsafe uint ListActiveComports()
        {
            //int iNumDevices = 0;

            // scan active comports
            if (DevCtrl.BthEnumCOMPorts(DevCtrl.m_hBt))
            {
                activeports = (int)DevCtrl.BthGetNumCOMs(DevCtrl.m_hBt);

                COMInfoList = new devicectrl.COMInfo[activeports];

                DevCtrl.BthGetCOMInfo(DevCtrl.m_hBt, COMInfoList);

            }

            comboBoxComports.Items.Clear();
            string COMPORT = new string(new char[100]);

            if ( selectedport != 0 )
                COMPORT = "COM" + selectedport + ":";

            bool found = false;
            for (int i = 0; i < activeports; i++)
            {
                fixed (char* name = COMInfoList[i].Name, nname = COMInfoList[i].NickName)
                {
                    string sname = "";
                    string snname = ""; 

                    for ( int j = 0 ; ; j++ )
                    {
                        if (name[j] == 0)
                            break;
                        sname += name[j];
            }

                    for (int j = 0; ; j++)
                    {
                        if (nname[j] == 0)
                            break;
                        snname += nname[j];
                    }
                    comboBoxComports.Items.Add(sname + "<" + snname + ">");

                    if ((selectedport != 0) && (COMPORT == new string(name))) // match
                    {
                        COMPORT = sname + "<" + snname + ">";
                        found = true;
                    }
                }
            }
            if (!found) // not found
            {
                comboBoxComports.SelectedIndex = 0;
                if (selectedport != 0)
                {
                    comboBoxComports.Items.Add(COMPORT);
                    comboBoxComports.SelectedIndex = comboBoxComports.Items.IndexOf(COMPORT);
                }
            }
            else
            {
                comboBoxComports.SelectedIndex = comboBoxComports.Items.IndexOf(COMPORT);
            }

            comboBoxComports.Enabled = true;

            return 0;
        }

        byte GetHexByte(string hex2)
        {
            byte bval = 0;

            string str = hex2.ToUpper();

            if ((str[0] >= '0') && (str[0] <= '9'))
                bval = (byte)(str[0] - '0');
            else if ( (str[0] >= 'A') && (str[0] <= 'F'))
                bval = (byte)(str[0] - 'A' + 10);
            else
                bval = 0;

            bval <<= 4;

            if ((str[1] >= '0') && (str[1] <= '9'))
                bval += (byte)(str[1] - '0');
            else if ((str[1] >= 'A') && (str[1] <= 'F'))
                bval += (byte)(str[1] - 'A' + 10);

            return bval;
        }

        UInt64 GetBID(string name_id)
        {
            UInt64 lval = 0;

            string bid = name_id.Substring(name_id.LastIndexOf('<')+1, 18-1);
            
            lval = GetHexByte(bid.Substring(0,2).ToUpper());
            lval = (lval << 8) + GetHexByte(bid.Substring(3,2));
            lval = (lval << 8) + GetHexByte(bid.Substring(6,2));
            lval = (lval << 8) + GetHexByte(bid.Substring(9,2));
            lval = (lval << 8) + GetHexByte(bid.Substring(12,2));
            lval = (lval << 8) + GetHexByte(bid.Substring(15,2));

            return lval;
        }

        int GetComPortNo(string comport)
        {
            string str = comport.ToUpper();
            int start = str.IndexOf("COM") + 3; 
            int port = 0;

            while ( (str[start] >= '0' ) && (str[start] <= '9') )
            {
                port = port * 10 + str[start] - '0';
                start++;
            }

            return port;
        }
    
        internal void BindDevice()
        {
            UInt64 device =  GetBID(comboBoxDevices.SelectedItem.ToString());// .SelectedValue.ToString();

	        UInt32 portno = (UInt32)GetComPortNo(comboBoxFreeComs.SelectedItem.ToString());

            UInt32 channel = 1;// RFCOMM_CHANNEL_SPP;

            bool pass = true;

	        while (true)
	        {
                Cursor.Current = Cursors.WaitCursor;

                IntPtr hDevice = DevCtrl.BthConnectComPort(DevCtrl.m_hBt, true, device, channel, portno, 0);

                Cursor.Current = Cursors.Default;

		        if ( hDevice.ToInt32() == 0 )
		        {
			        UInt32 index;
			        IntPtr h;

                    unsafe
                    {
                        if (!DevCtrl.BthQueryComPort(DevCtrl.m_hBt, &index, &h))
                        {
                            myMessageBox("BIND FAIL", "BIND", MessageBoxButtons.OK);
                            pass = false;
                            break;
                        }

                        if ( DialogResult.No == myMessageBox("Bth Device is binded to COM" + index + ":, reconnect ?", "BIND", MessageBoxButtons.YesNo))
                            break;

                        DevCtrl.BthDisconnectComPort(DevCtrl.m_hBt, h);
                    }
		        }
		        else
		        {
                    selectedport = (int)portno;
                    DevCtrl.DevSetDefaultPort((uint)selectedport);
                    ListActiveComports();

                    myMessageBox("BIND OK", "BIND", MessageBoxButtons.OK);
			        break;
		        }
	        }

            if (pass)
            {
                comboBoxFreeComs.Enabled = false;
                buttonBind.Enabled = false;
            }

            comboBoxDevices.Enabled = true;
        }

        public DialogResult myMessageBox(string message, string caption, MessageBoxButtons buttons)
        {
           return MessageBox.Show(message, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        private void buttonLink_Click(object sender, EventArgs e)
        {
            selectedport = GetComPortNo(comboBoxComports.SelectedItem.ToString());
            DevCtrl.DevSetDefaultPort((uint)selectedport);
        }

    }

    public class devicectrl
    {
        #region imports

        //should not be unsafe unsafe public delegate Int32 DeviceDispacher(DeviceInfo* info);
        public delegate Int32 DeviceDispacher(IntPtr pDeviceInfo);//DeviceInfo* info);

        [DllImport("devicectrl.dll", EntryPoint = "BthCreate", SetLastError = true)]
        internal static extern IntPtr _BthCreate();
        [DllImport("devicectrl.dll", EntryPoint = "BthDiscoverDevices", SetLastError = true)]
        internal static extern Int32 _BthDiscoverDevices(IntPtr bh, DeviceDispacher dispacher);
        [DllImport("devicectrl.dll", EntryPoint = "BthGetNumDevices", SetLastError = true)]
        internal static extern Int32 _BthGetNumDevices(IntPtr bh);
        [DllImport("devicectrl.dll", EntryPoint = "BthGetDeviceInfo", SetLastError = true)]
        unsafe internal static extern Int32 _BthGetDeviceInfo(IntPtr bh, DeviceInfo* Info);
        [DllImport("devicectrl.dll", EntryPoint = "BthEnumCOMPorts", SetLastError = true)]
        unsafe internal static extern Int32 _BthEnumCOMPorts(IntPtr bh);
        [DllImport("devicectrl.dll", EntryPoint = "BthGetNumCOMs", SetLastError = true)]
        unsafe internal static extern UInt32 _BthGetNumCOMs(IntPtr bh);
        [DllImport("devicectrl.dll", EntryPoint = "BthGetCOMInfo", SetLastError = true)]
        //unsafe internal static extern Int32 _BthGetCOMInfo(IntPtr bh, COMInfo* info);
        unsafe internal static extern Int32 _BthGetCOMInfo(IntPtr bh, byte* info);
        [DllImport("devicectrl.dll", EntryPoint = "BthConnectComPort", SetLastError = true)]
        unsafe internal static extern IntPtr _BthConnectComPort(IntPtr bh, Int32 client, UInt64/*BT_ADDR*/ device, UInt32* channel, UInt32 index, UInt32 stream_size);
        [DllImport("devicectrl.dll", EntryPoint = "BthQueryComPort", SetLastError = true)]
        unsafe internal static extern UInt32 _BthQueryComPort(IntPtr bh, UInt32* index, IntPtr *h);
        [DllImport("devicectrl.dll", EntryPoint = "BthDisconnectComPort", SetLastError = true)]
        unsafe internal static extern UInt32 _BthDisconnectComPort(IntPtr bh, IntPtr h);
        [DllImport("devicectrl.dll", EntryPoint = "DevSetDefaultPort", SetLastError = true)]
        unsafe internal static extern UInt32 _DevSetDefaultPort(UInt32 index);
        [DllImport("devicectrl.dll", EntryPoint = "DevGetDefaultPort", SetLastError = true)]
        unsafe internal static extern UInt32 _DevGetDefaultPort();

        #endregion

        #region definitions

        public const int MAX_NAME_SIZE = 128;

        public struct DeviceInfo
        {
            unsafe public Int64* /*BT_ADDR*/ bt_addr;
            unsafe public fixed char szDeviceNameAddr[MAX_NAME_SIZE];
            unsafe public fixed char szDeviceName[MAX_NAME_SIZE];
            unsafe public fixed char szDeviceAddr[MAX_NAME_SIZE];
        };

        public struct COMInfo
        {
            unsafe public fixed char Name[10];
            unsafe public fixed char NickName[100];
        };

        public delegate void threadSearchCallback(int reason);

        #endregion

        #region dll adaption

        unsafe public IntPtr BthCreate()
        {
            return _BthCreate();
        }

        unsafe public Int32 BthDiscoverDevices(IntPtr bh, DeviceDispacher dispacher)
        {
            return _BthDiscoverDevices(bh, dispacher);
        }

        unsafe public Int32 BthGetNumDevices(IntPtr bh)
        {
            return _BthGetNumDevices(bh);
        }

        unsafe public bool BthGetDeviceInfo(IntPtr bh, DeviceInfo[] Info)
        {
            byte[] Buff = new byte[Info.Length * sizeof(DeviceInfo)];
            fixed (byte* pBuff = Buff)
            {
                byte* tptr;

                if (_BthGetDeviceInfo(bh, (DeviceInfo*)pBuff) != 0)
                    return false;

                tptr = pBuff;
                for (int i = 0; i < Info.Length; i++)
                {
                    Info[i] = *(DeviceInfo*)tptr;
                    tptr = tptr + sizeof(DeviceInfo);
                }
            }
            return true;
        }

        unsafe public bool BthEnumCOMPorts(IntPtr bh)
        {
            return _BthEnumCOMPorts(bh) != 0;
        }

        unsafe public UInt32 BthGetNumCOMs(IntPtr bh)
        {
            return _BthGetNumCOMs(bh);
        }

        unsafe public bool BthGetCOMInfo(IntPtr bh, COMInfo[] info)
        {
            byte[] Buff = new byte[info.Length * sizeof(COMInfo)];
            fixed (byte* pBuff = Buff)
            {
                byte* tptr;

                if (_BthGetCOMInfo(bh, pBuff) != 0)
                    return false;

                tptr = pBuff;
                for (int i = 0; i < info.Length; i++)
                {
                    info[i] = *(COMInfo*)tptr;
                    tptr = tptr + sizeof(COMInfo);
                }
            }
            return true;
        }

        unsafe public IntPtr BthConnectComPort(IntPtr bh, bool client, UInt64 device, UInt32 channel, UInt32 index, UInt32 stream_size)
        {
            return _BthConnectComPort(bh, client ? 1 : 0, device, &channel, index, stream_size);
        }

        unsafe public bool BthQueryComPort(IntPtr bh, UInt32* index, IntPtr* h)
        {
            return 0 != _BthQueryComPort(bh, index, h);
        }

        unsafe public bool BthDisconnectComPort(IntPtr bh, IntPtr h)
        {
            return 0 != _BthDisconnectComPort(bh, h);
        }

        unsafe public bool DevSetDefaultPort(UInt32 index)
        {
            return 0 != _DevSetDefaultPort(index);
        }

        unsafe public UInt32 DevGetDefaultPort()
        {
            return _DevGetDefaultPort();
        }

        #endregion


        public Form parent;

        public IntPtr m_hBt;

        internal Int32 terminateScan = 0;
        unsafe internal DeviceInfo* pScanDeviceInfo = null;
        public static DeviceDispacher ScanDeviceHandler = null;
        public threadSearchCallback varProc = null;


        public devicectrl(Form thisForm)
        {
            parent = thisForm;
        }

        unsafe public Int32 DeviceHandler(IntPtr pInfo)//DeviceInfo* info)
        {
            object[] args = new object[1];

            pScanDeviceInfo = (DeviceInfo*)pInfo;

            args[0] = 5;
            parent.Invoke(varProc, args);

            if (terminateScan != 0)
                return 0;

            return 1;
        }

        internal void threadSearchProc()
        {
            int iRetVal = 0;
            object[] args = new object[1];

            Cursor.Current = Cursors.WaitCursor;

            args[0] = 1;
            parent.Invoke(varProc, args);

            // search devices
            unsafe
            {
                terminateScan = 0;
                ScanDeviceHandler = DeviceHandler;
                iRetVal = BthDiscoverDevices(m_hBt, ScanDeviceHandler);
            }

            Cursor.Current = Cursors.Default;

            if (iRetVal != 0)
            {
                string szMessage = iRetVal + ": Scanning failed.";
                MessageBox.Show(szMessage);
            }
            else
            {
                args[0] = 3;
                parent.Invoke(varProc, args);

                args[0] = 4;
                parent.Invoke(varProc, args);
            }

            args[0] = 2;
            parent.Invoke(varProc, args);
        }

    }
}