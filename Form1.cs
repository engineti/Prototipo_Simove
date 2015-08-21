using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UHFAPI_NET;

namespace DemoObras
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// main class
        /// </summary>
        public static UHFAPI_NET.UHFAPI_NET R900APP;
        public static bool R900_alive = false;
        public static FormInventory formInventory;
        public static FormAccess formAccess; // han 2011.2.7
        public static FormLink formLink; // han 2011.2.12

        public Form1()
        {
            InitializeComponent();

            //this.WindowState = FormWindowState.Maximized;

            label3.Text = "Off Line";
            label3.ForeColor = Color.Red;

            // show this form
            Show();
            Application.DoEvents();

            FormDialog formDlg = new FormDialog();
            formDlg.Message = "Please Wait ...";
            formDlg.Show();
            Application.DoEvents();

            R900APP = new UHFAPI_NET.UHFAPI_NET();

            // link R900 for control
            LinkReader();

            if (R900_alive)
            {
                label3.Text = "On Line";
                label3.ForeColor = Color.Green;

                
            }

            formDlg.Close();
            Application.DoEvents();

            // han 2011.2.7 // invoke inventory window
            // han 2011.2.7 formInventory = new FormInventory();
            // han 2011.2.7 formInventory.ShowDialog();

            // han 2011.2.7 DislinkReader();
            // Application.Exit();
        }

        public void LinkReader()
        {
            // 결과?�받??window handle ???�려 줍니??
            R900APP.UHFAPI_HWND_EX(0, 0);

            // ?�신 채널???�니??
            if (R900APP.UHFAPI_Open())
                R900_alive = true;
            

            // message handler ???���??�다.
            R900APP.evtR900TriggerEvent += new UHFAPI_NET.UHFAPI_NET.R900TriggerHandler(R900TriggerHandler);
            R900APP.evtCmdBegin += new UHFAPI_NET.UHFAPI_NET.BeginEventDispacher(R900CommandBegin);
            R900APP.evtCmdEnd += new UHFAPI_NET.UHFAPI_NET.EndEventDispacher(R900CommandEnd);
            R900APP.evtLinkLost += new UHFAPI_NET.UHFAPI_NET.LinkLostEventHandler(R900APP_evtLinkLost);
            R900APP.evtPlatformPowerResume += new UHFAPI_NET.UHFAPI_NET.PlatformPowerResumeEventHandler(R900APP_evtPlatformPowerResume);
        }

        void R900APP_evtPlatformPowerResume()
        {
            R900_alive = false;

            label3.Text = "Off Line";
            label3.ForeColor = Color.Red;

            R900APP.UHFAPI_Close();

            if (formInventory != null)
                formInventory.PowerResume();
            if (formAccess != null)
                formAccess.PowerResume();
        }

        void R900APP_evtLinkLost()
        {
            R900_alive = false;

            label3.Text = "Off Line";
            label3.ForeColor = Color.Red;

            R900APP.UHFAPI_Close();

            if (formInventory != null)
                formInventory.LinkLost();
            if (formAccess != null)
                formAccess.LinkLost();
        }

        public void DislinkReader()
        {
            // ?�신 채널???�습?�다.
            if (R900_alive || R900APP.UHFAPI_IsOpen())
            {
                R900_alive = false;
                R900APP.UHFAPI_Close();
            }

            R900APP.Cleanup();
        }

        internal void R900TriggerHandler(bool trigger_on)
        {
            if (formInventory != null)
                formInventory.InventoryTrigger(trigger_on);
            else if (formAccess != null)
                formAccess.AccessTrigger(trigger_on);
        }

        internal void R900CommandBegin(string cmd)
        {
        }

        internal void R900CommandEnd(string cmd)
        {
            if (cmd == "Inventory")
                formInventory.InventoryEnd();
            else // han 2011.2.9
                formAccess.AccessEnd();
        }

        private void button1_Click(object sender, EventArgs e) // han 2011.2.7
        {
            // invoke inventory window
            formInventory = new FormInventory();

            formInventory.ShowDialog();

            if (R900APP.UHFAPI_IsOpen())
            {
                R900_alive = true;

                label3.Text = "On Line";
                label3.ForeColor = Color.Green;
            }
            else
            {
                R900_alive = false;

                label3.Text = "Off Line";
                label3.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e) // han 2011.2.7
        {
            // invoke access window
            formAccess = new FormAccess();

            formAccess.ShowDialog();

            if (R900APP.UHFAPI_IsOpen())
            {
                R900_alive = true;

                label3.Text = "On Line";
                label3.ForeColor = Color.Green;
            }
            else
            {
                R900_alive = false;

                label3.Text = "Off Line";
                label3.ForeColor = Color.Red;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DislinkReader();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // dislink first
            if (R900APP.UHFAPI_IsOpen())
                DislinkReader();

            // invoke bind window
            formLink = new FormLink();

            formLink.ShowDialog();

            if (!R900APP.UHFAPI_IsOpen())
            {
                FormDialog formDlg = new FormDialog();
                formDlg.Message = "Please Wait ...";
                formDlg.Show();
                Application.DoEvents();

                // link R900 for control
                LinkReader();

                formDlg.Close();
                Application.DoEvents();
            }

            if (R900APP.UHFAPI_IsOpen())
            {
                R900_alive = true;

                label3.Text = "On Line";
                label3.ForeColor = Color.Green;
            }
            else
            {
                R900_alive = false;

                label3.Text = "Off Line";
                label3.ForeColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormConfigTags formConfigTags = new FormConfigTags();

            formConfigTags.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

    }

}
