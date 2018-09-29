using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoScene : MonoBehaviour {


    public string scene = "";
    private void OnTriggerEnter(Collider other)
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
            lm.loadScene(scene);
    }


        private void OnDrawGizmos()
        {
            Utils.DrawBoxCollider(this, Color.magenta);
        }
    
}

