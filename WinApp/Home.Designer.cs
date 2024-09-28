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
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(568, 43);
        label1.Name = "label1";
        label1.Size = new Size(52, 20);
        label1.TabIndex = 0;
        label1.Text = "Status:";
        // 
        // LblStatus
        // 
        LblStatus.AutoSize = true;
        LblStatus.ForeColor = Color.FromArgb(0, 192, 0);
        LblStatus.Location = new Point(617, 43);
        LblStatus.Name = "LblStatus";
        LblStatus.Size = new Size(36, 20);
        LblStatus.TabIndex = 1;
        LblStatus.Text = "Text";
        // 
        // BtnLTP
        // 
        BtnLTP.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        BtnLTP.ForeColor = Color.FromArgb(255, 128, 0);
        BtnLTP.Location = new Point(12, 33);
        BtnLTP.Name = "BtnLTP";
        BtnLTP.Size = new Size(304, 37);
        BtnLTP.TabIndex = 2;
        BtnLTP.Text = "LTP By Groww";
        BtnLTP.TextAlign = ContentAlignment.MiddleLeft;
        BtnLTP.UseVisualStyleBackColor = true;
        BtnLTP.Click += BtnLTP_Click;
        // 
        // BtnYahooHistory
        // 
        BtnYahooHistory.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        BtnYahooHistory.ForeColor = Color.FromArgb(255, 128, 0);
        BtnYahooHistory.Location = new Point(12, 148);
        BtnYahooHistory.Name = "BtnYahooHistory";
        BtnYahooHistory.Size = new Size(304, 36);
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
        BtnSkipYahooError.Location = new Point(12, 223);
        BtnSkipYahooError.Name = "BtnSkipYahooError";
        BtnSkipYahooError.Size = new Size(304, 36);
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
        button1.Location = new Point(12, 87);
        button1.Name = "button1";
        button1.Size = new Size(304, 37);
        button1.TabIndex = 9;
        button1.Text = "Sync History for Equiety Pandit";
        button1.TextAlign = ContentAlignment.MiddleLeft;
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // Home
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1798, 763);
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
}
