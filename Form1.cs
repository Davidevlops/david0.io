using System;
using System.Windows.Forms;
namespace Inventory_Management_System;

public partial class Form1 : Form
{
    public Form1()
    {
            InitializeComponent();

            Button btn = new Button();
            btn.Text = "Click Me";
            btn.Width = 100;
            btn.Top = 50;
            btn.Left = 50;

            btn.Click += (s, e) =>
            {
                MessageBox.Show("Hello David!");
            };

            Controls.Add(btn);
        }
}