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

        Player p = other.gameObject.GetComponent<Player>();

        if (p == null)
        {
            //collision with something else than player, so ignore this collision! dont save when barrel saves :)
            return;
        }
        else if (p.health <= 0f)
        {
            //dont save if player falls into saving point while dying
            return;
        }
        else if (saveGame.lastTriggerID == ID)
        {
            Debug.Log("Already saved!");
        }
        else {
            saveGame.lastTriggerID = ID;
            saveGame.save(); 
        } }

    private void OnDrawGizmos()
    {
        Utils.DrawBoxCollider(this, Color.red);
    }
}
