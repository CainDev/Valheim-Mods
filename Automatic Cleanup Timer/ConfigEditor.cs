using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class ConfigEditor
{

    public void GenerateDummyData(string modname)
    {
        JObject jsonObj = new JObject();
        JArray arrayOne = new JArray();
        JArray arrayTwo = new JArray();

        switch (modname)
        {
            case "TimedCleanup":
                arrayOne.Add("300");
                arrayOne.Add("false");
                arrayTwo.Add(new string[] { 
                    "Wood(Clone)", 
                    "Stone(Clone)", 
                    "Resin(Clone)", 
                    "Flint(Clone)", 
                    "FirCone(Clone)", 
                    "PineCone(Clone)",
                    "CarrotSeeds(Clone)",
                    "Dandelion(Clone)",
                    "Thistle(Clone)",
                    "Hammer(Clone)",
                    "AxeStone(Clone)",
                    "TrophyDeer(Clone)",
                    "TrophyBoar(Clone)",
                    "RawMeat(Clone)",
                    "NeckTail(Clone)",
                    "Feathers(Clone)",
                    "DeerHide(Clone)",
                    "LeatherScraps(Clone)",
                    "GreydwarfEye(Clone)"
                });
                jsonObj["Settings"] = arrayOne;
                jsonObj["junkItems"] = arrayTwo;
                string dummyData = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                CheckFolderExistance(Utils.GetSaveDataPath() + "/configs", "TimedCleanup");
                WriteData(dummyData, Utils.GetSaveDataPath() + "/configs/cleanupconfig.json");
                break;
            default:
                // No Mod Name Provided
                break;
        }
    }

    public void WriteData(string text, string fileLocation)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileLocation))
            {
                writer.Write(text);
            }
        } 
        catch
        {
            // Handle Error
        }
    }

    public string ReadData(string fileLocation)
    {
        try
        {
            return File.ReadAllText(fileLocation);
        }
        catch
        {
            // Handle Error
            return "Not Found";
        }
    }

    public void CheckFolderExistance(string folderLocation, string modName)
    {
        try
        {
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
                GenerateDummyData(modName);
            }
        }
        catch
        {
            // Handle Error
        }
    }

    public void CheckFileExistance(string folderLocation, string modName)
    {
        try
        {
            if (!File.Exists(folderLocation))
            {
                GenerateDummyData(modName);
            }
        }
        catch
        {
            // Handle Error
        }
    }
}
