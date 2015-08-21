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
    public partial class FormInventory : Form
    {
        internal static UHFAPI_NET.UHFAPI_NET.structTAG_OP_PARAM InventoryContolParam;
        internal int count = 0; // tag count
        internal bool trigger_on = false;
        internal bool force_stop = false;
        internal bool by_trigger = false;

        public static string tagUsuario;
        public static string tagCapacete;
        public static string tagCinto;
        public static string tagBota;
        public static bool permissaoDeAcesso;

        public static int tempoVerificacaoUsuario;
        public static int tempoVerificacaoEPIs;

        internal static bool leituraCapacete = false;
        internal static bool leituraCinto = false;
        internal static bool leituraBota = false;

        public FormInventory()
        {
            InitializeComponent();

            // prepare inventory parameter
            //PrepareSingleInventoryParameter();
            //PrepareSingleInventoryParameter_mask();
            //PrepareContinuousInventoryParameter();

            // set event dispacher
            Form1.R900APP.evtInventoryEPC += new UHFAPI_NET.UHFAPI_NET.InventoryEPCDispacher(ShowInventoryEPC);

            // setup list view
            if (Form1.R900_alive)
            {
                label3.Text = "On Line";
                label3.ForeColor = Color.Green;
            }
            else
            {
                label3.Text = "Off Line";
                label3.ForeColor = Color.Red;
                
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            }

            // clear status line
            label2.Text = "";

            // upload local inventory data
            if (Form1.R900_alive)
            {
                string str = Form1.R900APP.UHFAPI_GetFirmwareVersion();

                label2.Text = "Uploading local tag information";
                // set event dispacher
                Form1.R900APP.evtUploadEPC += new UHFAPI_NET.UHFAPI_NET.InventoryEPCDispacher(ShowUploadEPC);
                Form1.R900APP.R900LIB_UploadInventory();
                Form1.R900APP.R900LIB_ClearInventory();
                label2.Text = "Upload finished";
            }

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
            byte[] mask = { 0x30, 0x55, 0x31, 0x31, 0x2C, 0x0A, 0x23, 0x00, 0x00, 0x1E, 0xDB, 0x20 };
           
            InventoryContolParam = 
                    UHFAPI_NET.UHFAPI_NET.UHFAPI_GetTagOpParam(
                        (byte)UHFAPI_NET.UHFAPI_NET.MEMBANK_CODE.BANK_EPC, 2 * 16, 96, mask);

            // sample parameters for single no slect mask
            InventoryContolParam.single_tag = (int)1;
            InventoryContolParam.QuerySelected = (int)1;
            InventoryContolParam.timeout = 0; // 0 means endless
        }

        private void buttonInventory_Click(object sender, EventArgs e)
        {
            // check if reader linked
            force_stop = false;
            while (!force_stop && (Form1.R900APP.UHFAPI_IsOpen() == false) )
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

            if (buttonInventory.Text.Equals("Verificar"))
            {
                label2.Text = "starting inventory";
                Application.DoEvents();
                force_stop = false;
                by_trigger = false;
                StartInventory();
            }
            else
            {
                label2.Text = "stopping inventory";
                Application.DoEvents();
                force_stop = true;
                StopInventory();
            }
        }

        public void InventoryTrigger(bool trigger)
        {
            // save status
            trigger_on = trigger;
            force_stop = true;

            if (buttonInventory.Text.Equals("Verificar") && trigger)
            {
                by_trigger = true;

                // start inventory
                StartInventory();

                // disable button
                buttonInventory.Enabled = false;
            }
            else if (buttonInventory.Text.Equals("Parar") && !trigger)
            {
                // stop inventory
                StopInventory();

                // enable button
                buttonInventory.Enabled = true;
            }
            else if (buttonInventory.Text.Equals("Verificar"))
            {
                // enable button
                buttonInventory.Enabled = true;
            }
        }

        internal void StartInventory()
        {
            buttonInventory.Text = "Parar";
            force_stop = false;
            label2.Text = "";

            // prepare inventory parameter
            if (checkBoxContinous.Checked)
                PrepareContinuousInventoryParameter();
            else
                //PrepareSingleInventoryParameter_mask();
                PrepareSingleInventoryParameter();

            // start inventory
            Form1.R900APP.UHFAPI_Inventory(InventoryContolParam, false);
        }

        internal void StopInventory()
        {
            Form1.R900APP.UHFAPI_Stop();
            buttonInventory.Text = "Verificar";
            buttonInventory.Enabled = true;
            label2.Text = "inventory terminated";
        }

        public void InventoryEnd()
        {
            buttonInventory.Text = "Verificar";
            if ( (force_stop && !by_trigger) || (label3.Text != "On Line") )
            {
                label2.Text = "inventory terminated";
            }
            else
                label2.Text = "inventory finished";
            by_trigger = false;
        }

        public void LinkLost()
        {
            label3.Text = "Off Line";
            label3.ForeColor = Color.Red;

            if (buttonInventory.Text != "Verificar")
            {
                buttonInventory.Text = "Verificar";
                // enable button
                buttonInventory.Enabled = true;
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

        public void ShowUploadEPC(string epc)
        {
            //int i;
            int count = listView1.Items.Count;

            // increase count for repeated detection
            ListViewItem item = new ListViewItem(epc.Substring(0, epc.IndexOf(',')));
            item.SubItems.Add(epc.Substring(epc.IndexOf(",Total de Leituras=")+7));
            listView1.Items.Add(item);
            label1.Text = Convert.ToString(Convert.ToInt32(label1.Text) + 1);
        }

        private void FormInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            force_stop = true;
        }

        public void ShowInventoryEPC(string epc)
        {
            int i;
            int count = listView1.Items.Count;

            #region Contadores de Tags

            for (i = 0; i < count; i++)
                if (epc == listView1.Items[i].SubItems[0].Text)
                    break;

            // Total de leituras por tag
            if (i < count)
                listView1.Items[i].SubItems[1].Text = Convert.ToString(Convert.ToInt32(listView1.Items[i].SubItems[1].Text) + 1);
            // Total de tags lidas
            else
            {
                ListViewItem item = new ListViewItem(epc);
                item.SubItems.Add("1");
                listView1.Items.Add(item);
                label1.Text = Convert.ToString(Convert.ToInt32(label1.Text) + 1);
            }

            #endregion

            #region Regras do Mapa

            if (epc.ToString() == tagUsuario)
            {
                pictureTrabalhador.Visible = true;

                if (permissaoDeAcesso == true)
                {
                    pictureMapaVermelho.Visible = false;
                    pictureMapaVerde.Visible = true;
                }
                else
                {
                    pictureMapaVerde.Visible = false;
                    pictureMapaVermelho.Visible = true;
                }

                timerDoMapa.Stop();
                timerDoMapa.Interval = tempoVerificacaoUsuario * 1000;
                timerDoMapa.Start();
            }

            #endregion

            #region Regras das EPIS

                VerificarEPIs(epc);

            #endregion
        }

        private void VerificarEPIs(string epc)
        {
            if (epc.ToString() == tagCapacete)
            {
                pictureOkCapacete.Visible = true;
                leituraCapacete = true;

                timerCapacete.Stop();
                timerCapacete.Interval = tempoVerificacaoEPIs * 1000;
                timerCapacete.Start();
            }

            if (epc.ToString() == tagCinto)
            {
                pictureOkCinto.Visible = true;
                leituraCinto = true;

                timerCinto.Stop();
                timerCinto.Interval = tempoVerificacaoEPIs * 1000;
                timerCinto.Start();
            }

            if (epc.ToString() == tagBota)
            {
                pictureOkBota.Visible = true;
                leituraBota = true;

                timerBota.Stop();
                timerBota.Interval = tempoVerificacaoEPIs * 1000;
                timerBota.Start();
            }

            if (leituraCapacete == true && leituraCinto == true && leituraBota == true)
            {
                pictureUsuarioVerde.Visible = true;

                timerUsuario.Stop();
                timerUsuario.Interval = tempoVerificacaoEPIs * 1000;
                timerUsuario.Start();
            }
            else
            {
                pictureUsuarioVerde.Visible = false;
            }
        }

        private void timerDoMapa_Tick(object sender, EventArgs e)
        {
            pictureTrabalhador.Visible = false;
            pictureMapaVerde.Visible = false;
            pictureMapaVermelho.Visible = false;
        }

        private void timerCapacete_Tick(object sender, EventArgs e)
        {
            pictureOkCapacete.Visible = false;
            leituraCapacete = false;
        }

        private void timerCinto_Tick(object sender, EventArgs e)
        {
            pictureOkCinto.Visible = false;
            leituraCinto = false;
        }

        private void timerBota_Tick(object sender, EventArgs e)
        {
            pictureOkBota.Visible = false;
            leituraBota = false;
        }

        private void timerUsuario_Tick(object sender, EventArgs e)
        {
            pictureUsuarioVerde.Visible = false;
        }
    }
}