using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : Saveable

    //Opening a door on Click with a pseudo switch
{

    public Animator doorAnimator;
    public MeshRenderer mesh;

    //adding a collider object (3D Box added in Unity) to fire interaction with switch/to open door
    private void OnTriggerStay(Collider other)
    {
        //if the input gets Fire1 aka space, do openTheDoor()
        if (Input.GetAxisRaw("Fire1") != 0f && !doorAnimator.GetBool("isOpen"))
        {
            openTheDoor();
        }
    }

    //"isOpen" is set in the Animator as false, if this method is called, change to true and change the materials aka lamps on the switch
    private void openTheDoor()
    {
        doorAnimator.SetBool("isOpen", true);

        Material[] mats = mesh.materials;
        Material m2 = mats[2];

        mats[2] = mats[1];
        mats[1] = m2;
        mesh.materials = mats;

    }

    //what happens when game is saved
    protected override void saveme(SaveGameData savegame)
    {
        base.saveme(savegame); //to make sure base class is also operated
        savegame.doorIsOpen = doorAnimator.GetBool("isOpen");
    }

    //what happens when game is loaded
    protected override void loadme(SaveGameData savegame)
    {
        base.loadme(savegame);
        if (savegame.doorIsOpen)
            openTheDoor();
    }

    //to see where the collider is even if not focused, draw the outlines into the scene view - doesnt affect actual game!

        private void OnDrawGizmos()
        {
            Utils.DrawBoxCollider(this, Color.cyan);
        }
    
}
