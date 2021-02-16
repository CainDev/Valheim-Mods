using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

class TimedCleanup : MonoBehaviour
{
    /*
     * Author Information;-
     * Name: Hammond
     * Steam Link: https://steamcommunity.com/profiles/76561199083717641
     * GitHub Link: https://github.com/CainDev
     * 
     * Development Information;-
     * Version 0.1
     * Last Update: 16/02/21
     * 
     * Credits;-
     * Redphoenix (From Valheim Discord, Thanks for the constant help and corrections.)
     * Valheim Devs (Thanks for making such a fun game.)
     * Valheim Discord: https://discord.gg/AegsZrfDWf
     */

    private float timerSetting = 300f;
    private float timer;
    private bool isTimerRunning = false;

    private bool settingsImported = false;
    public Dictionary<string, string[]> modSettings = new Dictionary<string, string[]>();
    private List<string> allJunkItems = new List<string>();
    private bool wipeAllItems = false;
    private ConfigEditor editor = new ConfigEditor();
    

    void Start()
    {
        timer = timerSetting;
    }

    void Update()
    {
        CleanupTimer();
    }

    public void CleanupTimer()
    {
        if (settingsImported)
        {
            if (isTimerRunning)
            {
                if(timer > 0f)
                {
                    timer -= Time.deltaTime;
                    if(timer < 10)
                    {
                        Console.instance.AddString("Removing ground entities in " + timer.ToString("0"));
                    }
                } 
                else
                {
                    isTimerRunning = false;
                    Console.instance.AddString("Cleaning Ground Entities!");
                    
                    try
                    {
                        ItemDrop[] groundItems = FindObjectsOfType<ItemDrop>();

                        if (wipeAllItems)
                        {
                            for (int i = 0; groundItems.Length > 0; i++)
                            {
                                ZNetView groundItemComponent = groundItems[i].GetComponent<ZNetView>();

                                if (groundItemComponent)
                                {
                                    groundItemComponent.Destroy();
                                }
                            }
                        } 
                        else
                        {
                            
                            for (int i = 0; groundItems.Length > 0; i++)
                            {
                                ZNetView groundItemComponent = groundItems[i].GetComponent<ZNetView>();

                                if (allJunkItems.Contains(groundItemComponent.name))
                                {
                                    if (groundItemComponent)
                                    {
                                        groundItemComponent.Destroy();
                                    }   
                                }
                            }
                        }
                        

                        if (groundItems.Length == 0 || groundItems == null)
                        {
                            Console.instance.AddString("No entities to remove!");
                        }
                        else
                        {
                            Console.instance.AddString("Removed " + groundItems.Length.ToString() + " Entities. Restarting timer now.");
                        }

                        isTimerRunning = true;
                        timer = timerSetting;
                    } 
                    catch 
                    {
                        Console.instance.AddString("Error when clearing items!");
                    }
                }
            }
        }
        else
        {
            settingsImported = ImportSettings();
        }
    }

    // Import Settings from AppData\LocalLow\IronGate\Valheim\configs\cleanupconfig.json
    private bool ImportSettings()
    {
        try
        {
            // Check Folder/File Existance (Generates dummy data if not present)
            editor.CheckFolderExistance(Utils.GetSaveDataPath() + "/configs", "TimedCleanup");
            editor.CheckFileExistance(Utils.GetSaveDataPath() + "/configs/cleanupconfig.json", "TimedCleanup");

            // Grabs the Data from the File
            string jsonObject = editor.ReadData(Utils.GetSaveDataPath() + "/configs/cleanupconfig.json");

            // Converts Json to Dictionary Array
            modSettings = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonObject);

            // Import Item Settings
            wipeAllItems = bool.Parse(modSettings["Settings"][1]);
            foreach (string junkItem in modSettings["junkItems"])
            {
                allJunkItems.Add(junkItem);
            }

            // Import Timer Settings
            timerSetting = float.Parse(modSettings["Settings"][0]);
            

            // Return true so timer will begin.
            return true;
        }
        catch
        {
            // Failed to Import Items
            return false;
        }
        
    }
}