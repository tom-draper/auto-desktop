namespace auto_desktop;

public partial class Form1 : Form
{

    bool runningActions = false;

    private readonly System.Threading.SynchronizationContext context;
    public System.Threading.SynchronizationContext Context
    {
        get{ return this.context;}
    }

    public Form1()
    {
        InitializeComponent();
        this.context = WindowsFormsSynchronizationContext.Current;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        LoadActions();
    }


    private void LoadActions()
    {
        if (dgvActions.Columns[0] is DataGridViewComboBoxColumn comboBoxColumn)
        {
            comboBoxColumn.Items.Clear();
            foreach (var action in Actions.GetActions())
                comboBoxColumn.Items.Add(action.Name);
        }
    }

    private void Run_Click(object sender, EventArgs e)
    {
        if (runningActions)
        {
            btnRun.Text = "Run";
            runningActions = false;
            return;
        }

        IndicateActionsRunState();
        int delay = ParseStartDelay();
        LogToUserConsole($"Starting in {delay} seconds...", true);
        Thread.Sleep(delay * 1000);
        RunActions();
        LogToUserConsole("Complete");
        IndicateActionsStopState();
    }

    private int ParseStartDelay()
    {
        _ = int.TryParse(txtDelay.Text, out int delay) ? delay : 5;
        return delay;
    }

    private void LogToUserConsole(string text, bool prefixNewline = false)
    {
        string prefix = prefixNewline ? "\n" : "";
        rtbTerminal.AppendText($"{prefix}[{DateTime.Now.ToString("HH:mm:ss")}] {text}\n");
        // Scroll to end of text
        rtbTerminal.SelectionStart = rtbTerminal.Text.Length;
        rtbTerminal.ScrollToCaret();
        Application.DoEvents();
    }

    private void IndicateActionsRunState()
    {
        btnRun.Text = "Stop";
        runningActions = true;
    }

    private void IndicateActionsStopState()
    {
        btnRun.Text = "Run";
        runningActions = false;
    }

    private int ParseRepeat()
    {
        _ = int.TryParse(txtRepeat.Text, out int repeat) ? repeat : 1;
        return repeat;
    }

    [STAThread]
    private void RunActions()
    {
        int count = ParseRepeat();
        for (int i = 0; i < count; i++)
        {
            RunActionsOnce();
        }
    }

    private void RunActionsOnce()
    {
        foreach (DataGridViewRow row in dgvActions.Rows)
        {
            string actionName = ParseActionName(row);
            if (actionName == "")
                continue;

            row.Selected = true;

            int count = ParseCount(row);

            for (int i = 0; i < count; i++)
            {
                // If stop button has been pressed since, cancel process
                if (!runningActions)
                    return;

                LogToUserConsole($"Running action: {actionName}");

                switch (actionName)
                {
                    case "Wait 1 millisecond":
                        Thread.Sleep(1);
                        break;
                    case "Wait 1 second":
                        Thread.Sleep(1000);
                        break;
                    case "Wait 1 minute":
                        Thread.Sleep(1000 * 60);
                        break;
                    case "Wait 1 hour":
                        Thread.Sleep(1000 * 60 * 60);
                        break;
                    default:
                        Actions.Action action = Actions.GetAction(actionName);
                        if (action.Code == null)
                            continue;

                        SendKeys.Send(action.Code);
                        Thread.Sleep(10);
                        break;
                }
            }
            row.Selected = false;
        }
    }

    private static string ParseActionName(DataGridViewRow row)
    {
        return row.Cells[0].Value?.ToString() ?? "";
    }

    private static int ParseCount(DataGridViewRow row)
    {
        int count = 1;
        if (row.Cells[1].Value != null)
            _ = int.TryParse(row.Cells[1].Value.ToString(), out count);
        return count;
    }

    private void txtRepeat_TextChanged(object sender, EventArgs e)
    {
        string text = txtRepeat.Text;
        if (text == "")
            return;

        if (text == "1" && lblTimes.Text != "time")
        {
            lblTimes.Text = $"time";
        }
        else
        {
            lblTimes.Text = $"times";
        }
    }

    private void txtDelay_TextChanged(object sender, EventArgs e)
    {
        string text = txtDelay.Text;
        if (text == "")
            return;

        if (text == "1" && lblSeconds.Text != "second")
        {
            lblSeconds.Text = $"second";
        }
        else
        {
            lblSeconds.Text = $"seconds";
        }
    }
}
