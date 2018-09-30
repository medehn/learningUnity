using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevProfi.Utils;
using System.IO;

//Annotation um speicherbar zu machen
[System.Serializable]

public class SaveGameData  {

    //current saveGame
    public static SaveGameData current = new SaveGameData();

    public Vector3 playerPosition = Vector3.zero;
    public float playerHealth = 1f;

    public bool doorIsOpen = false;
    public string lastTriggerID = "";
    public string recentLevel = "";

    //blueprint to access the EventHandler for saving and loading in e.g. the DoorSwitch 
    public delegate void SaveHandler(SaveGameData savegame);

    //Actual handlers 
    public static event SaveHandler onSave;
    public static event SaveHandler onLoad;

   //configuring the filename for the savegame to a path specific to whatever device and whatever convention of /\
    private static string getFilename()
    {
        return Application.persistentDataPath +System.IO.Path.DirectorySeparatorChar+ "saveGame.xml";
    }

    //actual saving method
    public void save()
    {
        Debug.Log("Speichere Spielstand "+ getFilename());

        //if there are any things in onSave from other classes like the door, run the Eventhandler
        if (onSave != null) onSave(this);

        //access the XML file which handles saving 
        string xml = XML.Save(this);
        File.WriteAllText(getFilename(), xml);

        Debug.Log(xml);

    }

    //actual load method
    public static SaveGameData load()
    {
        //in case there is no savegame - create a new one
        if (!File.Exists(getFilename()))
            return new SaveGameData();

        //get the data out of the savegame XML file, again accessing the XML class
        SaveGameData save = XML.Load<SaveGameData>(File.ReadAllText(getFilename()));



        //if anything is in the Handler, load aswell
        if(onLoad!=null) onLoad(save);

        return save;
    }

}
