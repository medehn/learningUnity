using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//managing behaviour of our game, mainly starting the load circle

public class LevelManager : MonoBehaviour {


    private void Awake()
    {
        //setting the current savegame to the one that is loaded
        SaveGameData.current = SaveGameData.load();

    }

    private void Start()
    {
        loadScene(SaveGameData.current.recentLevel);
    }
    public void loadScene (string name)
    {
        if (name == "")
            return;

        //remove/unload all scenes exect global (0.)scene
        for(int i=SceneManager.sceneCount-1; i>0; i--)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).name);
        }

        Debug.Log("Lade jetzt: " + name);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
       
    }

    //debugging-possibility - enables loading of current saved state on pressing key "1"

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Alpha1))
    //        SaveGameData.load();
    //}
}
