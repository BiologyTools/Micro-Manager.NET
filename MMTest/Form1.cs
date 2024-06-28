using mmcorej;
using org.micromanager.@internal;
using System;

namespace MMTest
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            CMMCore core = new CMMCore();
            core.loadSystemConfiguration("MMConfig_demo.cfg");
            core.snapImage();
            System.Int16[] img = (System.Int16[])core.getImage();
        }
    }
}
