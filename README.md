[![NuGet version (Micro-Manager.NET)](https://img.shields.io/nuget/v/Micro-Manager.NET.svg)](https://www.nuget.org/packages/Micro-Manager.NET/2.0.3)
[![NuGet version (Micro-Manager.NET)](https://img.shields.io/nuget/dt/Micro-Manager.NET?color=g)](https://www.nuget.org/packages/Micro-Manager.NET/2.0.3)
# Micro-Manager.NET
Using Micro-Manager 2.0.3 with IKVM & C#

# Building 
- Get IKVM 8.10.2.

- Then use Micro-Manager in C#
```
using mmcorej;
using org.micromanager.@internal;
namespace MMTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Now, set the Java "user.dir" system property to match the C# current directory
            string p = Path.GetDirectoryName("C:/Program Files/Micro-Manager-2.0/");
            Directory.SetCurrentDirectory(p);
            java.lang.System.setProperty("user.dir", p);
            java.lang.System.setProperty("force.annotation.index", "true");
            java.lang.System.setProperty("org.micromanager.corej.path", "C:/Program Files/Micro-Manager-2.0/");
            CMMCore core;
            MMStudio studio;
            try
            {
                MMStudio.main(new string[] { });
                studio = MMStudio.getInstance();
                core = studio.core();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Environment.ProcessPath));
        }
    }
}

```
