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
    class ThingHolder
    {
        public string defName;
        public int rot;
        //public IntVec3 pos;
        public List<IntVec3> positions = new List<IntVec3>();
        public int count;
        public string kind;
        public string stuff;
        // TODO: THIS IS UNUSED AS YET. Needed for insects or mechs.
        //public string faction;

        public ThingHolder(string defNameIn, int rotIn, IntVec3 posIn, int countIn, string stuffIn, string kindIn)
        {
            defName = defNameIn;
            rot = rotIn;
            positions.Add(posIn);
            count = countIn;
            stuff = stuffIn;
            kind = kindIn;
        }

        public void AddPos(IntVec3 posIn)
        {
            // Only allow a thing at a location once. This could in theory cause an issue with stuff missing. I'll try
            // it and see what happens.
            //if (!positions.Contains(posIn))
                positions.Add(posIn);
        }
    }
}
