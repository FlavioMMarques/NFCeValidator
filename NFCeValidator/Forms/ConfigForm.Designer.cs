namespace NFCeValidator.Forms
{
    partial class ConfigForm
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMostrarSenha = new System.Windows.Forms.CheckBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.lblPorta = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnListarBancos = new System.Windows.Forms.Button();
            this.cmbBancoDados = new System.Windows.Forms.ComboBox();
            this.lblBancoDados = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.lblServidor = new System.Windows.Forms.Label();
            this.btnTestar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(279, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Configuração do Banco de Dados";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMostrarSenha);
            this.groupBox1.Controls.Add(this.txtTimeout);
            this.groupBox1.Controls.Add(this.lblTimeout);
            this.groupBox1.Controls.Add(this.txtPorta);
            this.groupBox1.Controls.Add(this.lblPorta);
            this.groupBox1.Controls.Add(this.txtSenha);
            this.groupBox1.Controls.Add(this.lblSenha);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.lblUsuario);
            this.groupBox1.Controls.Add(this.btnListarBancos);
            this.groupBox1.Controls.Add(this.cmbBancoDados);
            this.groupBox1.Controls.Add(this.lblBancoDados);
            this.groupBox1.Controls.Add(this.txtServidor);
            this.groupBox1.Controls.Add(this.lblServidor);
            this.groupBox1.Controls.Add(this.btnTestar);
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 246);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configurações de Conexão";
            // 
            // chkMostrarSenha
            // 
            this.chkMostrarSenha.AutoSize = true;
            this.chkMostrarSenha.Location = new System.Drawing.Point(160, 187);
            this.chkMostrarSenha.Name = "chkMostrarSenha";
            this.chkMostrarSenha.Size = new System.Drawing.Size(95, 17);
            this.chkMostrarSenha.TabIndex = 13;
            this.chkMostrarSenha.Text = "Mostrar Senha";
            this.chkMostrarSenha.UseVisualStyleBackColor = true;
            this.chkMostrarSenha.CheckedChanged += new System.EventHandler(this.chkMostrarSenha_CheckedChanged);
            // 
            // txtTimeout
            // 
            this.txtTimeout.Location = new System.Drawing.Point(414, 42);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(140, 20);
            this.txtTimeout.TabIndex = 12;
            this.txtTimeout.Text = "120";
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(411, 26);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(52, 13);
            this.lblTimeout.TabIndex = 11;
            this.lblTimeout.Text = "*Timeout:";
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(268, 42);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(140, 20);
            this.txtPorta.TabIndex = 10;
            this.txtPorta.Text = "5433";
            // 
            // lblPorta
            // 
            this.lblPorta.AutoSize = true;
            this.lblPorta.Location = new System.Drawing.Point(265, 26);
            this.lblPorta.Name = "lblPorta";
            this.lblPorta.Size = new System.Drawing.Size(39, 13);
            this.lblPorta.TabIndex = 9;
            this.lblPorta.Text = "*Porta:";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(16, 161);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(538, 20);
            this.txtSenha.TabIndex = 8;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(13, 145);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(45, 13);
            this.lblSenha.TabIndex = 7;
            this.lblSenha.Text = "*Senha:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(16, 122);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(538, 20);
            this.txtUsuario.TabIndex = 6;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(13, 106);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(50, 13);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "*Usuário:";
            // 
            // btnListarBancos
            // 
            this.btnListarBancos.Location = new System.Drawing.Point(414, 81);
            this.btnListarBancos.Name = "btnListarBancos";
            this.btnListarBancos.Size = new System.Drawing.Size(140, 23);
            this.btnListarBancos.TabIndex = 14;
            this.btnListarBancos.Text = "🔄 Listar Bancos";
            this.btnListarBancos.UseVisualStyleBackColor = true;
            this.btnListarBancos.Click += new System.EventHandler(this.btnListarBancos_Click);
            // 
            // cmbBancoDados
            // 
            this.cmbBancoDados.FormattingEnabled = true;
            this.cmbBancoDados.Location = new System.Drawing.Point(16, 83);
            this.cmbBancoDados.Name = "cmbBancoDados";
            this.cmbBancoDados.Size = new System.Drawing.Size(392, 21);
            this.cmbBancoDados.TabIndex = 4;
            // 
            // lblBancoDados
            // 
            this.lblBancoDados.AutoSize = true;
            this.lblBancoDados.Location = new System.Drawing.Point(13, 67);
            this.lblBancoDados.Name = "lblBancoDados";
            this.lblBancoDados.Size = new System.Drawing.Size(94, 13);
            this.lblBancoDados.TabIndex = 3;
            this.lblBancoDados.Text = "*Banco de Dados:";
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(16, 42);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(246, 20);
            this.txtServidor.TabIndex = 2;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Location = new System.Drawing.Point(13, 26);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(101, 13);
            this.lblServidor.TabIndex = 1;
            this.lblServidor.Text = "*Servidor\\Instância:";
            // 
            // btnTestar
            // 
            this.btnTestar.Location = new System.Drawing.Point(16, 210);
            this.btnTestar.Name = "btnTestar";
            this.btnTestar.Size = new System.Drawing.Size(150, 30);
            this.btnTestar.TabIndex = 3;
            this.btnTestar.Text = "Testar Conexão";
            this.btnTestar.UseVisualStyleBackColor = true;
            this.btnTestar.Click += new System.EventHandler(this.btnTestar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(376, 284);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 30);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(482, 284);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 320);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração do Banco de Dados";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.Button btnTestar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cmbBancoDados;
        private System.Windows.Forms.Label lblBancoDados;
        private System.Windows.Forms.Button btnListarBancos;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Label lblPorta;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.CheckBox chkMostrarSenha;
    }
}
