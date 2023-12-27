using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Verse;

/*
 * Notes: Map size is hardcoded to 250,250
 *          This could use error checking. Assuming input vals are correct for file and args.
 */

// USAGE: EXE inputfile(savegame) outputfile(mapgen XML) defnameForMap
// USAGE: C:\Users\johnn\source\repos\RimWorldMaps\RimWorldMaps\bin\Debug\RimWorldMaps.exe E:\Modding\RimWorld\_Templates\Dwarfs\_CustomMap\OasisshawDirty.rws E:\Modding\RimWorld\_Templates\Dwarfs\_CustomMap\OUTPUT.xml RH_TET_Dwarfs_CapturedHoldMap
// USAGE: RimWorldMaps.exe E:\Modding\RimWorld\_Templates\Dwarfs\_CustomMap\YT-DwarfUpdatesFilthy.rws E:\Modding\RimWorld\_Templates\Dwarfs\_CustomMap\OUTPUT.xml RH_TET_Dwarfs_CapturedHoldMap
namespace RimWorldMaps
{
    class Program
    {
        public static int MAP_SIZE = 250;

        static List<IntVec3> RoofConstructed = new List<IntVec3>();
        static List<IntVec3> RoofRockThick = new List<IntVec3>();
        static List<IntVec3> RoofRockThin = new List<IntVec3>();

        static Dictionary<string, List<IntVec3>> TerrainData = new Dictionary<string, List<IntVec3>>();
        static Dictionary<string, List<IntVec3>> UnderTerrainData = new Dictionary<string, List<IntVec3>>();
        static Dictionary<string, List<IntVec3>> CompressedThingData = new Dictionary<string, List<IntVec3>>();
        static List<ThingHolder> UncompressedThingData = new List<ThingHolder>();
        //static Dictionary<string, ThingHolder> UncompressedThingData = new Dictionary<string, ThingHolder>();

        static void Main(string[] args)
        {
            StringBuilder result = new StringBuilder();
            System.IO.StreamWriter outputFile = new System.IO.StreamWriter(args[1]);

            XElement savegame = XElement.Load(args[0]);
            XElement game = savegame.Element("game");
            XElement maps = game.Element("maps");
            XElement li = maps.Element("li");

            IEnumerable<XElement> things = li.Elements();
            
            outputFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            outputFile.WriteLine("<Defs>");
            outputFile.WriteLine("  <TheEndTimes_Dwarfs.MapGenDef>");
            outputFile.WriteLine("		<defName>" + args[2] + "</defName>");
            outputFile.WriteLine("		<size>(250, 250)</size>");
            outputFile.WriteLine("		<defogPosition>(125,0,0)</defogPosition>");

            LoadMapDataToObjects(things, ref result);
            
            outputFile.WriteLine("		<MapData>");

            OutputSimpleData(outputFile, "Terrain", TerrainData);
            OutputSimpleData(outputFile, "Thing", CompressedThingData);
            OutputComplexData(outputFile, UncompressedThingData);
            
            outputFile.WriteLine("		</MapData>");

            outputFile.WriteLine("		<UnderTerrainData>");
            OutputSimpleData(outputFile, "Terrain", UnderTerrainData);
            outputFile.WriteLine("		</UnderTerrainData>");

            outputFile.WriteLine("		<RoofData>");

            OutputRoofData(outputFile, "RoofConstructed", RoofConstructed);
            OutputRoofData(outputFile, "RoofRockThick", RoofRockThick);
            OutputRoofData(outputFile, "RoofRockThin", RoofRockThin);


            outputFile.WriteLine("		</RoofData>");

            outputFile.WriteLine("  </TheEndTimes_Dwarfs.MapGenDef>");
            outputFile.WriteLine("</Defs>");
            outputFile.Close();
        }

        private static void OutputComplexData(StreamWriter outputFile, List<ThingHolder> uncompressedThingData)
        {
            foreach (ThingHolder thing in uncompressedThingData)
            {
                if (!thing.defName.Equals("HeronInvisibleDoor"))
                { 
                    outputFile.WriteLine("		    <li>");
                    outputFile.WriteLine("		        <key>");
                    if (thing.kind == null)
                        outputFile.WriteLine("		            <Thing>" + thing.defName + "</Thing>");
                    else
                        outputFile.WriteLine("		            <Kind>" + thing.kind + "</Kind>");
                    if (!thing.stuff.NullOrEmpty())
                        outputFile.WriteLine("		            <Stuff>" + thing.stuff + "</Stuff>");
                    if (thing.count == 0)
                        outputFile.WriteLine("		            <Rotate>" + thing.rot + "</Rotate>");
                    if (thing.count > 0)
                        outputFile.WriteLine("		            <Count>" + thing.count + "</Count>");
                    outputFile.WriteLine("		        </key>");
                    outputFile.WriteLine("		        <value>");

                    foreach (IntVec3 currPos in thing.positions)
                        outputFile.WriteLine("		            <li>" + currPos.ToString() + "</li>");

                    outputFile.WriteLine("		        </value>");
                    outputFile.WriteLine("		    </li>");
                }
            }
        }

