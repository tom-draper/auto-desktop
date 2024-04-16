namespace auto_desktop;

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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        dgvActions = new DataGridView();
        Action = new DataGridViewComboBoxColumn();
        Count = new DataGridViewTextBoxColumn();
        lblRepeat = new Label();
        txtRepeat = new TextBox();
        lblTimes = new Label();
        rtbTerminal = new RichTextBox();
        btnRun = new Button();
        lblSeconds = new Label();
        txtDelay = new TextBox();
        label2 = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvActions).BeginInit();
        SuspendLayout();
        // 
        // dgvActions
        // 
        dataGridViewCellStyle1.BackColor = Color.FromArgb(15, 21, 36);
        dataGridViewCellStyle1.ForeColor = Color.White;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = Color.White;
        dgvActions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvActions.BackgroundColor = Color.FromArgb(28, 35, 50);
        dgvActions.BorderStyle = BorderStyle.None;
        dgvActions.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(14, 21, 37);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dgvActions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvActions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvActions.Columns.AddRange(new DataGridViewColumn[] { Action, Count });
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = Color.FromArgb(14, 21, 37);
        dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle5.ForeColor = Color.White;
        dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
        dgvActions.DefaultCellStyle = dataGridViewCellStyle5;
        dgvActions.EnableHeadersVisualStyles = false;
        dgvActions.GridColor = Color.Black;
        dgvActions.Location = new Point(12, 50);
        dgvActions.Name = "dgvActions";
        dgvActions.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = Color.FromArgb(14, 21, 25);
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle6.ForeColor = Color.White;
        dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
        dgvActions.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
        dataGridViewCellStyle7.BackColor = Color.FromArgb(14, 21, 37);
        dataGridViewCellStyle7.ForeColor = Color.White;
        dgvActions.RowsDefaultCellStyle = dataGridViewCellStyle7;
        dgvActions.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(14, 21, 37);
        dgvActions.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
        dgvActions.Size = new Size(388, 341);
        dgvActions.TabIndex = 0;
        // 
        // Action
        // 
        Action.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(14, 21, 37);
        dataGridViewCellStyle3.ForeColor = Color.White;
        Action.DefaultCellStyle = dataGridViewCellStyle3;
        Action.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
        Action.FlatStyle = FlatStyle.Flat;
        Action.HeaderText = "Action";
        Action.Name = "Action";
        // 
        // Count
        // 
        dataGridViewCellStyle4.BackColor = Color.FromArgb(14, 21, 37);
        dataGridViewCellStyle4.ForeColor = Color.White;
        dataGridViewCellStyle4.NullValue = "1";
        Count.DefaultCellStyle = dataGridViewCellStyle4;
        Count.HeaderText = "Count";
        Count.Name = "Count";
        Count.SortMode = DataGridViewColumnSortMode.NotSortable;
        Count.Width = 80;
        // 
        // lblRepeat
        // 
        lblRepeat.AutoSize = true;
        lblRepeat.ForeColor = Color.White;
        lblRepeat.Location = new Point(14, 24);
        lblRepeat.Name = "lblRepeat";
        lblRepeat.Size = new Size(43, 15);
        lblRepeat.TabIndex = 1;
        lblRepeat.Text = "Repeat";
        // 
        // txtRepeat
        // 
        txtRepeat.BackColor = Color.FromArgb(15, 21, 36);
        txtRepeat.BorderStyle = BorderStyle.None;
        txtRepeat.ForeColor = Color.White;
        txtRepeat.Location = new Point(59, 24);
        txtRepeat.Name = "txtRepeat";
        txtRepeat.Size = new Size(37, 16);
        txtRepeat.TabIndex = 2;
        txtRepeat.Text = "1";
        txtRepeat.TextAlign = HorizontalAlignment.Center;
        txtRepeat.TextChanged += txtRepeat_TextChanged;
        // 
        // lblTimes
        // 
        lblTimes.AutoSize = true;
        lblTimes.ForeColor = Color.White;
        lblTimes.Location = new Point(99, 24);
        lblTimes.Name = "lblTimes";
        lblTimes.Size = new Size(31, 15);
        lblTimes.TabIndex = 3;
        lblTimes.Text = "time";
        // 
        // rtbTerminal
        // 
        rtbTerminal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        rtbTerminal.BackColor = Color.FromArgb(15, 21, 36);
        rtbTerminal.BorderStyle = BorderStyle.None;
        rtbTerminal.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        rtbTerminal.ForeColor = Color.White;
        rtbTerminal.Location = new Point(414, -13);
        rtbTerminal.Margin = new Padding(1);
        rtbTerminal.Name = "rtbTerminal";
        rtbTerminal.Size = new Size(385, 450);
        rtbTerminal.TabIndex = 4;
        rtbTerminal.Text = "\n";
        // 
        // btnRun
        // 
        btnRun.Location = new Point(325, 397);
        btnRun.Name = "btnRun";
        btnRun.Size = new Size(75, 23);
        btnRun.TabIndex = 5;
        btnRun.Text = "Run";
        btnRun.UseVisualStyleBackColor = true;
        btnRun.Click += Run_Click;
        // 
        // lblSeconds
        // 
        lblSeconds.AutoSize = true;
        lblSeconds.ForeColor = Color.White;
        lblSeconds.Location = new Point(99, 401);
        lblSeconds.Name = "lblSeconds";
        lblSeconds.Size = new Size(50, 15);
        lblSeconds.TabIndex = 8;
        lblSeconds.Text = "seconds";
        // 
        // txtDelay
        // 
        txtDelay.BackColor = Color.FromArgb(15, 21, 36);
        txtDelay.BorderStyle = BorderStyle.None;
        txtDelay.ForeColor = Color.White;
        txtDelay.Location = new Point(59, 401);
        txtDelay.Name = "txtDelay";
        txtDelay.Size = new Size(37, 16);
        txtDelay.TabIndex = 7;
        txtDelay.Text = "5";
        txtDelay.TextAlign = HorizontalAlignment.Center;
        txtDelay.TextChanged += txtDelay_TextChanged;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.ForeColor = Color.White;
        label2.Location = new Point(14, 401);
        label2.Name = "label2";
        label2.Size = new Size(44, 15);
        label2.TabIndex = 6;
        label2.Text = "Start in";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(28, 35, 50);
        ClientSize = new Size(797, 432);
        Controls.Add(lblSeconds);
        Controls.Add(txtDelay);
        Controls.Add(label2);
        Controls.Add(btnRun);
        Controls.Add(rtbTerminal);
        Controls.Add(lblTimes);
        Controls.Add(txtRepeat);
        Controls.Add(lblRepeat);
        Controls.Add(dgvActions);
        Name = "Form1";
        Text = "Form1";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dgvActions).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DataGridView dgvActions;
    private Label lblRepeat;
    private TextBox txtRepeat;
    private Label lblTimes;
    private RichTextBox rtbTerminal;
    private Button btnRun;
    private Label lblSeconds;
    private TextBox txtDelay;
    private Label label2;
    private DataGridViewComboBoxColumn Action;
    private DataGridViewTextBoxColumn Count;
}
