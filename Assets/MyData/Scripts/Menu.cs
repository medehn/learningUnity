using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //menu off at start of game
        GetComponent<Canvas>().enabled = false;

    }

    //to make sure that menu is only opened or closed once at a time, default false
    private bool keyPressed = false;

	// Update is called once per frame
	void Update () {

        //Menu Key pressed: invert visibility of menu
        if (Input.GetAxisRaw("Menu") > 0f)
        {
            if(!keyPressed)
                GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
                keyPressed = true;
        }
        else
            keyPressed = false; //as updated every frame, this checks if the menu key was pressed in the frame, if not pressed trigger menu only once
	}

    //start new game on button click in menu
    public void startGameButton()
    {
        SaveGameData.current = new SaveGameData();
        LevelManager lm = FindObjectOfType<LevelManager>();
        lm.loadScene("Level1");

        GetComponent<Canvas>().enabled = false;

    }

    public void quitGameButton()
    {
        Application.Quit();
        Debug.Log("Spiel beenden!");
    }
}
