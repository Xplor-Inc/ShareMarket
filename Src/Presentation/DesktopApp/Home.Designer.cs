namespace ShareMarket.DesktopApp
{
    partial class Home
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
            grdRSIData = new DataGridView();
            Code = new DataGridViewLinkColumn();
            StockName = new DataGridViewTextBoxColumn();
            RSIUpdateOn = new DataGridViewTextBoxColumn();
            Close = new DataGridViewTextBoxColumn();
            Down = new DataGridViewTextBoxColumn();
            RSI = new DataGridViewTextBoxColumn();
            dataGridView1 = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            RSIDate = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            BtnReferesh = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)grdRSIData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // grdRSIData
            // 
            grdRSIData.AllowUserToAddRows = false;
            grdRSIData.AllowUserToDeleteRows = false;
            grdRSIData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grdRSIData.Columns.AddRange(new DataGridViewColumn[] { Code, StockName, RSIUpdateOn, Close, Down, RSI });
            grdRSIData.Location = new Point(1, 35);
            grdRSIData.Name = "grdRSIData";
            grdRSIData.ReadOnly = true;
            grdRSIData.RowHeadersWidth = 51;
            grdRSIData.Size = new Size(821, 720);
            grdRSIData.TabIndex = 0;
            grdRSIData.CellContentDoubleClick += GrdRSIData_CellContentDoubleClick;
            // 
            // Code
            // 
            Code.DataPropertyName = "Code";
            Code.HeaderText = "Code";
            Code.LinkBehavior = LinkBehavior.HoverUnderline;
            Code.MinimumWidth = 6;
            Code.Name = "Code";
            Code.ReadOnly = true;
            Code.Resizable = DataGridViewTriState.True;
            Code.SortMode = DataGridViewColumnSortMode.Automatic;
            Code.Width = 125;
            // 
            // StockName
            // 
            StockName.DataPropertyName = "Name";
            StockName.HeaderText = "Name";
            StockName.MinimumWidth = 6;
            StockName.Name = "StockName";
            StockName.ReadOnly = true;
            StockName.Width = 125;
            // 
            // RSIUpdateOn
            // 
            RSIUpdateOn.DataPropertyName = "RSIUpdateOn";
            RSIUpdateOn.HeaderText = "RSI UpdateOn";
            RSIUpdateOn.MinimumWidth = 6;
            RSIUpdateOn.Name = "RSIUpdateOn";
            RSIUpdateOn.ReadOnly = true;
            RSIUpdateOn.Width = 125;
            // 
            // Close
            // 
            Close.DataPropertyName = "PreviousClose";
            Close.HeaderText = "Close";
            Close.MinimumWidth = 6;
            Close.Name = "Close";
            Close.ReadOnly = true;
            Close.Width = 125;
            // 
            // Down
            // 
            Down.DataPropertyName = "CYHYLPercent";
            Down.HeaderText = "Down";
            Down.MinimumWidth = 6;
            Down.Name = "Down";
            Down.ReadOnly = true;
            Down.Width = 125;
            // 
            // RSI
            // 
            RSI.DataPropertyName = "RSI";
            RSI.HeaderText = "RSI";
            RSI.MinimumWidth = 6;
            RSI.Name = "RSI";
            RSI.ReadOnly = true;
            RSI.Width = 125;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, RSIDate, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7 });
            dataGridView1.Location = new Point(828, 35);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(932, 720);
            dataGridView1.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "Code";
            dataGridViewTextBoxColumn1.HeaderText = "Code";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 125;
            // 
            // RSIDate
            // 
            RSIDate.DataPropertyName = "Date";
            RSIDate.HeaderText = "Date";
            RSIDate.MinimumWidth = 6;
            RSIDate.Name = "RSIDate";
            RSIDate.ReadOnly = true;
            RSIDate.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "Open";
            dataGridViewTextBoxColumn3.HeaderText = "Open";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "Low";
            dataGridViewTextBoxColumn4.HeaderText = "Low";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "High";
            dataGridViewTextBoxColumn5.HeaderText = "High";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Width = 125;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.DataPropertyName = "Close";
            dataGridViewTextBoxColumn6.HeaderText = "Close";
            dataGridViewTextBoxColumn6.MinimumWidth = 6;
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Width = 125;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.DataPropertyName = "RSI";
            dataGridViewTextBoxColumn7.HeaderText = "RSI";
            dataGridViewTextBoxColumn7.MinimumWidth = 6;
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            dataGridViewTextBoxColumn7.Width = 125;
            // 
            // BtnReferesh
            // 
            BtnReferesh.Font = new Font("Segoe UI", 11F);
            BtnReferesh.ForeColor = Color.FromArgb(0, 192, 0);
            BtnReferesh.Location = new Point(2, -1);
            BtnReferesh.Name = "BtnReferesh";
            BtnReferesh.Size = new Size(151, 36);
            BtnReferesh.TabIndex = 6;
            BtnReferesh.Text = "Refresh Data";
            BtnReferesh.TextAlign = ContentAlignment.TopCenter;
            BtnReferesh.UseVisualStyleBackColor = true;
            BtnReferesh.Click += BtnReferesh_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 11F);
            button1.ForeColor = Color.FromArgb(0, 192, 0);
            button1.Location = new Point(671, -1);
            button1.Name = "button1";
            button1.Size = new Size(151, 36);
            button1.TabIndex = 7;
            button1.Text = "Next Page";
            button1.TextAlign = ContentAlignment.TopCenter;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1761, 771);
            Controls.Add(button1);
            Controls.Add(BtnReferesh);
            Controls.Add(dataGridView1);
            Controls.Add(grdRSIData);
            Name = "Home";
            Load += Home_Load;
            ((System.ComponentModel.ISupportInitialize)grdRSIData).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grdRSIData;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn RSIDate;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private Button BtnReferesh;
        private DataGridViewLinkColumn Code;
        private DataGridViewTextBoxColumn StockName;
        private DataGridViewTextBoxColumn RSIUpdateOn;
        private DataGridViewTextBoxColumn Close;
        private DataGridViewTextBoxColumn Down;
        private DataGridViewTextBoxColumn RSI;
        private Button button1;
    }
}
