extern alias mmc;
using mmc::org.micromanager.@internal;
using mmc::mmcorej;
using System;
namespace MMTest
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
