using System.Runtime.InteropServices;

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
        this.Invoke(new Action(() =>
        {
            rtbTerminal.AppendText($"{prefix}[{DateTime.Now.ToString("HH:mm:ss")}] {text}\n");
            // Scroll to end of text
            rtbTerminal.SelectionStart = rtbTerminal.Text.Length;
            rtbTerminal.ScrollToCaret();
        }));
    }

    private void IndicateActionsRunState()
    {
        this.Invoke(new Action(() =>
        {
            btnRun.Text = "Stop";
            runningActions = true;
        }));
    }

    private void IndicateActionsStopState()
    {
        this.Invoke(new Action(() =>
        {
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
            bool completed = RunActionsOnce(i + 1, count);
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
                    LogToUserConsole($"Running action [{i - skippedRows + 1}/{rowCount}]: {actionName}");
                }
                else
                {
                    LogToUserConsole($"Running action [{iteration}/{totalIterations}, {i - skippedRows + 1}/{rowCount}]: {actionName}");
                }

                PerformAction(actionName);
            }
            row.Selected = false;
        }
        return true;
    }

    private void PerformAction(string actionName)
    {
        if (Actions.IsDelayAction(actionName))
        {
            PerformDelayAction(actionName);
        }
        else if (Actions.IsMouseAction(actionName))
        {
            PerformMouseAction(actionName);
        }
        else
        {
            PerformKeyboardAction(actionName);
        }
    }

    private void PerformDelayAction(string actionName)
    {
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
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;
    private const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
    private const int MOUSEEVENTF_MIDDLEUP = 0x40;

    private static void PerformMouseAction(string actionName)
    {

        switch (actionName)
        {
            case "Mouse 1px left":
                Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                break;
            case "Mouse 1px right":
                Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                break;
            case "Mouse 1px up":
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                break;
            case "Mouse 1px down":
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                break;
            case "Left click":
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                break;
            case "Right click":
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                break;
            case "Middle click":
                mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                break;
        }
    }

    private static void PerformKeyboardAction(string actionName)
    {
        Actions.Action action = Actions.GetAction(actionName);
        if (action.Code != null)
            SendKeys.SendWait(action.Code);
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

    private void dgvActions_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {

        e.CellStyle.BackColor = dgvActions.DefaultCellStyle.BackColor;

        // Check if currently on the Product column

        DataGridView dgv = sender as DataGridView;
        if (dgv == null || dgv.CurrentCell == null || !(e.Control is ComboBox))
        {
            return;
        }

        ComboBox cbo = (ComboBox)e.Control;
        cbo.DropDownStyle = ComboBoxStyle.DropDown;

        // 29.0.0.0 - We need this handler attached to this dropdown column only in order to force it to take the value entered in text if it fails to do so itself
        cbo.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
    }

    private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 29.0.0.0 - There was an issue when typing in the value and clicking tab sometimes wouldn't register a ValueChange
        // So here we intercept any current text just entered into the dropdown and force it to change value
        if (dgvActions.CurrentCell != null)
        {
            dgvActions.CurrentCell.Value = dgvActions.CurrentCell.EditedFormattedValue;
        }
    }
}
