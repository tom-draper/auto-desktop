namespace auto_desktop;

public partial class Form1 : Form
{

    bool running = false;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
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

        IndicateActionsRunState();
        rtbTerminal.Text += "Starting...\n";
        Thread.Sleep(1000);
        RunActions();
        IndicateActionsStopState();
    }

    private void IndicateActionsRunState()
    {
        btnRun.Text = "Stop";
        running = true;
    }

    private void IndicateActionsStopState()
    {
        btnRun.Text = "Run";
        running = false;
    }

    [STAThread]
    private void RunActions()
    {
        foreach (DataGridViewRow row in dgvActions.Rows)
        {
            object value = row.Cells[0].Value;
            if (value == null)
                continue;
            string action = value.ToString();

            int count = ParseCount(row);

            for (int i = 0; i < count; i++)
            {
                if (!running)
                    return;
                if (action == "Wait 1 second")
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    SendKeys.Send("test");
                    Thread.Sleep(10);
                }
            }
        }
    }

    private int ParseCount(DataGridViewRow row)
    {
        int count = 1;
        if (row.Cells[1].Value == null)
            _ = int.TryParse(Text, out count);
        return count;
    }
}