        private static void OutputSimpleData(StreamWriter outputFile, string thingType, Dictionary<string, List<IntVec3>> data)
        {
            foreach (string key in data.Keys)
            { 
                outputFile.WriteLine("		    <li>");
                outputFile.WriteLine("		        <key>");
                outputFile.WriteLine("		            <" + thingType + ">" + key + "</" + thingType + ">");
                outputFile.WriteLine("		        </key>");
                outputFile.WriteLine("		        <value>");

                foreach (IntVec3 position in data.TryGetValue(key))
                {
                    outputFile.WriteLine("		            <li>" + position.ToString() + "</li>");
                }

                outputFile.WriteLine("		        </value>");
                outputFile.WriteLine("		    </li>");
            }
        }

        private static void OutputRoofData(StreamWriter outputFile, string defName, List<IntVec3> positions)
        {
            outputFile.WriteLine("		    <li>");
            outputFile.WriteLine("		        <RoofDef>" + defName + "</RoofDef>");
            outputFile.WriteLine("		        <Positions>");

            foreach (IntVec3 position in positions)
            {
                outputFile.WriteLine("		            <li>" + position.ToString() + "</li>");
            }

            outputFile.WriteLine("		        </Positions>");
            outputFile.WriteLine("		    </li>");
        }

        private static void LoadMapDataToObjects(IEnumerable<XElement> things, ref StringBuilder result)
        {
            foreach (XElement thing in things)
            {
                if (thing.Name.ToString().Equals("roofGrid"))
                {
                    XElement roofsDeflate = thing.Element("roofsDeflate");
                    string str = roofsDeflate.Value;

                    byte[] arr = (byte[])null;
                    if (str != null)
                    {
                        arr = CompressUtility.Decompress(Convert.FromBase64String(DataExposeUtility.RemoveLineBreaks(str)));

                        for (int index = 0; index < (MAP_SIZE * MAP_SIZE); ++index)
                        {
                            roofWriter(index, (ushort)((int)arr[index * 2] << 0 | (int)arr[index * 2 + 1] << 8));
                        }
                    }
                }
                else if (thing.Name.ToString().Equals("terrainGrid"))
                {
                    XElement underGridDeflate = thing.Element("underGridDeflate");
                    string underGridDeflateValue = underGridDeflate.Value;
                    byte[] underGridDeflated = (byte[])null;
                    if (underGridDeflateValue != null)
                    {
                        underGridDeflated = CompressUtility.Decompress(Convert.FromBase64String(DataExposeUtility.RemoveLineBreaks(underGridDeflateValue)));

                        for (int index = 0; index < (MAP_SIZE * MAP_SIZE); ++index)
                        {
                            underTerrainWriter(index, (ushort)((int)underGridDeflated[index * 2] << 0 | (int)underGridDeflated[index * 2 + 1] << 8));
                        }
                    }

                    XElement topGridDeflate = thing.Element("topGridDeflate");
                    string topGridDeflateValue = topGridDeflate.Value;
                    byte[] topGridDeflated = (byte[])null;
                    if (topGridDeflateValue != null)
                    {
                        topGridDeflated = CompressUtility.Decompress(Convert.FromBase64String(DataExposeUtility.RemoveLineBreaks(topGridDeflateValue)));

                        for (int index = 0; index < (MAP_SIZE * MAP_SIZE); ++index)
                        {
                            terrainWriter(index, (ushort)((int)topGridDeflated[index * 2] << 0 | (int)topGridDeflated[index * 2 + 1] << 8));
                        }
                    }
                }
                else if (thing.Name.ToString().Equals("compressedThingMapDeflate"))
                {
                    string compressedThingMapValue = thing.Value;
                    byte[] compressedThingMapDeflated = (byte[])null;
                    if (compressedThingMapValue != null)
                    {
                        compressedThingMapDeflated = CompressUtility.Decompress(Convert.FromBase64String(DataExposeUtility.RemoveLineBreaks(compressedThingMapValue)));

                        for (int index = 0; index < (MAP_SIZE * MAP_SIZE); ++index)
                        {
                            compressedThingWriter(index, (ushort)((int)compressedThingMapDeflated[index * 2] << 0 | (int)compressedThingMapDeflated[index * 2 + 1] << 8));
                        }
                    }
                }
                else if (thing.Name.ToString().Equals("things"))
                {
                    IEnumerable<XElement> thingsEnum = thing.Elements("thing");
                    
                    char[] delimiterChars = {','};

                    foreach (XElement currentThing in thingsEnum)
                    {
                        XElement def = currentThing.Element("def");
                        XElement pos = currentThing.Element("pos");
                        XElement rot = currentThing.Element("rot");
                        XElement count = currentThing.Element("stackCount");
                        XElement stuff = currentThing.Element("stuff");
                        XElement kind = currentThing.Element("kindDef");

                        string defValue = def.Value;
                        string stuffValue = null;
                        int rotValue = 0;
                        int countValue = 0;
                        string kindValue = null;

                        if (rot != null)
                        {
                            rotValue = Int32.Parse(rot.Value);
                        }
                        else
                        {
                            rotValue = 0;
                        }
                        if (count != null)
                        {
                            countValue = Int32.Parse(count.Value);
                        }
                        if (stuff != null)
                        {
                            stuffValue = stuff.Value;
                        }
                        if (kind != null)
                        {
                            kindValue = kind.Value;
                        }

                        string posVal = pos.Value;
                        posVal = posVal.Replace("(", "").Replace(")", "");
                        string[] posValues = posVal.Split(delimiterChars);
                        IntVec3 position = new IntVec3(Int32.Parse(posValues[0]), Int32.Parse(posValues[1]), Int32.Parse(posValues[2]));

                        ThingHolder thingCurr = FindThingByDefName(defValue);
                        if (thingCurr != null && MeetsCombinationRequirements(rot, count, stuff, kind, thingCurr))
                        {
                            thingCurr.AddPos(position);
                        }
                        else 
                            UncompressedThingData.Add(new ThingHolder(defValue, rotValue, position, countValue, stuffValue, kindValue));
                    }
                }
            }
        }

