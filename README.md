# MMControl
 Controlling Micro-Manager 2.0 with IKVM & C#

# Building 
- Get IKVM 8.9.0 or the [developer pre-release](https://github.com/ikvmnet/ikvm/actions/runs/9238355862/artifacts/1537937356).

- Then use Micro-Manager in C# with mmcore namespace.
```
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
```
