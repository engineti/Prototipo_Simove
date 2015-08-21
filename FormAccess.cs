using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoObras
{
    public partial class FormAccess : Form
    {
        internal static UHFAPI_NET.UHFAPI_NET.structTAG_OP_PARAM InventoryContolParam;
        internal bool trigger_on = false;
        internal bool force_stop = false;
        internal bool by_trigger = false;
                                        // pc_epc,acs_pwd,kill_pwd,tid,reserved,uii,tid,user
        internal byte [] bankIDlist =      {1,   0,   0,   2,   0,   1,   2,   3}; 
        internal string [] defaultoffset = {"01","02","00","00","00","00","00","00"};
        internal string [] defaultlength = {"7", "2", "2", "2", "1", "1", "1", "1"};

        public FormAccess()
        {
            InitializeComponent();
            // prepare inventory parameter
            //PrepareSingleInventoryParameter();
            //PrepareSingleInventoryParameter_mask();
            //PrepareContinuousInventoryParameter();

            // set event dispacher
            Form1.R900APP.evtAccessEPC += new UHFAPI_NET.UHFAPI_NET.AccessEPCDispacher(ShowAccessEPC);
            Form1.R900APP.evtAccessData += new UHFAPI_NET.UHFAPI_NET.AccessDataDispacher(ShowAccessData);
            Form1.R900APP.evtAccessResult += new UHFAPI_NET.UHFAPI_NET.AccessResultDispacher(ShowAccessResult);

            // setup list view
            if (Form1.R900_alive)
            {
                label3.Text = "On Line";
                label3.ForeColor = Color.Green;
                //button1.Enabled = true;
            }
            else
            {
                label3.Text = "Off Line";
                label3.ForeColor = Color.Red;
                //button1.Enabled = false;
            }

            // clear status line
            label2.Text = "";

            InitAccessParameters();
        }

        internal void InitAccessParameters()
        {
            textBox5.Text = "00000000";

            // init bank select
            comboBox1.SelectedIndex = 0;
            SetSelectBank();

            // init operation
            SetSelectOperation();
        }

        public void PrepareContinuousInventoryParameter()
        {
            // sample parameters for single no slect mask
            InventoryContolParam.single_tag = (int)0;
            InventoryContolParam.QuerySelected = (int)0;
            InventoryContolParam.timeout = 0; // 0 means endless
            InventoryContolParam.mask.mskBits = 0; // do not use mask
        }

        public void PrepareSingleInventoryParameter()
        {
            // sample parameters for single no slect mask
            InventoryContolParam.single_tag = (int)1;
            InventoryContolParam.QuerySelected = (int)0;
            InventoryContolParam.timeout = 0; // 0 means endless
            InventoryContolParam.mask.mskBits = 0; // do not use mask
        }

        public void PrepareSingleInventoryParameter_mask()
        {
            // set mask for 96 bit epc
            // inventory ??epc 값이 3000012c5678f431546d30639c92 ?�로 ?�시??
            // tag ?inventory ?맘는 경우 setting ?
            byte[] mask = { 0x01, 0x2c, 0x56, 0x78, 0xf4, 0x31, 0x54, 0x6d, 0x30, 0x63, 0x9c, 0x92 };

            InventoryContolParam =
                    UHFAPI_NET.UHFAPI_NET.UHFAPI_GetTagOpParam(
                        (byte)UHFAPI_NET.UHFAPI_NET.MEMBANK_CODE.BANK_EPC, 2 * 16, 96, mask);

            // sample parameters for single no slect mask
            InventoryContolParam.single_tag = (int)1;
            InventoryContolParam.QuerySelected = (int)1;
            InventoryContolParam.timeout = 0; // 0 means endless
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // check if reader linked
            force_stop = false;
            while ( !force_stop && (Form1.R900APP.UHFAPI_IsOpen() == false) )
            {
                label2.Text = "linking device";
                Application.DoEvents();

                if (Form1.R900APP.UHFAPI_Open())
                {
                    Form1.R900_alive = true;
                    label3.Text = "On Line";
                    label3.ForeColor = Color.Green;
                }
            }

            if (button1.Text.Equals("ACCESS"))
            {
                label2.Text = "starting access";
                Application.DoEvents();
                force_stop = false;
                by_trigger = false;
                StartAccess();
            }
            else
            {
                label2.Text = "stopping access";
                Application.DoEvents();
                force_stop = true;
                StopAccess();
            }

        }

        public void AccessTrigger(bool trigger)
        {
            // save status
            trigger_on = trigger;
            force_stop = true;

            if (button1.Text.Equals("ACCESS") && trigger)
            {
                by_trigger = true;

                // start access
                StartAccess();

                // disable button
                button1.Enabled = false;
            }
            else if (button1.Text.Equals("STOP") && !trigger)
            {
                // stop access
                StopAccess();

                // enable button
                button1.Enabled = true;
            }
            else if (button1.Text.Equals("ACCESS") )
            {
                // enable button
                button1.Enabled = true;
            }
        }

        internal bool HEX32(out UInt32 number, string hexastr)
        {
            number = 0;
            string str = hexastr.ToLower();
            for (int i = 0 ; i < hexastr.Length ; i++ )
            {
                number <<= 4;
                if ((str[i] >= '0') && (str[i] <= '9'))
                {
                    number += (UInt32)(str[i] - '0');
                }
                else if ((str[i] >= 'a') && (str[i] <= 'f'))
                {
                    number += (UInt32)(str[i] - 'a' + 10);
                }
                else
                    return false;
            }
            return true;
        }

        internal bool HEXSTR(out ushort[] buff, string hexastr)
        {
            string str = hexastr.ToLower();
            string str4;
            buff = new ushort [(hexastr.Length+3)/4];
            for (int i = 0; i < hexastr.Length; )
            {
                UInt32 val;
                buff[i / 4] = 0;
                str4 = str.Substring(i, 4);
                if (!HEX32(out val, str4))
                {
                    return false;
                }
                buff[i / 4] = (ushort)val;
                i += 4;
            }
            return true;
        }

        internal void StartAccess()
        {
            byte MemBank = bankIDlist[comboBox1.SelectedIndex];
            UInt32 offset = Convert.ToUInt32(textBox2.Text);
            UInt32 length = Convert.ToUInt32(textBox3.Text);
            UInt32 pwdACCESS;

            button1.Text = "STOP";
            force_stop = false;
            label2.Text = "";
            textBox6.Text = "";

            if (false == HEX32(out pwdACCESS, textBox5.Text))
            {
                AccessEnd();
                label2.Text = "access terminated by bad parameter";
                return;
            }

            // prepare inventory parameter
            if (checkBoxContinous.Checked)
                PrepareContinuousInventoryParameter();
            else
                //PrepareSingleInventoryParameter_mask();
                PrepareSingleInventoryParameter();

            // start access
            if (radioButton1.Checked)
            {
                // read access
                Form1.R900APP.UHFAPI_ReadTag(MemBank, offset, length, pwdACCESS, InventoryContolParam, false);
                textBox1.Text = "";
                textBox4.Text = "";
            }
            else if (radioButton2.Checked)
            {
                // write access
                ushort [] szWriteData;
                if (true == HEXSTR(out szWriteData, textBox4.Text))
                {
                    Form1.R900APP.UHFAPI_WriteTag(MemBank, offset, length, ref szWriteData, pwdACCESS, InventoryContolParam, false);
                    textBox1.Text = "";
                    //textBox4.Text = "";
                }
                else
                {
                    AccessEnd();
                    label2.Text = "access terminated by bad parameter";
                }
            }
            else if (radioButton3.Checked)
            {
                // lock access
                //bool kill_pwd;
                //bool access_pwd;
                //bool epc;
                //bool tid;
                //bool user;
                //Form1.R900APP.UHFAPI_LockTag(kill_pwd, access_pwd, epc, tid, user, pwdACCESS, InventoryContolParam, false);
                //textBox1.Text = "";
                //textBox4.Text = "";
            }
            else if (radioButton4.Checked)
            {
                // kill access
                //uint Kill_PWD;
                //Form1.R900APP.UHFAPI_KillTag(Kill_PWD, pwdACCESS, InventoryContolParam, false);
                //textBox1.Text = "";
                //textBox4.Text = "";
            }
            
        }

        internal void StopAccess()
        {
            Form1.R900APP.UHFAPI_Stop();
            AccessEnd();
        }

        public void ShowAccessEPC(string epc)
        {
            textBox1.Text = epc;
        }

        public void ShowAccessData(string data)
        {
            textBox4.Text = data;
        }

        public void ShowAccessResult(string res)
        {
            textBox6.Text = res;
        }

        public void AccessEnd()
        {
            button1.Text = "ACCESS";
            if ( (force_stop && !by_trigger) || (label3.Text != "On Line") )
            {
                label2.Text = "access terminated";
            }
            else
                label2.Text = "access finished";
            by_trigger = false;
        }

        public void LinkLost()
        {
            label3.Text = "Off Line";
            label3.ForeColor = Color.Red;

            if (button1.Text != "ACCESS")
            {
                button1.Text = "ACCESS";
                // enable button
                button1.Enabled = true;
                // clear trigger state
                trigger_on = false;
                by_trigger = false;
                force_stop = false;

                label2.Text = "link lost";
            }

        }

        public void PowerResume()
        {
            label2.Text = "power cycled";
        }

        internal void SetSelectBank()
        {
            // one of fixed locations
            if (comboBox1.SelectedIndex < 4)
            {
                textBox2.Text = defaultoffset[comboBox1.SelectedIndex];
                textBox3.Text = defaultlength[comboBox1.SelectedIndex];
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
            else
            {
                textBox2.Text = defaultoffset[comboBox1.SelectedIndex];
                textBox3.Text = defaultlength[comboBox1.SelectedIndex];
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectBank();
        }


        internal void SetSelectOperation()
        {
            if (radioButton1.Checked) // read
            {
                textBox1.Enabled = false;
                textBox4.Enabled = false;
            }
            else if (radioButton2.Checked) // write
            {
                textBox1.Enabled = false;
                textBox4.Enabled = true;
            }
            else if (radioButton3.Checked)  // lock 
            {
                textBox1.Enabled = false;
                textBox4.Enabled = false;
            }
            else if (radioButton4.Checked) // kill
            {
                textBox1.Enabled = false;
                textBox4.Enabled = false;
            }
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            SetSelectOperation();
        }

        private void FormAccess_FormClosing(object sender, FormClosingEventArgs e)
        {
            force_stop = true;
        }

    }
}