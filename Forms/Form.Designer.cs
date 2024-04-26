namespace auto_desktop;

partial class Form
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
        dgvActions = new DataGridView();
        Action = new DataGridViewComboBoxColumn();
        Count = new DataGridViewTextBoxColumn();
        txtRepeat = new TextBox();
        rtbTerminal = new RichTextBox();
        btnRun = new Button();
        lblSeconds = new Label();
        txtDelay = new TextBox();
        label2 = new Label();
        pictureBox1 = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)dgvActions).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // dgvActions
        // 
        dataGridViewCellStyle1.BackColor = Color.FromArgb(15, 21, 36);
        dataGridViewCellStyle1.ForeColor = Color.White;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = Color.White;
        dgvActions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvActions.BackgroundColor = Color.FromArgb(29, 35, 50);
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
        dgvActions.EditingControlShowing += dgvActions_EditingControlShowing;
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
        // txtRepeat
        // 
        txtRepeat.BackColor = Color.FromArgb(15, 21, 36);
        txtRepeat.BorderStyle = BorderStyle.None;
        txtRepeat.ForeColor = Color.White;
        txtRepeat.Location = new Point(45, 25);
        txtRepeat.Name = "txtRepeat";
        txtRepeat.Size = new Size(37, 16);
        txtRepeat.TabIndex = 2;
        txtRepeat.Text = "1";
        txtRepeat.TextAlign = HorizontalAlignment.Center;
        // 
        // rtbTerminal
        // 
        rtbTerminal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        rtbTerminal.BackColor = Color.FromArgb(15, 21, 36);
        rtbTerminal.BorderStyle = BorderStyle.None;
        rtbTerminal.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        rtbTerminal.ForeColor = Color.White;
        rtbTerminal.Location = new Point(414, -22);
        rtbTerminal.Margin = new Padding(1);
        rtbTerminal.Name = "rtbTerminal";
        rtbTerminal.ReadOnly = true;
        rtbTerminal.Size = new Size(385, 459);
        rtbTerminal.TabIndex = 4;
        rtbTerminal.Text = "\n";
        // 
        // btnRun
        // 
        btnRun.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnRun.FlatStyle = FlatStyle.System;
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
        lblSeconds.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
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
        txtDelay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
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
        label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        label2.AutoSize = true;
        label2.ForeColor = Color.White;
        label2.Location = new Point(14, 401);
        label2.Name = "label2";
        label2.Size = new Size(44, 15);
        label2.TabIndex = 6;
        label2.Text = "Start in";
        // 
        // pictureBox1
        // 
        pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
        pictureBox1.Location = new Point(14, 20);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(29, 23);
        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.TabIndex = 9;
        pictureBox1.TabStop = false;
        // 
        // Form
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(29, 35, 50);
        ClientSize = new Size(797, 432);
        Controls.Add(pictureBox1);
        Controls.Add(lblSeconds);
        Controls.Add(txtDelay);
        Controls.Add(label2);
        Controls.Add(btnRun);
        Controls.Add(rtbTerminal);
        Controls.Add(txtRepeat);
        Controls.Add(dgvActions);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "Form";
        Text = "Auto Desktop";
        Load += Form_Load;
        ((System.ComponentModel.ISupportInitialize)dgvActions).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DataGridView dgvActions;
    private TextBox txtRepeat;
    private RichTextBox rtbTerminal;
    private Button btnRun;
    private Label lblSeconds;
    private TextBox txtDelay;
    private Label label2;
    private DataGridViewComboBoxColumn Action;
    private DataGridViewTextBoxColumn Count;
    private PictureBox pictureBox1;
}
