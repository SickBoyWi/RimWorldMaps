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
    static class TerrainKeyValues
    {
        public static Dictionary<ushort, string> TERRAIN_KEY_VALUE_USHORT = new Dictionary<ushort, string>();

        static TerrainKeyValues ()
        {
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)8962, "Concrete");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)20203, "PavedTile");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)16966, "WoodPlankFloor");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)2806, "MetalTile");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)33573, "SilverTile");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)17351, "GoldTile");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)28773, "SterileTile");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)4526, "CarpetRed");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)21224, "CarpetGreen");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)64202, "CarpetBlue");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)62510, "CarpetCream");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)47591, "CarpetDark");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)59177, "BurnedWoodPlankFloor");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)1195, "BurnedCarpet");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)59992, "TileSandstone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)37600, "TileGranite");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)58272, "TileLimestone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)56283, "TileSlate");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)63102, "TileMarble");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)49581, "FlagstoneSandstone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)62889, "FlagstoneGranite");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)47861, "FlagstoneLimestone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)23099, "FlagstoneSlate");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)17409, "FlagstoneMarble");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)34465, "Soil");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)12446, "MossyTerrain");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)32751, "MarshyTerrain");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)32883, "SoilRich");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)65097, "Gravel");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)21158, "Sand");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)1433, "SoftSand");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)42032, "Mud");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)37631, "Ice");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)59853, "BrokenAsphalt");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)40270, "PackedDirt");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)7973, "Underwall");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)6727, "Bridge");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)31116, "WaterDeep");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)42896, "WaterOceanDeep");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)32789, "WaterMovingChestDeep");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)11445, "WaterShallow");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)65417, "WaterOceanShallow");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)26324, "WaterMovingShallow");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)34310, "Marsh");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)50658, "RH_TET_ChaosWastesSoil");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)36660, "RH_TET_ChaosWastesSoilRich");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)28958, "RH_TET_ChaosWastesGravel");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)37351, "RH_TET_ChaosWastesSand");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)10040, "Sandstone_Rough");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)14838, "Sandstone_RoughHewn");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)53402, "Sandstone_Smooth");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)61150, "Granite_Rough");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)30324, "Granite_RoughHewn");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)64967, "Granite_Smooth");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)56931, "Limestone_Rough");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)46162, "Limestone_RoughHewn");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)65262, "Limestone_Smooth");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)15764, "Slate_Rough");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)50533, "Slate_RoughHewn");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)34232, "Slate_Smooth");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)27449, "Marble_Rough");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)41863, "Marble_RoughHewn");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)3280, "Marble_Smooth");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)8937, "RH_TET_Dwarfs_FancyFloorGranite");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)39988, "RH_TET_Dwarfs_FancyFloorSandstone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)38268, "RH_TET_Dwarfs_FancyFloorLimestone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)38727, "RH_TET_Dwarfs_FancyFloorSlate");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)43151, "RH_TET_Dwarfs_FancyFloorMarble");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)52100, "RH_TET_Dwarfs_FineFloorGranite");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)36098, "RH_TET_Dwarfs_FineFloorSandstone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)34378, "RH_TET_Dwarfs_FineFloorLimestone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)7539, "RH_TET_Dwarfs_FineFloorSlate");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)59342, "RH_TET_Dwarfs_FineFloorMarble");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)20420, "RH_TET_Dwarfs_MidFloorGranite");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)64926, "RH_TET_Dwarfs_MidFloorSandstone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)63206, "RH_TET_Dwarfs_MidFloorLimestone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)25169, "RH_TET_Dwarfs_MidFloorSlate");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)16039, "RH_TET_Dwarfs_MidFloorMarble");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)13152, "RH_TET_Dwarfs_BasicFloorGranite");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)27153, "RH_TET_Dwarfs_BasicFloorSandstone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)25433, "RH_TET_Dwarfs_BasicFloorLimestone");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)13841, "RH_TET_Dwarfs_BasicFloorSlate");
            TERRAIN_KEY_VALUE_USHORT.Add((ushort)58085, "RH_TET_Dwarfs_BasicFloorMarble");
        }
    }
}
