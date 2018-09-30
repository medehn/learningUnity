using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{

    public Image overlay; //Image which is faded in/out for level start


    private IEnumerator performFading(float toAlpha, bool revertToSaveGame, float delay = 0f) //IEnumerator pauses method! float delay = 0f means default param value is 0f
    {

        if (delay > 0){
            yield return new WaitForSeconds(delay);
        }

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
    public void fadeIn(bool revertToSaveGame, float delay = 0f)
    {
        StartCoroutine(performFading(0f, revertToSaveGame, delay));
    }

    public void fadeOut(bool revertToSaveGame, float delay = 0f)
    {
        StartCoroutine(performFading(1f, revertToSaveGame, delay));
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
