using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MemoryViewer
{
    public partial class frmMain : Form
    {
        const string EMPTY_NODE_TEXT = "Loading...";

        public frmMain()
        {
            InitializeComponent();
            RefreshProcesses();
        }

        private void btnFindByPID_Click(object sender, EventArgs e)
        {
            var selector = new frmPidSelector();
            if (selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    var proc = Process.GetProcessById((int)selector.Tag);
                    new frmProcViewer(proc).Show();
                }
                catch (ArgumentException)
                { MessageBox.Show("No such process is running"); }
                catch (InvalidOperationException)
                { MessageBox.Show("That process was not started from this process (permission denied?)"); }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshProcesses();
        }

        private void RefreshProcesses()
        {
            treProcs.Nodes.Clear();

            foreach (var proc in Process.GetProcesses().OrderBy(p => p.Id))
            {
                treProcs.Nodes.Add(NodeFromProc(proc));
            }
        }

        private void treProcs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Node.Nodes[0].Text == EMPTY_NODE_TEXT)
                FillNode(e.Node);
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                OpenAnalysisWindow(e);
        }

        private void treProcs_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OpenAnalysisWindow(e);
        }

        private void FillNode(TreeNode treeNode)
        {
            if(treeNode.Tag.GetType() == typeof(Process))
            {
                treeNode.Nodes.Clear();
                foreach (ProcessThread thr in ((Process)treeNode.Tag).Threads)
                {
                    treeNode.Nodes.Add(NodeFromThread(thr));
                }
            }
        }

        private void OpenAnalysisWindow(TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag.GetType() == typeof(Process))
            {
                new frmProcViewer((Process)e.Node.Tag).Show();
            }

            else if (e.Node.Tag.GetType() == typeof(ProcessThread))
            {
                new frmThreadViewer((Process)e.Node.Parent.Tag, (ProcessThread)e.Node.Tag).Show();
            }
        }

        public static TreeNode NodeFromProc(Process p)
        {
            var node = new TreeNode(p.ProcessName + " (" + p.Id + ")");
            node.Tag = p;
            node.Nodes.Add(EMPTY_NODE_TEXT);
            return node;
        }

        public static TreeNode NodeFromThread(ProcessThread t)
        {
            var node = new TreeNode("0x" + Convert.ToString(t.Id, 16));
            node.Tag = t;
            return node;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                Process.Start(@".\README.txt");
        }
    }
}
