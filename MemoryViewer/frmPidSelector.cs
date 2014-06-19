using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryViewer
{
    public partial class frmPidSelector : Form
    {
        public frmPidSelector()
        {
            InitializeComponent();
        }

        private void frmPidSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Tag = (int)numericUpDown1.Value;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Tag = (int)numericUpDown1.Value;
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }

        }
    }
}