        private static ThingHolder FindThingByDefName(string defValue)
        {
            foreach(ThingHolder currThing in UncompressedThingData)
            {
                if (currThing.defName.Equals(defValue))
                    return currThing;
            }

            return null;
        }

        // Determine whether currThing matches the item we're attempting to store. Will determine whether a new entry is made in the OUTPUT
        // If we made it here, we know the def matches.
        private static bool MeetsCombinationRequirements(XElement rot, XElement count, XElement stuff, XElement kind, ThingHolder currThing)
        {
            int currRot = currThing.rot;
            int newRot = (rot == null ? 0 : Int32.Parse(rot.Value));
            int currCount = currThing.count;
            int newCount = (count == null ? 0 : Int32.Parse(count.Value));
            string currStuff = currThing.stuff;
            string newStuff = (stuff == null ? null : stuff.Value);
            string currKind = currThing.kind;
            string newKind = (kind == null ? null : kind.Value);

            if (currRot == newRot
                && currCount == newCount
                && currStuff == newStuff
                && currKind == newKind)
            {
                return true;
            }


            //if (kind != null && rot != null && count == null && stuff == null)// 
            //    return true;
            //else if (kind == null && rot == null && count == null && stuff == null)// 
            //    return true;
            //else if (stuff != null && rot == null && count == null && kind == null)// 
            //    return true;

            return false;
        }

        public static void roofShortWriter(IntVec3 c, ushort val)
        {
            if (val == 5133)
            {
                RoofConstructed.Add(c);
            }
            else if (val == 10820)
            {
                RoofRockThick.Add(c);
            }
            else if (val == 6699)
            {
                RoofRockThin.Add(c);
            }
        }

        public static void roofWriter (int idx, ushort data)
        {
            roofShortWriter(IndexToCell(idx), data);
        }

        public static void underTerrainWriter(int idx, ushort val)
        {
            IntVec3 c = IndexToCell(idx);

            String lookUpVal = TerrainKeyValues.TERRAIN_KEY_VALUE_USHORT.TryGetValue(val);

            if (!lookUpVal.NullOrEmpty())
            {
                if (!UnderTerrainData.ContainsKey(lookUpVal))
                {
                    List<IntVec3> locations = new List<IntVec3>();
                    locations.Add(c);

                    UnderTerrainData.Add(lookUpVal, locations);
                }
                else
                {
                    UnderTerrainData.TryGetValue(lookUpVal).Add(c);
                }
            }
            else
            {
                // Toss an error.
            }
        }

        public static void terrainWriter(int idx, ushort val)
        {
            IntVec3 c = IndexToCell(idx);

            String lookUpVal = TerrainKeyValues.TERRAIN_KEY_VALUE_USHORT.TryGetValue(val);

            if (!lookUpVal.NullOrEmpty())
            {
                if (!TerrainData.ContainsKey(lookUpVal))
                {
                    List<IntVec3> locations = new List<IntVec3>();
                    locations.Add(c);

                    TerrainData.Add(lookUpVal, locations);
                }
                else
                {
                    TerrainData.TryGetValue(lookUpVal).Add(c);
                }
            }
            else
            {
                // Toss an error.
            }
        }

        public static void compressedThingWriter(int idx, ushort data)
        {
            IntVec3 c = IndexToCell(idx);

            String lookUpVal = ThingKeyValues.THING_KEY_VALUE_USHORT.TryGetValue(data);

            if (!lookUpVal.NullOrEmpty())
            {
                if (!CompressedThingData.ContainsKey(lookUpVal))
                {
                    List<IntVec3> locations = new List<IntVec3>();
                    locations.Add(c);

                    CompressedThingData.Add(lookUpVal, locations);
                }
                else
                {
                    CompressedThingData.TryGetValue(lookUpVal).Add(c);
                }
            }
            else
            {
                // Toss an error.
            }
        }

        public static IntVec3 IndexToCell(int ind)
        {
            return CellIndicesUtility.IndexToCell(ind, MAP_SIZE);
        }

    }
}
