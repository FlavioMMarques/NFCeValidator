namespace NFCeValidator.Forms
{
    partial class MainForm
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
            this.btnSelecionarPasta = new System.Windows.Forms.Button();
            this.txtCaminhoPasta = new System.Windows.Forms.TextBox();
            this.lblCaminhoPasta = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvXML = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.btnCarregarView = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblValorTotalView = new System.Windows.Forms.Label();
            this.lblQuantidadeView = new System.Windows.Forms.Label();
            this.btnValidar = new System.Windows.Forms.Button();
            this.btnConfigurar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLoja = new System.Windows.Forms.ComboBox();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.lblDataFinal = new System.Windows.Forms.Label();
            this.lblDataInicial = new System.Windows.Forms.Label();
            this.txtNomeView = new System.Windows.Forms.TextBox();
            this.lblNomeView = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXML)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(251, 24);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Validador de NFCe - XML";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelecionarPasta);
            this.groupBox1.Controls.Add(this.txtCaminhoPasta);
            this.groupBox1.Controls.Add(this.lblCaminhoPasta);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1456, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleção de Pasta";
            // 
            // btnSelecionarPasta
            // 
            this.btnSelecionarPasta.Location = new System.Drawing.Point(1326, 37);
            this.btnSelecionarPasta.Name = "btnSelecionarPasta";
            this.btnSelecionarPasta.Size = new System.Drawing.Size(110, 25);
            this.btnSelecionarPasta.TabIndex = 2;
            this.btnSelecionarPasta.Text = "Selecionar Pasta";
            this.btnSelecionarPasta.UseVisualStyleBackColor = true;
            this.btnSelecionarPasta.Click += new System.EventHandler(this.btnSelecionarPasta_Click);
            // 
            // txtCaminhoPasta
            // 
            this.txtCaminhoPasta.Location = new System.Drawing.Point(16, 40);
            this.txtCaminhoPasta.Name = "txtCaminhoPasta";
            this.txtCaminhoPasta.ReadOnly = true;
            this.txtCaminhoPasta.Size = new System.Drawing.Size(1304, 20);
            this.txtCaminhoPasta.TabIndex = 1;
            // 
            // lblCaminhoPasta
            // 
            this.lblCaminhoPasta.AutoSize = true;
            this.lblCaminhoPasta.Location = new System.Drawing.Point(13, 24);
            this.lblCaminhoPasta.Name = "lblCaminhoPasta";
            this.lblCaminhoPasta.Size = new System.Drawing.Size(149, 13);
            this.lblCaminhoPasta.TabIndex = 0;
            this.lblCaminhoPasta.Text = "Caminho da Pasta com XMLs:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvXML);
            this.groupBox2.Location = new System.Drawing.Point(12, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 350);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "📄 XMLs Carregados";
            // 
            // dgvXML
            // 
            this.dgvXML.AllowUserToAddRows = false;
            this.dgvXML.AllowUserToDeleteRows = false;
            this.dgvXML.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvXML.Location = new System.Drawing.Point(3, 16);
            this.dgvXML.Name = "dgvXML";
            this.dgvXML.ReadOnly = true;
            this.dgvXML.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvXML.Size = new System.Drawing.Size(719, 331);
            this.dgvXML.TabIndex = 0;
            
            
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvView);
            this.groupBox5.Location = new System.Drawing.Point(743, 192);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(725, 350);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "💾 Dados da View (Sistema)";
            // 
            // dgvView
            // 
            this.dgvView.AllowUserToAddRows = false;
            this.dgvView.AllowUserToDeleteRows = false;
            this.dgvView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvView.Location = new System.Drawing.Point(3, 16);
            this.dgvView.Name = "dgvView";
            this.dgvView.ReadOnly = true;
            this.dgvView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvView.Size = new System.Drawing.Size(719, 331);
            this.dgvView.TabIndex = 0;
            // 
            // btnCarregarView
            // 
            this.btnCarregarView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnCarregarView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarregarView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCarregarView.ForeColor = System.Drawing.Color.White;
            this.btnCarregarView.Location = new System.Drawing.Point(746, 613);
            this.btnCarregarView.Name = "btnCarregarView";
            this.btnCarregarView.Size = new System.Drawing.Size(237, 35);
            this.btnCarregarView.TabIndex = 10;
            this.btnCarregarView.Text = "🔄 Carregar Dados da View";
            this.btnCarregarView.UseVisualStyleBackColor = false;
            this.btnCarregarView.Click += new System.EventHandler(this.btnCarregarView_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblValorTotal);
            this.groupBox3.Controls.Add(this.lblQuantidade);
            this.groupBox3.Location = new System.Drawing.Point(12, 548);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(725, 60);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Totalizadores - XMLs";
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotal.Location = new System.Drawing.Point(460, 25);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(155, 17);
            this.lblValorTotal.TabIndex = 1;
            this.lblValorTotal.Text = "Valor Total: R$ 0,00";
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantidade.Location = new System.Drawing.Point(16, 25);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(181, 17);
            this.lblQuantidade.TabIndex = 0;
            this.lblQuantidade.Text = "Quantidade de Notas: 0";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblValorTotalView);
            this.groupBox6.Controls.Add(this.lblQuantidadeView);
            this.groupBox6.Location = new System.Drawing.Point(743, 548);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(725, 60);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Totalizadores - View";
            // 
            // lblValorTotalView
            // 
            this.lblValorTotalView.AutoSize = true;
            this.lblValorTotalView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotalView.Location = new System.Drawing.Point(460, 25);
            this.lblValorTotalView.Name = "lblValorTotalView";
            this.lblValorTotalView.Size = new System.Drawing.Size(155, 17);
            this.lblValorTotalView.TabIndex = 1;
            this.lblValorTotalView.Text = "Valor Total: R$ 0,00";
            // 
            // lblQuantidadeView
            // 
            this.lblQuantidadeView.AutoSize = true;
            this.lblQuantidadeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantidadeView.Location = new System.Drawing.Point(16, 25);
            this.lblQuantidadeView.Name = "lblQuantidadeView";
            this.lblQuantidadeView.Size = new System.Drawing.Size(181, 17);
            this.lblQuantidadeView.TabIndex = 0;
            this.lblQuantidadeView.Text = "Quantidade de Notas: 0";
            // 
            // btnValidar
            // 
            this.btnValidar.Enabled = false;
            this.btnValidar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidar.Location = new System.Drawing.Point(12, 614);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(150, 35);
            this.btnValidar.TabIndex = 4;
            this.btnValidar.Text = "✓ Comparar";
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // btnConfigurar
            // 
            this.btnConfigurar.Location = new System.Drawing.Point(1318, 614);
            this.btnConfigurar.Name = "btnConfigurar";
            this.btnConfigurar.Size = new System.Drawing.Size(150, 35);
            this.btnConfigurar.TabIndex = 5;
            this.btnConfigurar.Text = "⚙ Configurar Banco";
            this.btnConfigurar.UseVisualStyleBackColor = true;
            this.btnConfigurar.Click += new System.EventHandler(this.btnConfigurar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(168, 614);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(150, 35);
            this.btnLimpar.TabIndex = 6;
            this.btnLimpar.Text = "🗑 Limpar Listas";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(324, 614);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(150, 35);
            this.btnExportar.TabIndex = 7;
            this.btnExportar.Text = "📊 Exportar CSV";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.cmbLoja);
            this.groupBox4.Controls.Add(this.dtpDataFinal);
            this.groupBox4.Controls.Add(this.dtpDataInicial);
            this.groupBox4.Controls.Add(this.lblDataFinal);
            this.groupBox4.Controls.Add(this.lblDataInicial);
            this.groupBox4.Controls.Add(this.txtNomeView);
            this.groupBox4.Controls.Add(this.lblNomeView);
            this.groupBox4.Location = new System.Drawing.Point(12, 122);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1456, 64);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configuração da Validação";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(858, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Loja Origem:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(861, 34);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(140, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(686, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Loja Origem:";
            // 
            // cmbLoja
            // 
            this.cmbLoja.FormattingEnabled = true;
            this.cmbLoja.Location = new System.Drawing.Point(689, 34);
            this.cmbLoja.Name = "cmbLoja";
            this.cmbLoja.Size = new System.Drawing.Size(140, 21);
            this.cmbLoja.TabIndex = 6;
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Location = new System.Drawing.Point(544, 35);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(120, 20);
            this.dtpDataFinal.TabIndex = 5;
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Location = new System.Drawing.Point(389, 35);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(120, 20);
            this.dtpDataInicial.TabIndex = 4;
            // 
            // lblDataFinal
            // 
            this.lblDataFinal.AutoSize = true;
            this.lblDataFinal.Location = new System.Drawing.Point(541, 19);
            this.lblDataFinal.Name = "lblDataFinal";
            this.lblDataFinal.Size = new System.Drawing.Size(58, 13);
            this.lblDataFinal.TabIndex = 3;
            this.lblDataFinal.Text = "Data Final:";
            // 
            // lblDataInicial
            // 
            this.lblDataInicial.AutoSize = true;
            this.lblDataInicial.Location = new System.Drawing.Point(386, 19);
            this.lblDataInicial.Name = "lblDataInicial";
            this.lblDataInicial.Size = new System.Drawing.Size(63, 13);
            this.lblDataInicial.TabIndex = 2;
            this.lblDataInicial.Text = "Data Inicial:";
            // 
            // txtNomeView
            // 
            this.txtNomeView.Location = new System.Drawing.Point(16, 35);
            this.txtNomeView.Name = "txtNomeView";
            this.txtNomeView.Size = new System.Drawing.Size(300, 20);
            this.txtNomeView.TabIndex = 1;
            this.txtNomeView.Text = "vw_NFCe";
            // 
            // lblNomeView
            // 
            this.lblNomeView.AutoSize = true;
            this.lblNomeView.Location = new System.Drawing.Point(13, 19);
            this.lblNomeView.Name = "lblNomeView";
            this.lblNomeView.Size = new System.Drawing.Size(79, 13);
            this.lblNomeView.TabIndex = 0;
            this.lblNomeView.Text = "Nome da View:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 661);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnCarregarView);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnConfigurar);
            this.Controls.Add(this.btnValidar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validador de NFCe";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvXML)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelecionarPasta;
        private System.Windows.Forms.TextBox txtCaminhoPasta;
        private System.Windows.Forms.Label lblCaminhoPasta;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvXML;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblValorTotalView;
        private System.Windows.Forms.Label lblQuantidadeView;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.Button btnConfigurar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnCarregarView;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label lblDataFinal;
        private System.Windows.Forms.Label lblDataInicial;
        private System.Windows.Forms.TextBox txtNomeView;
        private System.Windows.Forms.Label lblNomeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLoja;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}