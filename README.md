# Micro-Manager.NET
 Controlling Micro-Manager 2.0.3 with IKVM & C#

# Building 
- Get IKVM 8.9.0 or the [developer pre-release](https://github.com/ikvmnet/ikvm/actions/runs/9238355862/artifacts/1537937356).

- Then use Micro-Manager in C#
```
extern alias mm;
using mm::org.micromanager.@internal;
using mm::mmcorej;
using System;
namespace MicroManagerNET
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            MicroManager.Initialize("C:/Program Files/Micro-Manager-2.0/MMConfig_demo.cfg");
            Directory.SetCurrentDirectory("C:/Program Files/Micro-Manager-2.0/");
            java.lang.System.setProperty("force.annotation.index", "true");
            // Set the library path (adjust the path as needed)
            java.lang.System.setProperty("org.micromanager.corej.path", "C:/Program Files/Micro-Manager-2.0");
            try
            {
                MMStudio.main(args);
                MMStudio ms = MMStudio.getInstance();
                CMMCore core = ms.core();
                core.setExposure(5);
                core.setXYPosition(100, 100);
                string obj = MicroManager.Objectives.GetObjective(1).Values[0];
                core.setConfig(MicroManager.TurretName, obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}


```
