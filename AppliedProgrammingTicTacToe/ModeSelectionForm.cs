using System;
using System.Drawing;
using System.Windows.Forms;

public class ModeSelectionForm : Form
{
    public string? SelectedMode { get; private set; } = null;

    public ModeSelectionForm()
    {
        Text = "Seleccionar Modo de Juego";
        Size = new Size(320, 180);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        Label label = new Label()
        {
            Text = "¿Cómo querés jugar?\n\n1 = Consola\n2 = Interfaz Gráfica",
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 70
        };

        Button btn1 = new Button() { Text = "1", Width = 80, Height = 40 };
        btn1.Click += (s, e) => { SelectedMode = "1"; DialogResult = DialogResult.OK; Close(); };

        Button btn2 = new Button() { Text = "2", Width = 80, Height = 40 };
        btn2.Click += (s, e) => { SelectedMode = "2"; DialogResult = DialogResult.OK; Close(); };

        Button btnCancel = new Button() { Text = "Cancelar", Width = 100, Height = 40 };
        btnCancel.Click += (s, e) => { SelectedMode = null; DialogResult = DialogResult.Cancel; Close(); };

        FlowLayoutPanel panel = new FlowLayoutPanel()
        {
            Dock = DockStyle.Bottom,
            FlowDirection = FlowDirection.LeftToRight,
            Height = 60,
            Padding = new Padding(10),
            AutoSize = true
        };

        panel.Controls.Add(btn1);
        panel.Controls.Add(btn2);
        panel.Controls.Add(btnCancel);

        Controls.Add(label);
        Controls.Add(panel);
    }
}

