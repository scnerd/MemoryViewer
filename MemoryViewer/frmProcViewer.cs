using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MemoryViewer
{
    public partial class frmProcViewer : Form
    {
        readonly int PTR_SIZE;
        Process baseProc;
        internal List<byte> procMemory;
        System.Threading.SemaphoreSlim canStop = new System.Threading.SemaphoreSlim(1);
        bool needToStop = false;
        Task ReadInBytes;

        public frmProcViewer(Process proc, Task AfterLoadBytes = null)
        {
            PTR_SIZE = IsWin64(proc) ? 64/8 : 32/8;
            baseProc = proc;
            InitializeComponent();
            this.Text = frmMain.NodeFromProc(proc).Text;

            datData.Columns.Add("name", "Name");
            datData.Columns.Add("value", "Value");
            foreach(var field in proc.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .OrderBy(f => f.Name))
            {
                datData.Rows.Add(field.Name, field.GetValue(proc));
            }

            datModules.Columns.Add("modName", "Module Name");
            datModules.Columns.Add("baseAddr", "Loaded Address");
            datModules.Columns.Add("entryPt", "Size");
            datModules.Columns.Add("fileName", "File Name");
            foreach (ProcessModule mod in proc.Modules)
            {
                datModules.Rows.Add(mod.ModuleName,
                    "0x" + Convert.ToString(FromPtr(mod.BaseAddress, Process.GetCurrentProcess()), 16).PadLeft(PTR_SIZE, '0'),
                    mod.ModuleMemorySize / 1024 + "KB",
                    mod.FileName);
            }

            datMemory.Columns.Add("addr", "Address");
            datMemory.Columns.Add("hex", "Hex");
            datMemory.Columns.Add("ascii", "ASCII");
            datMemory.Rows.Add("", "Loading, please wait", "");
            foreach (DataGridViewColumn c in datData.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            foreach (DataGridViewColumn c in datModules.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            foreach (DataGridViewColumn c in datMemory.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            Task DisplayBytes = null;
            ReadInBytes = new Task(new Action(() =>
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
                procMemory = new List<byte>();

                var handle = OpenProcess(PROCESS_WM_READ, false, proc.Id);
                var startOffset = proc.MainModule.BaseAddress;
                var endOffset = IntPtr.Add(startOffset, proc.MainModule.ModuleMemorySize);
                long start = FromPtr(startOffset, proc); long end = FromPtr(endOffset, proc);
                for (long loc = start; loc < end; loc += buff.Length)
                {
                    res = ReadProcessMemory((int)handle, (int)loc, buff, (int)Math.Min(buff.Length, end-start), ref actually_read);
                    if (res)
                        procMemory.AddRange(buff.Take((int)actually_read));
                    else
                        break;
                }
                CloseHandle(handle);
                DisplayBytes.Start();
            }));
            DisplayBytes = new Task(new Action(() =>
                {
                    if (AfterLoadBytes != null)
                        AfterLoadBytes.Start();
                    var rows = ConvertBytesToRows(procMemory.ToArray());
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

        internal static long FromPtr(IntPtr ptr, Process proc)
        {
            return (long)(IsWin64(proc) ? ptr.ToInt64() : ptr.ToInt64());
        }

        internal static IEnumerator<string[]> ConvertBytesToRows(byte[] data, int offset = 0)
        {
            const int ROW_SIZE = 32;
            const int ADDR_SIZE = 16;
            for (; offset < data.Length; offset += ROW_SIZE)
            {
                yield return new string[]{
                            Convert.ToString(offset, ADDR_SIZE).PadLeft(8, '0'),
                            data
                            .Skip(offset)
                            .Take(Math.Min(ROW_SIZE, data.Length - offset))
                            .Select(b => Convert.ToString(b, 16).PadLeft(2, '0'))
                            .Select((s,i) => i > 0 && i % (ROW_SIZE / 8) == 0 ? (i % (ROW_SIZE / 2) == 0 ? "  " : " ") + s : s)
                            .Aggregate("", (a,b) => a + b),
                            data
                            .Skip(offset)
                            .Take(Math.Min(ROW_SIZE, data.Length - offset))
                            .Select(b => char.IsControl((char)b) ? '.' : (char)b)
                            .Aggregate<char, string>("", (a,b) => a.ToString() + "" + b.ToString())
                        };
            }
        }

        //http://stackoverflow.com/questions/1953377/how-to-know-a-process-is-32-bit-or-64-bit-programmatically
        private static bool IsWin64(Process process)
        {
            if ((Environment.OSVersion.Version.Major > 5)
                || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)))
            {
                IntPtr processHandle;
                bool retVal;

                try
                {
                    processHandle = Process.GetProcessById(process.Id).Handle;
                }
                catch
                {
                    return false; // access is denied to the process
                }

                return NativeMethods.IsWow64Process(processHandle, out retVal) && retVal;
            }

            return false; // not on 64-bit Windows
        }


        //http://www.codeproject.com/Articles/670373/Csharp-Read-Write-another-Process-Memory
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
          int lpBaseAddress, byte[] lpBuffer, int dwSize,
#if X64
            ref long lpNumberOfBytesRead
#else
            ref int lpNumberOfBytesRead
#endif       
            );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        private void frmProcViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            lock (this)
            {
                needToStop = true;
            }
        }
    }
    
    //http://stackoverflow.com/questions/1953377/how-to-know-a-process-is-32-bit-or-64-bit-programmatically
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
    }
}
