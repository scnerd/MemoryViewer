Memory Viewer
David Maxson
scnerd@gmail.com
4/22/14


==============================
INTRODUCTION
==============================

Memory Viewer (MV) is a small utility to help you analyze the memory layout of various applications on your computer. It provides a list of all processes and their associated threads currently running, or you can input the PID of the process you're interested in to get to its information faster. Processes show the start locations and sizes of all loaded modules, which may indicate infection or potential hooking points, as well as the process's main module's memory layout. You can also see an individual thread's memory layout.


==============================
INSTALLATION
==============================

To get MV running properly, you should only need .NET 4.0 installed. If you do not have it installed already, you can install it from here:

http://www.microsoft.com/en-us/download/details.aspx?id=17851


==============================
LAUNCHING
==============================

Launch MemoryViewer.exe, and give it administrator priviledges. It cannot run with those permissions.

Note that if you have the entire source code, this can be found in MemoryViewer\MemoryViewer\bin\Release\


==============================
USAGE
==============================

--- Main Screen ---
The primary screen shows a listing of all processes currently running on your computer. It is a static list, but can be refreshed using the button at the bottom. Also, a specific PID can be found and analyzed using the "Find by Pid" button. Expand processes to see their thread listing. Right-click or double-click any process or thread to open its analysis window to see more information about it.

--- Process Analyzer ---
Opening the details on a process will bring up the Process Analyzer screen. The left panel is C# reflection information on the given process, providing various low-level, esoteric information on the process. Of main interest is the modules listing and description on the upper right side, where each module is listed with its name, path, start address in memory, and size in memory. On the lower right is the memory layout of the program's main module.

--- Thread Analyer ---
Opening the details on a thread will bring up the Thread Analyzer screen. The left panel is C# reflection information on the given thread, providing some low-level information on the thread. On the right panel is the memory dump of the module that the thread was launched in.

--- Help ---
Pressing the F1 key will bring up this README. For further information, you may contact the author at scnerd@gmail.com
