using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoObras
{
    public partial class FormConfigTags : Form
    {
        public FormConfigTags()
        {
            InitializeComponent();

            if (cbTempoUsuario.SelectedItem == null)
            {
                cbTempoUsuario.SelectedItem = "2";
            }

            if (cbTempoEPIs.SelectedItem  == null)
            {
                cbTempoEPIs.SelectedItem = "2";
            }

            textBox1.Text = FormInventory.tagUsuario;
            textBox2.Text = FormInventory.tagCapacete;
            textBox3.Text = FormInventory.tagCinto;
            textBox4.Text = FormInventory.tagBota;

            cbTempoUsuario.SelectedItem = FormInventory.tempoVerificacaoUsuario.ToString();
            cbTempoEPIs.SelectedItem = FormInventory.tempoVerificacaoEPIs.ToString();

            checkBox1.Checked = FormInventory.permissaoDeAcesso;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormInventory.tagUsuario = textBox1.Text;
            FormInventory.tagCapacete = textBox2.Text;
            FormInventory.tagCinto = textBox3.Text;
            FormInventory.tagBota = textBox4.Text;

            FormInventory.tempoVerificacaoUsuario = Convert.ToInt32(cbTempoUsuario.SelectedItem);
            FormInventory.tempoVerificacaoEPIs = Convert.ToInt32(cbTempoEPIs.SelectedItem);


            this.Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            FormInventory.permissaoDeAcesso = checkBox1.Checked;
        }
    }
}
