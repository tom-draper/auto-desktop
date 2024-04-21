using System.Runtime.InteropServices;
using auto_desktop.Classes;

namespace auto_desktop;

public partial class Form : System.Windows.Forms.Form
{

    private bool runningActions = false;

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
        // Remove any existing cell highlights before row highlight animation
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
        Invoke(new Action(() =>
        {
            rtbTerminal.AppendText($"{prefix}[{DateTime.Now:HH:mm:ss.fff}] {text}\n");
            // Scroll to end of text
            rtbTerminal.SelectionStart = rtbTerminal.Text.Length;
            rtbTerminal.ScrollToCaret();
        }));
    }

    private void IndicateActionsRunState()
    {
        Invoke(new Action(() =>
        {
            btnRun.Text = "Stop";
            runningActions = true;
        }));
    }

    private void IndicateActionsStopState()
    {
        Invoke(new Action(() =>
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
                    LogToUserConsole($"Running action [{i - skippedRows + 1}/{rowCount}]: {actionName}");
                else
                    LogToUserConsole($"Running action [{iteration}/{totalIterations}, {i - skippedRows + 1}/{rowCount}]: {actionName}");

                PerformAction(actionName);
            }
            row.Selected = false;
        }
        return true;
    }

    private static void PerformAction(string actionName)
    {
        if (Actions.IsDelayAction(actionName))
            PerformDelayAction(actionName);
        else if (Actions.IsMouseAction(actionName))
            PerformMouseAction(actionName);
        else
            PerformKeyboardAction(actionName);
    }

    private static void PerformDelayAction(string actionName)
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
        Actions.Action? action = Actions.GetAction(actionName);
        if (action?.Code != null)
            SendKeys.SendWait(action.Code);
        else
            // Action is a literal string to type
            SendKeys.SendWait(actionName);
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
            lblSeconds.Text = $"second";
        else
            lblSeconds.Text = $"seconds";
    }

    private void dgvActions_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        e.CellStyle.ForeColor = dgvActions.DefaultCellStyle.ForeColor;
        e.CellStyle.BackColor = dgvActions.DefaultCellStyle.BackColor;

        if (sender is not DataGridView dgv || dgv.CurrentCell == null || e.Control is not ComboBox)
            return;

        ComboBox cbo = (ComboBox)e.Control;
        cbo.DropDownStyle = ComboBoxStyle.DropDown;

        cbo.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
        cbo.KeyUp += ComboBox_OnKeyUp;
        cbo.Leave += ComboBox_OnLeave;
    }

    private void ComboBox_OnLeave(object? sender, EventArgs e)
    {
        dgvActions.CurrentCell.Value = dgvActions.CurrentCell.EditedFormattedValue;
        //if (dgvActions.CurrentCell.RowIndex == dgvActions.Rows.Count - 1)
        //    dgvActions.Rows.Add();
    }

    private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dgvActions.CurrentCell == null)
            return;

        dgvActions.CurrentCell.Value = dgvActions.CurrentCell.EditedFormattedValue;

        // Check if the current cell is in the last row
        //if (dgvActions.CurrentCell.RowIndex == dgvActions.RowCount - 1)
            // Add a new row to the DataGridView
            //dgvActions.Rows.Add();
    }

    private void ComboBox_OnKeyUp(object sender, EventArgs e)
    {
        string value = dgvActions.CurrentCell.EditedFormattedValue.ToString() ?? "";
        DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dgvActions.Columns[dgvActions.CurrentCell.ColumnIndex];
        if (value != "" && !col.Items.Contains(value))
        {
            col.Items.Add(value);
            //dgvActions.CurrentCell.Value = value;
        }
    }
}
