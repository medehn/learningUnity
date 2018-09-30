using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    //filling and emptying health bar on HUD

    public Image progressBar;
    private Player player;

    // Update is called once per frame
    void Update()
    {

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        else
        {
            progressBar.fillAmount = player.health;
        }
    }
}
