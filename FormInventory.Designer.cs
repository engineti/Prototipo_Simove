namespace Prototipo
{
    partial class FormInventory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInventory));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBoxContinous = new System.Windows.Forms.CheckBox();
            this.buttonInventory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timerDoMapa = new System.Windows.Forms.Timer(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureCapacete = new System.Windows.Forms.PictureBox();
            this.pictureBota = new System.Windows.Forms.PictureBox();
            this.pictureCinto = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureTrabalhador = new System.Windows.Forms.PictureBox();
            this.pictureOkBota = new System.Windows.Forms.PictureBox();
            this.pictureOkCinto = new System.Windows.Forms.PictureBox();
            this.pictureOkCapacete = new System.Windows.Forms.PictureBox();
            this.pictureUsuarioVerde = new System.Windows.Forms.PictureBox();
            this.pictureUsuarioVermelho = new System.Windows.Forms.PictureBox();
            this.pictureMapaVermelho = new System.Windows.Forms.PictureBox();
            this.pictureMapaVerde = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.timerUsuario = new System.Windows.Forms.Timer(this.components);
            this.timerCapacete = new System.Windows.Forms.Timer(this.components);
            this.timerCinto = new System.Windows.Forms.Timer(this.components);
            this.timerBota = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCapacete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCinto)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTrabalhador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOkBota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOkCinto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOkCapacete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuarioVerde)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuarioVermelho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMapaVermelho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMapaVerde)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(12, 396);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(732, 150);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Número da Tag";
            this.columnHeader1.Width = 625;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Total de Leituras";
            this.columnHeader2.Width = 100;
            // 
            // checkBoxContinous
            // 
            this.checkBoxContinous.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxContinous.Location = new System.Drawing.Point(644, 615);
            this.checkBoxContinous.Name = "checkBoxContinous";
            this.checkBoxContinous.Size = new System.Drawing.Size(100, 16);
            this.checkBoxContinous.TabIndex = 1;
            this.checkBoxContinous.Text = "Contínuo";
            this.checkBoxContinous.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.checkBoxContinous.UseVisualStyleBackColor = false;
            // 
            // buttonInventory
            // 
            this.buttonInventory.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInventory.Location = new System.Drawing.Point(621, 566);
            this.buttonInventory.Name = "buttonInventory";
            this.buttonInventory.Size = new System.Drawing.Size(123, 36);
            this.buttonInventory.TabIndex = 2;
            this.buttonInventory.Text = "Verificar";
            this.buttonInventory.UseVisualStyleBackColor = false;
            this.buttonInventory.Click += new System.EventHandler(this.buttonInventory_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20F);
            this.label1.Location = new System.Drawing.Point(12, 557);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 615);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // timerDoMapa
            // 
            this.timerDoMapa.Interval = 2000;
            this.timerDoMapa.Tick += new System.EventHandler(this.timerDoMapa_Tick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(57, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(122, 135);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // pictureCapacete
            // 
            this.pictureCapacete.Image = ((System.Drawing.Image)(resources.GetObject("pictureCapacete.Image")));
            this.pictureCapacete.Location = new System.Drawing.Point(23, 206);
            this.pictureCapacete.Name = "pictureCapacete";
            this.pictureCapacete.Size = new System.Drawing.Size(83, 59);
            this.pictureCapacete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCapacete.TabIndex = 8;
            this.pictureCapacete.TabStop = false;
            // 
            // pictureBota
            // 
            this.pictureBota.Image = ((System.Drawing.Image)(resources.GetObject("pictureBota.Image")));
            this.pictureBota.Location = new System.Drawing.Point(23, 295);
            this.pictureBota.Name = "pictureBota";
            this.pictureBota.Size = new System.Drawing.Size(83, 60);
            this.pictureBota.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBota.TabIndex = 9;
            this.pictureBota.TabStop = false;
            // 
            // pictureCinto
            // 
            this.pictureCinto.Image = ((System.Drawing.Image)(resources.GetObject("pictureCinto.Image")));
            this.pictureCinto.Location = new System.Drawing.Point(124, 206);
            this.pictureCinto.Name = "pictureCinto";
            this.pictureCinto.Size = new System.Drawing.Size(83, 62);
            this.pictureCinto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCinto.TabIndex = 10;
            this.pictureCinto.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureTrabalhador);
            this.panel1.Controls.Add(this.pictureOkBota);
            this.panel1.Controls.Add(this.pictureOkCinto);
            this.panel1.Controls.Add(this.pictureOkCapacete);
            this.panel1.Controls.Add(this.pictureUsuarioVerde);
            this.panel1.Controls.Add(this.pictureUsuarioVermelho);
            this.panel1.Controls.Add(this.pictureCinto);
            this.panel1.Controls.Add(this.pictureCapacete);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBota);
            this.panel1.Location = new System.Drawing.Point(524, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 386);
            this.panel1.TabIndex = 14;
            // 
            // pictureTrabalhador
            // 
            this.pictureTrabalhador.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureTrabalhador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureTrabalhador.Image = ((System.Drawing.Image)(resources.GetObject("pictureTrabalhador.Image")));
            this.pictureTrabalhador.Location = new System.Drawing.Point(57, 12);
            this.pictureTrabalhador.Name = "pictureTrabalhador";
            this.pictureTrabalhador.Size = new System.Drawing.Size(122, 135);
            this.pictureTrabalhador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureTrabalhador.TabIndex = 25;
            this.pictureTrabalhador.TabStop = false;
            this.pictureTrabalhador.Visible = false;
            // 
            // pictureOkBota
            // 
            this.pictureOkBota.Image = ((System.Drawing.Image)(resources.GetObject("pictureOkBota.Image")));
            this.pictureOkBota.Location = new System.Drawing.Point(87, 283);
            this.pictureOkBota.Name = "pictureOkBota";
            this.pictureOkBota.Size = new System.Drawing.Size(22, 24);
            this.pictureOkBota.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureOkBota.TabIndex = 24;
            this.pictureOkBota.TabStop = false;
            this.pictureOkBota.Visible = false;
            // 
            // pictureOkCinto
            // 
            this.pictureOkCinto.Image = ((System.Drawing.Image)(resources.GetObject("pictureOkCinto.Image")));
            this.pictureOkCinto.Location = new System.Drawing.Point(188, 194);
            this.pictureOkCinto.Name = "pictureOkCinto";
            this.pictureOkCinto.Size = new System.Drawing.Size(22, 24);
            this.pictureOkCinto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureOkCinto.TabIndex = 23;
            this.pictureOkCinto.TabStop = false;
            this.pictureOkCinto.Visible = false;
            // 
            // pictureOkCapacete
            // 
            this.pictureOkCapacete.Image = ((System.Drawing.Image)(resources.GetObject("pictureOkCapacete.Image")));
            this.pictureOkCapacete.Location = new System.Drawing.Point(87, 194);
            this.pictureOkCapacete.Name = "pictureOkCapacete";
            this.pictureOkCapacete.Size = new System.Drawing.Size(22, 24);
            this.pictureOkCapacete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureOkCapacete.TabIndex = 22;
            this.pictureOkCapacete.TabStop = false;
            this.pictureOkCapacete.Visible = false;
            // 
            // pictureUsuarioVerde
            // 
            this.pictureUsuarioVerde.Image = ((System.Drawing.Image)(resources.GetObject("pictureUsuarioVerde.Image")));
            this.pictureUsuarioVerde.Location = new System.Drawing.Point(145, 304);
            this.pictureUsuarioVerde.Name = "pictureUsuarioVerde";
            this.pictureUsuarioVerde.Size = new System.Drawing.Size(44, 45);
            this.pictureUsuarioVerde.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureUsuarioVerde.TabIndex = 19;
            this.pictureUsuarioVerde.TabStop = false;
            this.pictureUsuarioVerde.Visible = false;
            // 
            // pictureUsuarioVermelho
            // 
            this.pictureUsuarioVermelho.Image = ((System.Drawing.Image)(resources.GetObject("pictureUsuarioVermelho.Image")));
            this.pictureUsuarioVermelho.Location = new System.Drawing.Point(145, 304);
            this.pictureUsuarioVermelho.Name = "pictureUsuarioVermelho";
            this.pictureUsuarioVermelho.Size = new System.Drawing.Size(44, 45);
            this.pictureUsuarioVermelho.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureUsuarioVermelho.TabIndex = 18;
            this.pictureUsuarioVermelho.TabStop = false;
            // 
            // pictureMapaVermelho
            // 
            this.pictureMapaVermelho.BackColor = System.Drawing.Color.Transparent;
            this.pictureMapaVermelho.Image = ((System.Drawing.Image)(resources.GetObject("pictureMapaVermelho.Image")));
            this.pictureMapaVermelho.Location = new System.Drawing.Point(110, 145);
            this.pictureMapaVermelho.Name = "pictureMapaVermelho";
            this.pictureMapaVermelho.Size = new System.Drawing.Size(45, 50);
            this.pictureMapaVermelho.TabIndex = 16;
            this.pictureMapaVermelho.TabStop = false;
            this.pictureMapaVermelho.Visible = false;
            // 
            // pictureMapaVerde
            // 
            this.pictureMapaVerde.BackColor = System.Drawing.Color.Transparent;
            this.pictureMapaVerde.Image = ((System.Drawing.Image)(resources.GetObject("pictureMapaVerde.Image")));
            this.pictureMapaVerde.Location = new System.Drawing.Point(110, 145);
            this.pictureMapaVerde.Name = "pictureMapaVerde";
            this.pictureMapaVerde.Size = new System.Drawing.Size(45, 50);
            this.pictureMapaVerde.TabIndex = 17;
            this.pictureMapaVerde.TabStop = false;
            this.pictureMapaVerde.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 594);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(112, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // pictureBox15
            // 
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(321, 582);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(116, 49);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox15.TabIndex = 18;
            this.pictureBox15.TabStop = false;
            // 
            // pictureBox16
            // 
            this.pictureBox16.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox16.Image")));
            this.pictureBox16.Location = new System.Drawing.Point(488, 470);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(267, 177);
            this.pictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox16.TabIndex = 19;
            this.pictureBox16.TabStop = false;
            // 
            // timerUsuario
            // 
            this.timerUsuario.Interval = 2000;
            this.timerUsuario.Tick += new System.EventHandler(this.timerUsuario_Tick);
            // 
            // timerCapacete
            // 
            this.timerCapacete.Interval = 2000;
            this.timerCapacete.Tick += new System.EventHandler(this.timerCapacete_Tick);
            // 
            // timerCinto
            // 
            this.timerCinto.Interval = 2000;
            this.timerCinto.Tick += new System.EventHandler(this.timerCinto_Tick);
            // 
            // timerBota
            // 
            this.timerBota.Interval = 2000;
            this.timerBota.Tick += new System.EventHandler(this.timerBota_Tick);
            // 
            // FormInventory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(756, 643);
            this.Controls.Add(this.pictureBox15);
            this.Controls.Add(this.pictureMapaVerde);
            this.Controls.Add(this.pictureMapaVermelho);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonInventory);
            this.Controls.Add(this.checkBoxContinous);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pictureBox16);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximizeBox = false;
            this.Name = "FormInventory";
            this.Text = "Painel de Controle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInventory_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCapacete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCinto)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureTrabalhador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOkBota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOkCinto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOkCapacete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuarioVerde)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsuarioVermelho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMapaVermelho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMapaVerde)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.CheckBox checkBoxContinous;
        private System.Windows.Forms.Button buttonInventory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerDoMapa;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureCapacete;
        private System.Windows.Forms.PictureBox pictureBota;
        private System.Windows.Forms.PictureBox pictureCinto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureMapaVermelho;
        private System.Windows.Forms.PictureBox pictureMapaVerde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureUsuarioVermelho;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.PictureBox pictureUsuarioVerde;
        private System.Windows.Forms.Timer timerUsuario;
        private System.Windows.Forms.Timer timerCapacete;
        private System.Windows.Forms.Timer timerCinto;
        private System.Windows.Forms.Timer timerBota;
        private System.Windows.Forms.PictureBox pictureOkBota;
        private System.Windows.Forms.PictureBox pictureOkCinto;
        private System.Windows.Forms.PictureBox pictureOkCapacete;
        private System.Windows.Forms.PictureBox pictureTrabalhador;
    }
}