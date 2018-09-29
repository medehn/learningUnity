using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menuRoot;
    

    // Use this for initialization
    void Start()
    {
        menuRoot.gameObject.SetActive(true);
      

        //menu off at start of game
        menuRoot.SetActive(false);

    }

    //to make sure that menu is only opened or closed once at a time, default false
    private bool keyPressed = false;

    // Update is called once per frame
    void Update()
    {
        //Menu Key pressed: invert visibility of menu
        if (Input.GetAxisRaw("Menu") > 0f)
        {
            if (!keyPressed)
            {
                menuRoot.SetActive(!menuRoot.activeSelf);

                Time.timeScale = menuRoot.activeSelf ? 0f : 1f; //ternär - 1. if (canvas enabled) ?true -->0f, :false -->1f setting time to zero when menu is visible
            }
           keyPressed = true;
        }
        else
        {
            keyPressed = false;
        }
    }

        //start new game on button click in menu
        public void startGameButton()
        {
            
            SaveGameData.current = new SaveGameData();
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.loadScene("Level1");

        menuRoot.SetActive(false);
            Time.timeScale = 1f;

    }

        public void quitGameButton()
        {
            Application.Quit();
            Debug.Log("Spiel beenden!");
        }
    }
