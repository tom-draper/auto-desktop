namespace auto_desktop;

public partial class Form : System.Windows.Forms.Form
{

    bool runningActions = false;

    public Form()
    {
        InitializeComponent();
    }

    private void Form_Load(object sender, EventArgs e)
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
        DeselectAllRows();

        // Handle stopping actions
        if (runningActions)
        {
            LogToUserConsole("Stopping actions...");
            btnRun.Text = "Run";
            runningActions = false;
            return;
        }

        int actionsCount = CountActions();
        if (actionsCount == 0)
        {
            LogToUserConsole("No actions specified", true);
            return;
        }

        Thread thread = new(HandleRunActions);
        thread.Start();
    }

    private int CountActions()
    {
        int count = 0;
        foreach (DataGridViewRow row in dgvActions.Rows)
        {
            if (row.Cells[0].Value != null)
                count++;
        }
        return count;
    }

    private int ParseStartDelay()
    {
        _ = int.TryParse(txtDelay.Text, out int delay) ? delay : 5;
        return delay;
    }

    private void LogToUserConsole(string text, bool prefixNewline = false)
    {
        string prefix = prefixNewline ? "\n" : "";
        this.Invoke(new Action(() => {
            rtbTerminal.AppendText($"{prefix}[{DateTime.Now.ToString("HH:mm:ss")}] {text}\n");
            // Scroll to end of text
            rtbTerminal.SelectionStart = rtbTerminal.Text.Length;
            rtbTerminal.ScrollToCaret();
        }));
    }

    private void IndicateActionsRunState()
    {
        this.Invoke(new Action(() => {
            btnRun.Text = "Stop";
            runningActions = true;
        }));
    }

    private void IndicateActionsStopState()
    {
        this.Invoke(new Action(() => {
            btnRun.Text = "Run";
            runningActions = false;
        }));
    }

    private int ParseRepeat()
    {
        _ = int.TryParse(txtRepeat.Text, out int repeat) ? repeat : 1;
        return repeat;
    }

    private void DeselectAllRows()
    {
        foreach (DataGridViewRow row in dgvActions.Rows)
            row.Selected = false;
    }

    private void HandleRunActions()
    {
        IndicateActionsRunState();
        int delay = ParseStartDelay();
        LogToUserConsole($"Starting in {delay} seconds...", true);
        Thread.Sleep(delay * 1000);
        bool completed = RunActions();
        if (completed)
            LogToUserConsole("Complete");
        IndicateActionsStopState();
    }

    private bool RunActions()
    {
        int count = ParseRepeat();
        for (int i = 0; i < count; i++)
        {
            bool completed = RunActionsOnce(i+1, count);
            if (!completed)
                return false;
        }
        return true;
    }

    private bool RunActionsOnce(int iteration = 1, int totalIterations = 1)
    {
        int rowCount = CountActions();
        int skippedRows = 0;
        for (int i = 0; i < dgvActions.Rows.Count; i++)
        {
            DataGridViewRow row = dgvActions.Rows[i];
            string actionName = ParseActionName(row);
            if (actionName == "")
            {
                skippedRows++;
                continue;
            }

            row.Selected = true;

            int count = ParseCount(row);

            for (int j = 0; j < count; j++)
            {
                // If stop button has been pressed since, cancel process
                if (!runningActions)
                    return false;

                if (iteration == 1 && totalIterations == 1)
                {
                    LogToUserConsole($"Running action [{i-skippedRows+1}/{rowCount}]: {actionName}");
                }
                else
                {
                    LogToUserConsole($"Running action [{iteration}/{totalIterations}, {i-skippedRows+1}/{rowCount}]: {actionName}");
                }

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

                        SendKeys.SendWait(action.Code);
                        break;
                }
            }
            row.Selected = false;
        }
        return true;
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
