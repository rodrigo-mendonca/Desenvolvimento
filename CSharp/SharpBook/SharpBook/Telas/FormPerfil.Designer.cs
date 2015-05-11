namespace SharpBook.Telas
{
    partial class FormPerfil
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
            this.picPerfil = new System.Windows.Forms.PictureBox();
            this.grbOpcoes = new System.Windows.Forms.GroupBox();
            this.cmdPerfil = new System.Windows.Forms.Button();
            this.cmdFilmes = new System.Windows.Forms.Button();
            this.cmdAmigos = new System.Windows.Forms.Button();
            this.cmdMsg = new System.Windows.Forms.Button();
            this.edtEditPerfil = new System.Windows.Forms.LinkLabel();
            this.lblNome = new System.Windows.Forms.Label();
            this.gridAmigos = new System.Windows.Forms.DataGridView();
            this.gridMsg = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.picPerfil)).BeginInit();
            this.grbOpcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAmigos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // picPerfil
            // 
            this.picPerfil.BackColor = System.Drawing.Color.White;
            this.picPerfil.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPerfil.Location = new System.Drawing.Point(12, 12);
            this.picPerfil.Name = "picPerfil";
            this.picPerfil.Size = new System.Drawing.Size(140, 140);
            this.picPerfil.TabIndex = 0;
            this.picPerfil.TabStop = false;
            // 
            // grbOpcoes
            // 
            this.grbOpcoes.Controls.Add(this.cmdPerfil);
            this.grbOpcoes.Controls.Add(this.cmdFilmes);
            this.grbOpcoes.Controls.Add(this.cmdAmigos);
            this.grbOpcoes.Controls.Add(this.cmdMsg);
            this.grbOpcoes.Controls.Add(this.edtEditPerfil);
            this.grbOpcoes.Location = new System.Drawing.Point(12, 158);
            this.grbOpcoes.Name = "grbOpcoes";
            this.grbOpcoes.Size = new System.Drawing.Size(140, 265);
            this.grbOpcoes.TabIndex = 1;
            this.grbOpcoes.TabStop = false;
            this.grbOpcoes.Text = "Opções";
            // 
            // cmdPerfil
            // 
            this.cmdPerfil.Location = new System.Drawing.Point(10, 36);
            this.cmdPerfil.Name = "cmdPerfil";
            this.cmdPerfil.Size = new System.Drawing.Size(124, 23);
            this.cmdPerfil.TabIndex = 4;
            this.cmdPerfil.UseVisualStyleBackColor = true;
            // 
            // cmdFilmes
            // 
            this.cmdFilmes.Location = new System.Drawing.Point(10, 123);
            this.cmdFilmes.Name = "cmdFilmes";
            this.cmdFilmes.Size = new System.Drawing.Size(124, 23);
            this.cmdFilmes.TabIndex = 3;
            this.cmdFilmes.Text = "Filmes";
            this.cmdFilmes.UseVisualStyleBackColor = true;
            // 
            // cmdAmigos
            // 
            this.cmdAmigos.Location = new System.Drawing.Point(10, 94);
            this.cmdAmigos.Name = "cmdAmigos";
            this.cmdAmigos.Size = new System.Drawing.Size(124, 23);
            this.cmdAmigos.TabIndex = 2;
            this.cmdAmigos.Text = "Amigos";
            this.cmdAmigos.UseVisualStyleBackColor = true;
            this.cmdAmigos.Click += new System.EventHandler(this.cmdAmigos_Click);
            // 
            // cmdMsg
            // 
            this.cmdMsg.Location = new System.Drawing.Point(10, 65);
            this.cmdMsg.Name = "cmdMsg";
            this.cmdMsg.Size = new System.Drawing.Size(124, 23);
            this.cmdMsg.TabIndex = 1;
            this.cmdMsg.Text = "Menssagens";
            this.cmdMsg.UseVisualStyleBackColor = true;
            this.cmdMsg.Click += new System.EventHandler(this.cmdMsg_Click);
            // 
            // edtEditPerfil
            // 
            this.edtEditPerfil.AutoSize = true;
            this.edtEditPerfil.Location = new System.Drawing.Point(7, 20);
            this.edtEditPerfil.Name = "edtEditPerfil";
            this.edtEditPerfil.Size = new System.Drawing.Size(60, 13);
            this.edtEditPerfil.TabIndex = 0;
            this.edtEditPerfil.TabStop = true;
            this.edtEditPerfil.Text = "Editar Perfil";
            this.edtEditPerfil.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.edtEditPerfil_LinkClicked);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(158, 12);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(323, 25);
            this.lblNome.TabIndex = 5;
            this.lblNome.Text = "Nome e Sobre Nome do Perfil";
            // 
            // gridAmigos
            // 
            this.gridAmigos.BackgroundColor = System.Drawing.Color.White;
            this.gridAmigos.GridColor = System.Drawing.Color.White;
            this.gridAmigos.Location = new System.Drawing.Point(163, 40);
            this.gridAmigos.Name = "gridAmigos";
            this.gridAmigos.RowHeadersVisible = false;
            this.gridAmigos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAmigos.Size = new System.Drawing.Size(377, 383);
            this.gridAmigos.TabIndex = 6;
            this.gridAmigos.Visible = false;
            // 
            // gridMsg
            // 
            this.gridMsg.BackgroundColor = System.Drawing.Color.White;
            this.gridMsg.GridColor = System.Drawing.Color.White;
            this.gridMsg.Location = new System.Drawing.Point(163, 40);
            this.gridMsg.Name = "gridMsg";
            this.gridMsg.RowHeadersVisible = false;
            this.gridMsg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMsg.Size = new System.Drawing.Size(377, 383);
            this.gridMsg.TabIndex = 7;
            this.gridMsg.Visible = false;
            // 
            // FormPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 427);
            this.Controls.Add(this.gridMsg);
            this.Controls.Add(this.gridAmigos);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.grbOpcoes);
            this.Controls.Add(this.picPerfil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormPerfil";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Perfil";
            this.Deactivate += new System.EventHandler(this.FormPerfil_Deactivate);
            this.Load += new System.EventHandler(this.FormPerfil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPerfil)).EndInit();
            this.grbOpcoes.ResumeLayout(false);
            this.grbOpcoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAmigos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMsg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPerfil;
        private System.Windows.Forms.GroupBox grbOpcoes;
        private System.Windows.Forms.Button cmdPerfil;
        private System.Windows.Forms.Button cmdFilmes;
        private System.Windows.Forms.Button cmdAmigos;
        private System.Windows.Forms.Button cmdMsg;
        private System.Windows.Forms.LinkLabel edtEditPerfil;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.DataGridView gridAmigos;
        private System.Windows.Forms.DataGridView gridMsg;
    }
}