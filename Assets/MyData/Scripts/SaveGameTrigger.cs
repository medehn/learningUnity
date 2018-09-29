using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//save point, points towards SaveGameData class for further functionality and invokes saving

public class SaveGameTrigger : MonoBehaviour {

    //ID for actual trigger which was used last to prevent triggering multiple times
    public string ID = "";

    private void OnTriggerEnter(Collider other)
    {
        SaveGameData saveGame = SaveGameData.current;

        if (saveGame.lastTriggerID != ID)
        {
            saveGame.lastTriggerID = ID;
            saveGame.save();
        }

        else Debug.Log("Already saved!");
    }



    private void OnDrawGizmos()
    {
        Utils.DrawBoxCollider(this, Color.red);
    }
}
