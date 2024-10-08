using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWrightFramework.Config
{
    public class TestSettings
    {
        public string[] args { get; set; }
        public float timeout { get; set; }
        public bool headless { get; set; }
        public bool devtools { get; set;}
        public float slowMo { get; set; }
        public DriverType DriverType { get; set; }
        public string ApplicationUrl { get; set; }
    }

    public enum DriverType 
    { 
        Chromium,
        FireFox,
        Edge,
        Chrome,
        WebKit
    
    }
}
