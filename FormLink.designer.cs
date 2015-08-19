namespace Prototipo
{
    partial class FormLink
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBind = new System.Windows.Forms.Button();
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.comboBoxFreeComs = new System.Windows.Forms.ComboBox();
            this.buttonComports = new System.Windows.Forms.Button();
            this.comboBoxComports = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(66, 104);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(163, 31);
            this.buttonSearch.TabIndex = 0;
            this.buttonSearch.Text = "DEVICE SEARCH";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonBind
            // 
            this.buttonBind.Location = new System.Drawing.Point(87, 198);
            this.buttonBind.Name = "buttonBind";
            this.buttonBind.Size = new System.Drawing.Size(114, 34);
            this.buttonBind.TabIndex = 1;
            this.buttonBind.Text = "BIND";
            this.buttonBind.Click += new System.EventHandler(this.buttonBind_Click);
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.Location = new System.Drawing.Point(27, 142);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(238, 20);
            this.comboBoxDevices.TabIndex = 2;
            this.comboBoxDevices.SelectedIndexChanged += new System.EventHandler(this.comboBoxDevices_SelectedIndexChanged);
            // 
            // comboBoxFreeComs
            // 
            this.comboBoxFreeComs.Location = new System.Drawing.Point(73, 175);
            this.comboBoxFreeComs.Name = "comboBoxFreeComs";
            this.comboBoxFreeComs.Size = new System.Drawing.Size(145, 20);
            this.comboBoxFreeComs.TabIndex = 3;
            // 
            // buttonComports
            // 
            this.buttonComports.Location = new System.Drawing.Point(68, 10);
            this.buttonComports.Name = "buttonComports";
            this.buttonComports.Size = new System.Drawing.Size(160, 30);
            this.buttonComports.TabIndex = 4;
            this.buttonComports.Text = "Select COMPORT";
            this.buttonComports.UseVisualStyleBackColor = true;
            this.buttonComports.Click += new System.EventHandler(this.buttonLink_Click);
            // 
            // comboBoxComports
            // 
            this.comboBoxComports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxComports.FormattingEnabled = true;
            this.comboBoxComports.Location = new System.Drawing.Point(27, 49);
            this.comboBoxComports.Name = "comboBoxComports";
            this.comboBoxComports.Size = new System.Drawing.Size(238, 20);
            this.comboBoxComports.Sorted = true;
            this.comboBoxComports.TabIndex = 5;
            // 
            // FormLink
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.comboBoxComports);
            this.Controls.Add(this.buttonComports);
            this.Controls.Add(this.comboBoxFreeComs);
            this.Controls.Add(this.comboBoxDevices);
            this.Controls.Add(this.buttonBind);
            this.Controls.Add(this.buttonSearch);
            this.Name = "FormLink";
            this.Text = "FormLink";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonBind;
        private System.Windows.Forms.ComboBox comboBoxDevices;
        private System.Windows.Forms.ComboBox comboBoxFreeComs;
        private System.Windows.Forms.Button buttonComports;
        private System.Windows.Forms.ComboBox comboBoxComports;
    }
}

