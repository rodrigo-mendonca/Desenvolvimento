namespace PGM.FormTeste
{
    partial class FormTeste
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Grid = new PGM.Controls.PgmControl.PgmDataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDesc = new PGM.Controls.PgmControl.PgmTextBox();
            this.txtValor = new PGM.Controls.PgmControl.PgmTextBox();
            this.txtData = new PGM.Controls.PgmControl.PgmTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtID = new PGM.Controls.PgmControl.PgmTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.txtUtil = new PGM.Controls.PgmControl.PgmTextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.txtDiasUteis = new PGM.Controls.PgmControl.PgmTextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.txtAte = new PGM.Controls.PgmControl.PgmTextBox();
            this.txtNumZero = new PGM.Controls.PgmControl.PgmTextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.txtSenha = new PGM.Controls.PgmControl.PgmTextBox();
            this.cmdCriptar = new PGM.Controls.PgmControl.PgmButton();
            this.pgmButton1 = new PGM.Controls.PgmControl.PgmButton();
            this.cmdExportar = new PGM.Controls.PgmControl.PgmButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(159, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Incluir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Grid
            // 
            this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grid.IsSortable = true;
            this.Grid.Location = new System.Drawing.Point(239, 26);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(632, 461);
            this.Grid.TabIndex = 5;
            this.Grid.PgmDoubleClick += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Desc.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Valor";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.txtDesc.BlankIfZero = false;
            this.txtDesc.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.String;
            this.txtDesc.Location = new System.Drawing.Point(53, 68);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Obrigatory = true;
            this.txtDesc.Size = new System.Drawing.Size(98, 20);
            this.txtDesc.TabIndex = 8;
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.BlankIfZero = false;
            this.txtValor.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Decimal;
            this.txtValor.Location = new System.Drawing.Point(53, 94);
            this.txtValor.Name = "txtValor";
            this.txtValor.Obrigatory = false;
            this.txtValor.Size = new System.Drawing.Size(98, 20);
            this.txtValor.TabIndex = 9;
            // 
            // txtData
            // 
            this.txtData.BackColor = System.Drawing.Color.White;
            this.txtData.BlankIfZero = false;
            this.txtData.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Date;
            this.txtData.Location = new System.Drawing.Point(53, 120);
            this.txtData.Name = "txtData";
            this.txtData.Obrigatory = false;
            this.txtData.Size = new System.Drawing.Size(98, 20);
            this.txtData.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Data";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(207, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "<<";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 189);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "Update";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.txtID.BlankIfZero = false;
            this.txtID.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Integer;
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(53, 42);
            this.txtID.Name = "txtID";
            this.txtID.Obrigatory = true;
            this.txtID.Size = new System.Drawing.Size(98, 20);
            this.txtID.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Id.";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(15, 218);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "Delete";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(118, 260);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 17;
            this.button6.Text = "Verif. Util";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // txtUtil
            // 
            this.txtUtil.BackColor = System.Drawing.Color.White;
            this.txtUtil.BlankIfZero = false;
            this.txtUtil.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Date;
            this.txtUtil.Location = new System.Drawing.Point(14, 260);
            this.txtUtil.Name = "txtUtil";
            this.txtUtil.Obrigatory = false;
            this.txtUtil.Size = new System.Drawing.Size(98, 20);
            this.txtUtil.TabIndex = 18;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(118, 294);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 19;
            this.button7.Text = "Prox. Util";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtDiasUteis
            // 
            this.txtDiasUteis.BackColor = System.Drawing.Color.White;
            this.txtDiasUteis.BlankIfZero = false;
            this.txtDiasUteis.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Integer;
            this.txtDiasUteis.Location = new System.Drawing.Point(14, 294);
            this.txtDiasUteis.Name = "txtDiasUteis";
            this.txtDiasUteis.Obrigatory = false;
            this.txtDiasUteis.Size = new System.Drawing.Size(98, 20);
            this.txtDiasUteis.TabIndex = 20;
            this.txtDiasUteis.ValidatingType = typeof(int);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(118, 323);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 21;
            this.button8.Text = "Conta Util";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // txtAte
            // 
            this.txtAte.BackColor = System.Drawing.Color.White;
            this.txtAte.BlankIfZero = false;
            this.txtAte.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Date;
            this.txtAte.Location = new System.Drawing.Point(15, 323);
            this.txtAte.Name = "txtAte";
            this.txtAte.Obrigatory = false;
            this.txtAte.Size = new System.Drawing.Size(98, 20);
            this.txtAte.TabIndex = 22;
            // 
            // txtNumZero
            // 
            this.txtNumZero.BackColor = System.Drawing.Color.White;
            this.txtNumZero.BlankIfZero = false;
            this.txtNumZero.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.Integer;
            this.txtNumZero.Location = new System.Drawing.Point(15, 366);
            this.txtNumZero.Mask = "00000";
            this.txtNumZero.Name = "txtNumZero";
            this.txtNumZero.Obrigatory = false;
            this.txtNumZero.Size = new System.Drawing.Size(98, 20);
            this.txtNumZero.TabIndex = 23;
            this.txtNumZero.ValidatingType = typeof(int);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(119, 363);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 24;
            this.button9.Text = "StrZero";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // txtSenha
            // 
            this.txtSenha.BackColor = System.Drawing.Color.White;
            this.txtSenha.BlankIfZero = false;
            this.txtSenha.DataType = PGM.Controls.PgmControl.PgmTextBox.Type.String;
            this.txtSenha.Location = new System.Drawing.Point(12, 419);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Obrigatory = false;
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(98, 20);
            this.txtSenha.TabIndex = 26;
            // 
            // cmdCriptar
            // 
            this.cmdCriptar.Location = new System.Drawing.Point(119, 416);
            this.cmdCriptar.Name = "cmdCriptar";
            this.cmdCriptar.Size = new System.Drawing.Size(75, 23);
            this.cmdCriptar.TabIndex = 27;
            this.cmdCriptar.Text = "Criptar";
            this.cmdCriptar.UseVisualStyleBackColor = true;
            this.cmdCriptar.Click += new System.EventHandler(this.cmdCriptar_Click);
            // 
            // pgmButton1
            // 
            this.pgmButton1.Location = new System.Drawing.Point(794, 18);
            this.pgmButton1.Name = "pgmButton1";
            this.pgmButton1.Size = new System.Drawing.Size(75, 23);
            this.pgmButton1.TabIndex = 52;
            this.pgmButton1.Text = "pgmButton1";
            this.pgmButton1.UseVisualStyleBackColor = true;
            this.pgmButton1.Click += new System.EventHandler(this.pgmButton1_Click);
            // 
            // cmdExportar
            // 
            this.cmdExportar.Location = new System.Drawing.Point(159, 464);
            this.cmdExportar.Name = "cmdExportar";
            this.cmdExportar.Size = new System.Drawing.Size(75, 23);
            this.cmdExportar.TabIndex = 53;
            this.cmdExportar.Text = "Exportar";
            this.cmdExportar.UseVisualStyleBackColor = true;
            this.cmdExportar.Click += new System.EventHandler(this.cmdExportar_Click);
            // 
            // FormTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 499);
            this.Controls.Add(this.cmdExportar);
            this.Controls.Add(this.pgmButton1);
            this.Controls.Add(this.cmdCriptar);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.txtNumZero);
            this.Controls.Add(this.txtAte);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.txtDiasUteis);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.txtUtil);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FormTeste";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.Grid, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtDesc, 0);
            this.Controls.SetChildIndex(this.txtValor, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtData, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            this.Controls.SetChildIndex(this.button4, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.button5, 0);
            this.Controls.SetChildIndex(this.button6, 0);
            this.Controls.SetChildIndex(this.txtUtil, 0);
            this.Controls.SetChildIndex(this.button7, 0);
            this.Controls.SetChildIndex(this.txtDiasUteis, 0);
            this.Controls.SetChildIndex(this.button8, 0);
            this.Controls.SetChildIndex(this.txtAte, 0);
            this.Controls.SetChildIndex(this.txtNumZero, 0);
            this.Controls.SetChildIndex(this.button9, 0);
            this.Controls.SetChildIndex(this.txtSenha, 0);
            this.Controls.SetChildIndex(this.cmdCriptar, 0);
            this.Controls.SetChildIndex(this.pgmButton1, 0);
            this.Controls.SetChildIndex(this.cmdExportar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Controls.PgmControl.PgmDataGrid Grid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.PgmControl.PgmTextBox txtDesc;
        private Controls.PgmControl.PgmTextBox txtValor;
        private Controls.PgmControl.PgmTextBox txtData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private Controls.PgmControl.PgmTextBox txtID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private Controls.PgmControl.PgmTextBox txtUtil;
        private System.Windows.Forms.Button button7;
        private Controls.PgmControl.PgmTextBox txtDiasUteis;
        private System.Windows.Forms.Button button8;
        private Controls.PgmControl.PgmTextBox txtAte;
        private Controls.PgmControl.PgmTextBox txtNumZero;
        private System.Windows.Forms.Button button9;
        private Controls.PgmControl.PgmTextBox txtSenha;
        private Controls.PgmControl.PgmButton cmdCriptar;
        private Controls.PgmControl.PgmButton pgmButton1;
        private Controls.PgmControl.PgmButton cmdExportar;
    }
}

