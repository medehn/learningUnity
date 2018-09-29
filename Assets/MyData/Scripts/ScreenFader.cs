using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{

    public Image overlay; //Image which is faded in/out for level start


    private IEnumerator performFading(float toAlpha, bool revertToSaveGame) //IEnumerator pauses method!
    {
        overlay.CrossFadeAlpha(toAlpha, 1f, false);

        yield return new WaitForSeconds(1f); //waiting for one second
        //loading level after death

        if (revertToSaveGame)
        {
            SaveGameData.current = SaveGameData.load();
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.loadScene(SaveGameData.current.recentLevel);
        }
    }

    //reverttosavegame prevents multiple loads of levels when false - only true after fadeout, so when no scene is active, set in player update
    public void fadeIn(bool revertToSaveGame)
    {
        StartCoroutine(performFading(0f, revertToSaveGame));
    }

    public void fadeOut(bool revertToSaveGame)
    {
        StartCoroutine(performFading(1f, revertToSaveGame));
    }

    //whenevre a scene is loaded, fadeIn
    private void Awake()

    {
        overlay.gameObject.SetActive(true);
        SceneManager.sceneLoaded += whenLevelWasLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= whenLevelWasLoaded;
    }

    public void whenLevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeIn(false);

    }
}
