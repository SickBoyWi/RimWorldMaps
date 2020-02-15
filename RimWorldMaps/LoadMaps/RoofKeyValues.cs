using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Verse;

namespace RimWorldMaps
{
    [StaticConstructorOnStartup]
    static class RoofKeyValues
    {
        public static Dictionary<ushort, string> ROOF_KEY_VALUE_USHORT = new Dictionary<ushort, string>();

        static RoofKeyValues ()
        {
            ROOF_KEY_VALUE_USHORT.Add((ushort)5133, "RoofConstructed");
            ROOF_KEY_VALUE_USHORT.Add((ushort)10820, "RoofRockThick");
            ROOF_KEY_VALUE_USHORT.Add((ushort)6699, "RoofRockThin");
        }
    }
}
