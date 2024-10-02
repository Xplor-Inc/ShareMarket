namespace ShareMarket.WinApp;

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
        label1 = new Label();
        LblStatus = new Label();
        BtnLTP = new Button();
        BtnYahooHistory = new Button();
        BtnSkipYahooError = new Button();
        button1 = new Button();
        Grd102050DMA = new DataGridView();
        btnFundamentals = new Button();
        GrdAnalysis = new DataGridView();
        button2 = new Button();
        button3 = new Button();
        Grd102050DMA_Code = new DataGridViewTextBoxColumn();
        Grd102050DMA_5DMA = new DataGridViewTextBoxColumn();
        Grd102050DMA_10DMA = new DataGridViewTextBoxColumn();
        Grd102050DMA_20DMA = new DataGridViewTextBoxColumn();
        Grd102050DMA_LTP = new DataGridViewTextBoxColumn();
        Grd102050DMA_RSI = new DataGridViewTextBoxColumn();
        GRD5020RankByGroww = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)Grd102050DMA).BeginInit();
        ((System.ComponentModel.ISupportInitialize)GrdAnalysis).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 11F);
        label1.Location = new Point(12, 46);
        label1.Name = "label1";
        label1.Size = new Size(66, 25);
        label1.TabIndex = 0;
        label1.Text = "Status:";
        // 
        // LblStatus
        // 
        LblStatus.AutoSize = true;
        LblStatus.Font = new Font("Segoe UI", 11F);
        LblStatus.ForeColor = Color.FromArgb(0, 192, 0);
        LblStatus.Location = new Point(84, 48);
        LblStatus.Name = "LblStatus";
        LblStatus.Size = new Size(45, 25);
        LblStatus.TabIndex = 1;
        LblStatus.Text = "Text";
        // 
        // BtnLTP
        // 
        BtnLTP.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        BtnLTP.ForeColor = Color.FromArgb(255, 128, 0);
        BtnLTP.Location = new Point(1, 2);
        BtnLTP.Name = "BtnLTP";
        BtnLTP.Size = new Size(148, 37);
        BtnLTP.TabIndex = 2;
        BtnLTP.Text = "LTP By Groww";
        BtnLTP.TextAlign = ContentAlignment.MiddleLeft;
        BtnLTP.UseVisualStyleBackColor = true;
        BtnLTP.Click += BtnGrowwLTP_Click;
        // 
        // BtnYahooHistory
        // 
        BtnYahooHistory.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        BtnYahooHistory.ForeColor = Color.FromArgb(255, 128, 0);
        BtnYahooHistory.Location = new Point(465, 3);
        BtnYahooHistory.Name = "BtnYahooHistory";
        BtnYahooHistory.Size = new Size(183, 36);
        BtnYahooHistory.TabIndex = 3;
        BtnYahooHistory.Text = "History By Yahoo";
        BtnYahooHistory.TextAlign = ContentAlignment.MiddleLeft;
        BtnYahooHistory.UseVisualStyleBackColor = true;
        BtnYahooHistory.Click += BtnYahooHistory_Click;
        // 
        // BtnSkipYahooError
        // 
        BtnSkipYahooError.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        BtnSkipYahooError.ForeColor = Color.FromArgb(255, 128, 0);
        BtnSkipYahooError.Location = new Point(1201, 4);
        BtnSkipYahooError.Name = "BtnSkipYahooError";
        BtnSkipYahooError.Size = new Size(131, 36);
        BtnSkipYahooError.TabIndex = 4;
        BtnSkipYahooError.Text = "Skip Yahoo";
        BtnSkipYahooError.TextAlign = ContentAlignment.MiddleLeft;
        BtnSkipYahooError.UseVisualStyleBackColor = true;
        BtnSkipYahooError.Click += BtnSkipYahooError_Click;
        // 
        // button1
        // 
        button1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        button1.ForeColor = Color.FromArgb(255, 128, 0);
        button1.Location = new Point(155, 2);
        button1.Name = "button1";
        button1.Size = new Size(304, 37);
        button1.TabIndex = 9;
        button1.Text = "Sync History for Equiety Pandit";
        button1.TextAlign = ContentAlignment.MiddleLeft;
        button1.UseVisualStyleBackColor = true;
        button1.Click += EquityPandit_Click;
        // 
        // Grd102050DMA
        // 
        Grd102050DMA.AllowUserToAddRows = false;
        Grd102050DMA.AllowUserToDeleteRows = false;
        Grd102050DMA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        Grd102050DMA.Columns.AddRange(new DataGridViewColumn[] { Grd102050DMA_Code, Grd102050DMA_5DMA, Grd102050DMA_10DMA, Grd102050DMA_20DMA, Grd102050DMA_LTP, Grd102050DMA_RSI, GRD5020RankByGroww });
        Grd102050DMA.Location = new Point(12, 76);
        Grd102050DMA.Name = "Grd102050DMA";
        Grd102050DMA.ReadOnly = true;
        Grd102050DMA.RowHeadersWidth = 51;
        Grd102050DMA.Size = new Size(1007, 663);
        Grd102050DMA.TabIndex = 10;
        Grd102050DMA.RowValidating += Grd102050DMA_RowValidating;
        // 
        // btnFundamentals
        // 
        btnFundamentals.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnFundamentals.ForeColor = Color.FromArgb(255, 128, 0);
        btnFundamentals.Location = new Point(654, 2);
        btnFundamentals.Name = "btnFundamentals";
        btnFundamentals.Size = new Size(265, 37);
        btnFundamentals.TabIndex = 11;
        btnFundamentals.Text = "Sync Fundamentals Equiety Pandit";
        btnFundamentals.TextAlign = ContentAlignment.MiddleLeft;
        btnFundamentals.UseVisualStyleBackColor = true;
        btnFundamentals.Click += BtnFundamentals_Click;
        // 
        // GrdAnalysis
        // 
        GrdAnalysis.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        GrdAnalysis.Location = new Point(1139, 46);
        GrdAnalysis.Name = "GrdAnalysis";
        GrdAnalysis.RowHeadersWidth = 51;
        GrdAnalysis.Size = new Size(611, 693);
        GrdAnalysis.TabIndex = 12;
        // 
        // button2
        // 
        button2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        button2.ForeColor = Color.FromArgb(255, 128, 0);
        button2.Location = new Point(925, 3);
        button2.Name = "button2";
        button2.Size = new Size(126, 37);
        button2.TabIndex = 13;
        button2.Text = "Trade Book";
        button2.TextAlign = ContentAlignment.MiddleLeft;
        button2.UseVisualStyleBackColor = true;
        button2.Click += BtnTradeBook;
        // 
        // button3
        // 
        button3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        button3.ForeColor = Color.FromArgb(255, 128, 0);
        button3.Location = new Point(1057, 3);
        button3.Name = "button3";
        button3.Size = new Size(126, 37);
        button3.TabIndex = 14;
        button3.Text = "Calculations";
        button3.TextAlign = ContentAlignment.MiddleLeft;
        button3.UseVisualStyleBackColor = true;
        button3.Click += BtnCalculation;
        // 
        // Grd102050DMA_Code
        // 
        Grd102050DMA_Code.DataPropertyName = "Code";
        Grd102050DMA_Code.HeaderText = "Code";
        Grd102050DMA_Code.MinimumWidth = 6;
        Grd102050DMA_Code.Name = "Grd102050DMA_Code";
        Grd102050DMA_Code.ReadOnly = true;
        Grd102050DMA_Code.Width = 125;
        // 
        // Grd102050DMA_5DMA
        // 
        Grd102050DMA_5DMA.DataPropertyName = "DMA5";
        Grd102050DMA_5DMA.HeaderText = "5DMA";
        Grd102050DMA_5DMA.MinimumWidth = 6;
        Grd102050DMA_5DMA.Name = "Grd102050DMA_5DMA";
        Grd102050DMA_5DMA.ReadOnly = true;
        Grd102050DMA_5DMA.Width = 125;
        // 
        // Grd102050DMA_10DMA
        // 
        Grd102050DMA_10DMA.DataPropertyName = "DMA10";
        Grd102050DMA_10DMA.HeaderText = "10 DMA";
        Grd102050DMA_10DMA.MinimumWidth = 6;
        Grd102050DMA_10DMA.Name = "Grd102050DMA_10DMA";
        Grd102050DMA_10DMA.ReadOnly = true;
        Grd102050DMA_10DMA.Width = 125;
        // 
        // Grd102050DMA_20DMA
        // 
        Grd102050DMA_20DMA.DataPropertyName = "DMA20";
        Grd102050DMA_20DMA.HeaderText = "20DMA";
        Grd102050DMA_20DMA.MinimumWidth = 6;
        Grd102050DMA_20DMA.Name = "Grd102050DMA_20DMA";
        Grd102050DMA_20DMA.ReadOnly = true;
        Grd102050DMA_20DMA.Width = 125;
        // 
        // Grd102050DMA_LTP
        // 
        Grd102050DMA_LTP.DataPropertyName = "LTP";
        Grd102050DMA_LTP.HeaderText = "LTP";
        Grd102050DMA_LTP.MinimumWidth = 6;
        Grd102050DMA_LTP.Name = "Grd102050DMA_LTP";
        Grd102050DMA_LTP.ReadOnly = true;
        Grd102050DMA_LTP.Width = 125;
        // 
        // Grd102050DMA_RSI
        // 
        Grd102050DMA_RSI.DataPropertyName = "RSI14EMADiff";
        Grd102050DMA_RSI.HeaderText = "RSI";
        Grd102050DMA_RSI.MinimumWidth = 6;
        Grd102050DMA_RSI.Name = "Grd102050DMA_RSI";
        Grd102050DMA_RSI.ReadOnly = true;
        Grd102050DMA_RSI.Width = 125;
        // 
        // GRD5020RankByGroww
        // 
        GRD5020RankByGroww.DataPropertyName = "RankByGroww";
        GRD5020RankByGroww.HeaderText = "Groww Rank";
        GRD5020RankByGroww.MinimumWidth = 6;
        GRD5020RankByGroww.Name = "GRD5020RankByGroww";
        GRD5020RankByGroww.ReadOnly = true;
        GRD5020RankByGroww.Width = 125;
        // 
        // Home
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1798, 763);
        Controls.Add(button3);
        Controls.Add(button2);
        Controls.Add(GrdAnalysis);
        Controls.Add(btnFundamentals);
        Controls.Add(Grd102050DMA);
        Controls.Add(button1);
        Controls.Add(BtnSkipYahooError);
        Controls.Add(BtnYahooHistory);
        Controls.Add(BtnLTP);
        Controls.Add(LblStatus);
        Controls.Add(label1);
        Name = "Home";
        Text = "Share Market";
        WindowState = FormWindowState.Maximized;
        Load += Home_Load;
        ((System.ComponentModel.ISupportInitialize)Grd102050DMA).EndInit();
        ((System.ComponentModel.ISupportInitialize)GrdAnalysis).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label LblStatus;
    private Button BtnLTP;
    private Button BtnYahooHistory;
    private Button BtnSkipYahooError;
    private Button Btn15DMA;
    private Button Btn10DMA;
    private Button button1;
    private DataGridView Grd102050DMA;
    private Button btnFundamentals;
    private DataGridView GrdAnalysis;
    private Button button2;
    private Button button3;
    private DataGridViewTextBoxColumn Grd102050DMA_Code;
    private DataGridViewTextBoxColumn Grd102050DMA_5DMA;
    private DataGridViewTextBoxColumn Grd102050DMA_10DMA;
    private DataGridViewTextBoxColumn Grd102050DMA_20DMA;
    private DataGridViewTextBoxColumn Grd102050DMA_LTP;
    private DataGridViewTextBoxColumn Grd102050DMA_RSI;
    private DataGridViewTextBoxColumn GRD5020RankByGroww;
}
