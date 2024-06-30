extern alias mmc;
using mmc::org.micromanager.@internal;
using System;

namespace MMTest
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            Directory.SetCurrentDirectory("C:/Program Files/Micro-Manager-2.0/");
            java.lang.System.setProperty("force.annotation.index", "true");
            // Set the library path (adjust the path as needed)
            java.lang.System.setProperty("org.micromanager.corej.path", "C:/Program Files/Micro-Manager-2.0");
            try
            {
                MMStudio.main(args);
                MMStudio ms = MMStudio.getInstance();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}