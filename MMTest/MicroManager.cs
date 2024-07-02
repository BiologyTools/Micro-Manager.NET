using AForge;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RectangleD = AForge.RectangleD;

namespace MMTest
{
    public static class MicroManager
    {
        public static Dictionary<string, List<Conf>> Config = new Dictionary<string, List<Conf>>();
        public struct Conf
        {
            public string Class {  get; set; }
            public string Type { get; set; }
            public string[] Values { get; set; }
            public Conf(string Class, string Type, string[] Values)
            {
                this.Class = Class;
                this.Type = Type;
                this.Values = Values;
            }
            public override string ToString()
            {
                string s = Class + "," + Type + ",";
                foreach (string item in Values)
                {
                    s += item + ",";
                }
                return s;
            }
        }
        public static Conf[] GetConfigs(string Class, string Type)
        {
            List<Conf> configs = new List<Conf>();
            foreach (var conf in Config[Class]) 
            {
                if(conf.Type == Type) configs.Add(conf);
            }
            return configs.ToArray();
        }
        public static bool initialized = false;
        public static string TurretName = "";
        public static bool Initialize(string config)
        {
            string[] sts = File.ReadAllLines(config);
            foreach (string s in sts)
            {
                if(!s.Replace(" ","").StartsWith("#"))
                {
                    string[] vals = s.Split(',');
                    if (vals.Length == 1)
                        continue;
                    if (!Config.ContainsKey(vals[0]))
                    Config.Add(vals[0],new List<Conf>());
                    List<string> vs = new List<string>();
                    for (int j = 2; j < vals.Length; j++)
                    {
                        vs.Add(vals[j]);
                    }
                    Config[vals[0]].Add(new Conf(vals[0], vals[1], vs.ToArray()));
                }
            }
            if(Config.ContainsKey("Device"))
            {
                //We get the name of the objective turret required for changing objectives
                foreach (Conf item in Config["Device"])
                {
                    if(item.Type == "Objective")
                    {
                        //We set the 
                        TurretName = "Objective";
                        initialized = true;
                    }
                }
            }
            Objectives.Initialize();
            if(!initialized)
            {
                Console.WriteLine("Unable to find Objective Turret (DObjective) Label in MMConfig.");
                return false;
            }
            Shutters.Initialize();
            Point3D loc = new Point3D();
            return true;
        }
        public static class Objectives
        {
            /// <summary>
            /// Number of Installed Objectives
            /// </summary>
            public static int InstalledCount
            {
                get
                {
                    return GetConfigs("ConfigGroup", "Objective").Length;
                }
            }
            public static List<Objective> List = new List<Objective>();
            public class Objective
            {
                public string Name { get; set; }
                public int Index {  get; set; }
                public int Magnification { get; set; }
                public Objective(string name, int index, int mag)
                {
                    Name = name;
                    Index = index;
                    Magnification = mag;
                }
            }
            internal static void Initialize()
            {
                int i = 0;
                foreach (Conf obj in GetConfigs("ConfigGroup", "Objective"))
                {
                    List.Add(new Objective(obj.Values[0], int.Parse(obj.Values[3]), int.Parse(obj.Values[0].Replace("X", ""))));
                }
            }

            public static Conf GetObjective(int index)
            {
                Conf[] cfs = GetConfigs("ConfigGroup", "Objective");
                return cfs[index];
            }
            
        }
        public static class Shutters
        {
            public class Shutter
            {
                public string Name { get; set; }
                public Shutter(string name)
                {
                    Name = name;
                }
            }
            public static List<Shutter> List = new List<Shutter>();
            public static void Initialize()
            {
                if (Config.ContainsKey("Device"))
                {
                    //We get the name of the shutters.
                    foreach (Conf item in Config["Device"])
                    {
                        if (item.Values.Last().Contains("Shutter"))
                        {
                            List.Add(new Shutter(item.Type));
                        }
                    }
                }
            }
        }
    }
}
