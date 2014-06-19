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
using System.Reflection;
using System.Runtime.InteropServices;

namespace MemoryViewer
{
    public partial class frmThreadViewer : Form
    {
        Process baseProc;
        ProcessThread baseThread;
        bool needToStop = false;
        List<byte> thrMemory;

        public frmThreadViewer(Process proc, ProcessThread thr)
        {
            baseProc = proc;
            baseThread = thr;
            InitializeComponent();
            this.Text = frmMain.NodeFromProc(proc).Text + " -> " + frmMain.NodeFromThread(thr).Text;

            datData.Columns.Add("name", "Name");
            datData.Columns.Add("value", "Value");
            foreach (var field in thr.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .OrderBy(f => f.Name))
            {
                datData.Rows.Add(field.Name, field.GetValue(thr));
            }

            datMemory.Columns.Add("addr", "Address");
            datMemory.Columns.Add("hex", "Hex");
            datMemory.Columns.Add("ascii", "ASCII");
            foreach (DataGridViewColumn c in datData.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            foreach (DataGridViewColumn c in datMemory.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            Task DisplayBytes = null;
            Task ReadInBytes = new Task(new Action(() =>
            {
                const int PROCESS_WM_READ = 0x0010;
                //Read the memory from the process
                byte[] buff = new byte[2048];
#if X64
                long actually_read = 1;
#else
                int actually_read = 1;
#endif
                bool res = true;
                thrMemory = new List<byte>();

                var handle = frmProcViewer.OpenProcess(PROCESS_WM_READ, false, proc.Id);
                IntPtr startOffset = IntPtr.Zero, endOffset = IntPtr.Zero;
                ProcessModule toScan = null;
                foreach (ProcessModule mod in proc.Modules)
                {
                    startOffset = mod.BaseAddress;
                    endOffset = IntPtr.Add(startOffset, mod.ModuleMemorySize);
                    if (frmProcViewer.FromPtr(startOffset, proc) <= frmProcViewer.FromPtr(thr.StartAddress, proc)
                        && frmProcViewer.FromPtr(thr.StartAddress, proc) < frmProcViewer.FromPtr(endOffset, proc))
                    {
                        toScan = mod;
                        break;
                    }
                }
                long start = frmProcViewer.FromPtr(startOffset, proc); long end = frmProcViewer.FromPtr(endOffset, proc);
                for (long loc = start; loc < end; loc += buff.Length)
                {
                    res = frmProcViewer.ReadProcessMemory((int)handle, (int)loc, buff, (int)Math.Min(buff.Length, end - start), ref actually_read);
                    if (res)
                        thrMemory.AddRange(buff.Take((int)actually_read));
                    else
                        break;
                }
                frmProcViewer.CloseHandle(handle);
                DisplayBytes.Start();
            }));
            DisplayBytes = new Task(new Action(() =>
            {
                var rows = frmProcViewer.ConvertBytesToRows(thrMemory.ToArray());
                this.Invoke(new Action(() => datMemory.Rows.Clear()));

                foreach (var r in new IteratorList<string[]>(rows))
                {
                    lock (this)
                    {
                        if (needToStop)
                            return;
                        this.BeginInvoke(new Action(() =>
                        { if (!datMemory.IsDisposed) datMemory.Rows.Add(r); }));
                    }
                    System.Threading.Thread.Sleep(5);
                }
            }));
            ReadInBytes.Start();
        }

        private void frmThreadViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            lock (this)
            {
                needToStop = true;
            }
        }
    }
}
