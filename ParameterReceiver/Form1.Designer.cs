namespace ParameterReceiver
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvSerieA = new DataGridView();
            Code = new DataGridViewTextBoxColumn();
            SymboleIndex = new DataGridViewTextBoxColumn();
            IndexHex = new DataGridViewTextBoxColumn();
            ValeurHex = new DataGridViewTextBoxColumn();
            Valeur = new DataGridViewTextBoxColumn();
            txbLog = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            txb410 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvSerieA).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvSerieA
            // 
            dgvSerieA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSerieA.Columns.AddRange(new DataGridViewColumn[] { Code, SymboleIndex, IndexHex, ValeurHex, Valeur });
            dgvSerieA.Dock = DockStyle.Fill;
            dgvSerieA.Location = new Point(3, 3);
            dgvSerieA.Name = "dgvSerieA";
            dgvSerieA.Size = new Size(723, 308);
            dgvSerieA.TabIndex = 2;
            // 
            // Code
            // 
            Code.HeaderText = "Code";
            Code.Name = "Code";
            // 
            // SymboleIndex
            // 
            SymboleIndex.HeaderText = "SymboleIndex";
            SymboleIndex.Name = "SymboleIndex";
            // 
            // IndexHex
            // 
            IndexHex.HeaderText = "IndexHex";
            IndexHex.Name = "IndexHex";
            // 
            // ValeurHex
            // 
            ValeurHex.HeaderText = "ValeurHex";
            ValeurHex.Name = "ValeurHex";
            // 
            // Valeur
            // 
            Valeur.HeaderText = "Valeur";
            Valeur.Name = "Valeur";
            // 
            // txbLog
            // 
            txbLog.Dock = DockStyle.Bottom;
            txbLog.Location = new Point(0, 342);
            txbLog.Multiline = true;
            txbLog.Name = "txbLog";
            txbLog.Size = new Size(737, 95);
            txbLog.TabIndex = 3;
            txbLog.TextChanged += txbLog_TextChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(737, 342);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dgvSerieA);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(729, 314);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabTM";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(txb410);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(729, 314);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tab410";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // txb410
            // 
            txb410.Dock = DockStyle.Fill;
            txb410.Location = new Point(3, 3);
            txb410.Multiline = true;
            txb410.Name = "txb410";
            txb410.Size = new Size(723, 308);
            txb410.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 437);
            Controls.Add(tabControl1);
            Controls.Add(txbLog);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSerieA).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvSerieA;
        private DataGridViewTextBoxColumn Code;
        private DataGridViewTextBoxColumn SymboleIndex;
        private DataGridViewTextBoxColumn IndexHex;
        private DataGridViewTextBoxColumn ValeurHex;
        private DataGridViewTextBoxColumn Valeur;
        private TextBox txbLog;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox txb410;
    }
}
