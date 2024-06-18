using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
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
                comboBoxColumn.Items.Add(action.Key);
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
            bool completed = RunActionsOnce(i, count);
            if (!completed)
                return false;
        }
        return true;
    }

    private bool RunActionsOnce(int iteration = 0, int totalIterations = 1)
    {
        int totalActions = CountActions();
        int actionsProcessed = 0;
        for (int i = 0; i < dgvActions.Rows.Count; i++)
        {
            DataGridViewRow row = dgvActions.Rows[i];
            string actionName = ParseActionName(row);
            if (!Actions.IsAction(actionName))
                continue;

            row.Selected = true;

            int actionRepeat = ParseCount(row);
            if (Actions.IsMultipliableAction(actionName))
            {
                // If stop button has been pressed since, cancel process
                if (!runningActions)
                    return false;

                int multiplier = actionRepeat;

                string counterText = GetCounterText(iteration, totalIterations, actionsProcessed, totalActions, actionRepeat - 1, actionRepeat);
                string updatedActionName = actionName.Replace("1", multiplier.ToString());
                if (counterText == "")
                    LogToUserConsole($"Running action: {updatedActionName}");
                else
                    LogToUserConsole($"Running action [{counterText}]: {updatedActionName}");

                Actions.InvokeAction(actionName, actionRepeat);
            }
            else
            {
                for (int j = 0; j < actionRepeat; j++)
                {
                    // If stop button has been pressed since, cancel process
                    if (!runningActions)
                        return false;

                    string counterText = GetCounterText(iteration, totalIterations, actionsProcessed, totalActions, j, actionRepeat);
                    if (counterText == "")
                        LogToUserConsole($"Running action: {actionName}");
                    else
                        LogToUserConsole($"Running action [{counterText}]: {actionName}");

                    Actions.InvokeAction(actionName);
                }
            }

            row.Selected = false;

            actionsProcessed++;
        }
        return true;
    }

    private static string GetCounterText(int iteration, int totalIterations, int actionsProcessed, int totalActions, int actionRepeatCount, int actionRepeat)
    {
        if (totalIterations > 1)
            return $"{iteration + 1}/{totalIterations} {actionsProcessed + 1}/{totalActions} {actionRepeatCount + 1}/{actionRepeat}";

        if (totalActions > 1)
            return $"{actionsProcessed + 1}/{totalActions} {actionRepeatCount + 1}/{actionRepeat}";

        if (actionRepeat > 1)
            return $"{actionRepeatCount + 1}/{actionRepeat}";

        return "";
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

        cbo.Leave += ComboBox_OnLeave;
        cbo.SelectedValueChanged += ComboBox_CellValueChanged;

        // Unsubscribe from the events when the editing control is hidden
        dgv.EditingControlShowing += (s, args) =>
        {
            if (args.Control is ComboBox comboBox)
                comboBox.Leave -= ComboBox_OnLeave;
        };
    }

    private void ComboBox_OnLeave(object? sender, EventArgs e)
    {
        if (dgvActions.CurrentCell == null)
            return;

        string value = dgvActions.CurrentCell.EditedFormattedValue.ToString() ?? "";
        DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dgvActions.Columns[dgvActions.CurrentCell.ColumnIndex];
        if (value != "" && !col.Items.Contains(value))
            col.Items.Add(value);

        dgvActions.CurrentCell.Value = dgvActions.CurrentCell.EditedFormattedValue;
    }

    private void ComboBox_CellValueChanged(object? sender, EventArgs e)
    {
        if (dgvActions.CurrentCell == null)
            return;

        string value = dgvActions.CurrentCell.EditedFormattedValue.ToString() ?? "";
        DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dgvActions.Columns[dgvActions.CurrentCell.ColumnIndex];
        if (value != "" && !col.Items.Contains(value))
            col.Items.Add(value);

        dgvActions.CurrentCell.Value = dgvActions.CurrentCell.EditedFormattedValue;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        StringBuilder content = new();
        foreach (DataGridViewRow row in dgvActions.Rows)
        {
            string actionName = ParseActionName(row);
            if (actionName == "")
                continue;

            int actionRepeat = ParseCount(row);
            content.AppendLine($"\"{actionName}\",{actionRepeat}");
        }

        FolderBrowserDialog fbd = new()
        {
            Description = "Custom Description"
        };

        if (fbd.ShowDialog() == DialogResult.OK)
        {
            string selectedPath = fbd.SelectedPath;
            File.WriteAllText($"{selectedPath}/auto_desktop_{DateTime.Now:yyyMMddHHmmss}.csv", content.ToString());
        }
    }

    private void btnInsertRow_Click(object sender, EventArgs e)
    {
        dgvActions.Rows.Insert(dgvActions.CurrentRow.Index);
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        OpenFileDialog choofdlog = new()
        {
            Filter = "All Files (*.*)|*.*",
            FilterIndex = 1,
            Multiselect = true
        };

        if (choofdlog.ShowDialog() == DialogResult.OK)
        {
            string fileName = choofdlog.FileName;
            using var reader = new StreamReader(fileName);
            dgvActions.Rows.Clear();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                try
                {
                    string[] values = line.Split("\",");
                    string action = values[0][1..]; // Remove leading quote
                    string count = values[1];

                    int index = dgvActions.Rows.Add();
                    dgvActions.Rows[index].Cells[0].Value = action;
                    dgvActions.Rows[index].Cells[1].Value = count;
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Error reading line: {line}");
                }
            }
        }
    }
}
