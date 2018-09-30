using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Danger source, hurting or killing player

public class Danger : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();

        if (p != null) //when collision with player
        {
            p.looseHealth();
        }
    }
}
