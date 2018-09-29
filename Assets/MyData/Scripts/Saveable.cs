using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent for inheritance for objects that are ment to be saved
public class Saveable : MonoBehaviour {

    //awake runs before everything else
    protected virtual void Awake()
    {
        SaveGameData.onSave += saveme;
        SaveGameData.onLoad += loadme;
    }

    protected virtual void Start()
    {
        loadme(SaveGameData.current);
    }

    //what happens when game is saved
    protected virtual void saveme(SaveGameData savegame)
    {
    }

    //what happens when game is loaded
    protected virtual void loadme(SaveGameData savegame)
    {
    }

    //alwas destroy instances to avoid memory leaks!
    protected virtual void OnDestroy()
    {
        SaveGameData.onLoad -= loadme;
        SaveGameData.onSave -= saveme;
    }
}
