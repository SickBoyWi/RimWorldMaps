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
    /**
     * Drop this class into a mod's Harmony patch, so you can see the hash codes for the various defs you're looking for.
     */
    [StaticConstructorOnStartup]
    static class ExampleDefHashFinder
    {
        private static bool TEMPDELETE = false;

        static ExampleDefHashFinder()
        {
        }

        public static void SampleCode()
        {
            if (!TEMPDELETE)
            {
                TEMPDELETE = true;

                // TODO THING VS TERRAIN
                Dictionary<ushort, ThingDef> defsByShortHash = new Dictionary<ushort, ThingDef>();
                //Dictionary<ushort, TerrainDef> defsByShortHash = new Dictionary<ushort, TerrainDef>();

                // TODO THING VS TERRAIN
                foreach (ThingDef allDef in DefDatabase<ThingDef>.AllDefs)
                {
                    if (!defsByShortHash.ContainsKey(allDef.shortHash))
                        defsByShortHash.Add(allDef.shortHash, allDef);
                }

                bool kickOut = false;
                int iterator = 0;
                int iteratorCount = 20;
                StringBuilder output = new StringBuilder();
                foreach (ushort key in defsByShortHash.Keys)
                {
                    if (defsByShortHash.TryGetValue(key).defName.Contains("Dwarf"))
                    {
                        // TODO THING VS TERRAIN
                        output.Append("THING_KEY_VALUE_USHORT.Add((ushort)" + key + ", \"" + defsByShortHash.TryGetValue(key) + "\");ENDLINE");
                        //output.Append("TERRAIN_KEY_VALUE_USHORT.Add((ushort)" + key + ", \"" + defsByShortHash.TryGetValue(key) + "\");ENDLINE");

                        if (iterator == iteratorCount - 1)
                            kickOut = true;

                        if (kickOut)
                            Log.Message(output.ToString());

                        if (iterator == iteratorCount - 1)
                        {
                            iterator = 0;
                            kickOut = false;
                            output = new StringBuilder();
                        }

                        iterator++;
                    }
                }
            }
        }
    }
}
