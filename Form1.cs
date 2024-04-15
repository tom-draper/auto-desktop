namespace auto_desktop;

public partial class Form1 : Form
{

    bool running = false;

    public Form1()
    {
        InitializeComponent();
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void dgvActions_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void Run_Click(object sender, EventArgs e)
    {
        if (running)
        {
            btnRun.Text = "Run";
            running = false;
            return;
        }

        btnRun.Text = "Stop";
        running = true;
        rtbTerminal.Text += "Starting...\n";
        Thread.Sleep(1000);
        RunActions();
        running = false;
    }

    [STAThread]
    private void RunActions()
    {
        foreach (DataGridViewRow row in dgvActions.Rows)
        {
            if (row.Cells[0].Value == null)
                continue;

            int count = 1;
            if (row.Cells[1].Value == null)
            {
                _ = int.TryParse(Text, out count);
            }

            for (int i = 0; i < count; i++)
            {
                if (!running)
                    return;
                SendKeys.Send("test");
                Thread.Sleep(10);
            }
        }
    }
}
