# Automatic Cleanup Timer
This timer will run whenever you set it too. You can change the settings to wipe all items or only selected items. The choice is yours!

## Setup
After the first time you run the mod it will generate the settings file. 

You can find this file here C:\Users\[YOUR NAME]\AppData\LocalLow\IronGate\Valheim\configs

Right click and edit this with a file editor of your choice (Notepad will do!)

It will look like this
```json
{
  "Settings": [
    "300", < This is how often you want the timer to run (in seconds)
    "false" < This is if you want to wipe everything. [true = Destroy ALL item entities, false = follow the junk item list below]
  ],
  "junkItems": [ 
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
  ]
}
```

I have included an ItemList.txt in the Github so you can switch these items out. This is CaSe SeNsItIve so please make sure you modify this list carefully.


