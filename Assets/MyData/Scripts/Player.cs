﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


/// <summary>
/// Control for player movement 
/// </summary>
public class Player : Saveable
{
    public float speed = 0.05f;
    public GameObject model;
    private float towardsY = 0; //Degree of turning around itself
    private Rigidbody rigid;
    private Animator anim;
    public float jumpPush = 1f;
    public float extraGravity = 20f;
    private bool onGround = false;
    public GameObject cameraTarget;

    protected override void Awake()
    {
        CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();

        if (cvc != null)
        {
            cvc.Follow = transform;
            cvc.LookAt = cameraTarget.transform;
        }
        base.Awake();
    }

    protected override void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        base.Start();
        setRagdollMode(false);
    }

    //what happens when game is saved
    protected override void saveme(SaveGameData savegame)
    {
        base.saveme(savegame);
        //where is the player?
        savegame.playerPosition = transform.position;

        //assign recentLevel ID
        savegame.recentLevel = gameObject.scene.name;

        savegame.playerHealth = health;
    }

    //what happens when game is loaded
    protected override void loadme(SaveGameData savegame)
    {
        base.loadme(savegame);
        if (savegame.recentLevel == gameObject.scene.name) //if loaded scene is same scene that saved position - load position
            transform.position = savegame.playerPosition;
        health = Mathf.Clamp01(savegame.playerHealth); //never <0 or >1 so no loading with less than zero or more than 1 health 
    }

    public float health = 1f; //full health

    //switching on/off ragdoll-mode (character dies)
    private void setRagdollMode(bool isDead)
    {

        //if isdead = true: switch components off and ragdoll on
        //important: getcomponentS finds also siblings, not only children, so do this first and specify other components afterwards!
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = isDead;
        }
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = !isDead;
        }

        GetComponent<Rigidbody>().isKinematic = isDead;
        GetComponent<Collider>().enabled = !isDead;
        GetComponentInChildren<Animator>().enabled = !isDead;

        if (isDead)
        {
            //adding fading to player
            ScreenFader sf = FindObjectOfType<ScreenFader>();
            sf.fadeOut(true, 1f);

            CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
            //when fadeOut starts, camera stops following player
            if (cvc != null)
            {
                cvc.Follow = null;
                cvc.LookAt = null;
            }

            enabled = false;
        }

    }
    //player dies
    public void looseHealth()
    {
        health -= 0.5f; //whenever hit, loose half health

        if (health <= 0)
        {
            setRagdollMode(true);
        }
    }


    // Update is called once per frame
    private void Update()
    {
        //when player falls off plane - player dies
        if (transform.position.y < -1.4f)
        {
            looseHealth();
            return;
        }

        if (Time.timeScale == 0f) return;//when paused, stop all following updates

        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("forward", Mathf.Abs(h));

        transform.position += h * speed * transform.forward;

        //Turning around
        if (h > 0f)
            towardsY = 0f;
        else if (h < 0f)
            towardsY = -180f;

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0f, towardsY, 0f), Time.deltaTime * 10f);

        //jumping
        //checking of on ground or already jumping
        RaycastHit hitInfo;
        onGround = Physics.Raycast(transform.position + (Vector3.up * 0.3f), Vector3.down, out hitInfo, 0.6f);

        //slide down slope

        if (onGround && Vector3.Angle(Vector3.up, hitInfo.normal) > 10)
        {
            rigid.AddForce(hitInfo.normal);
        }
        anim.SetBool("grounded", onGround);

        float j = Input.GetAxis("Jump");

        if (j > 0f && onGround)
        {
            Vector3 power = rigid.velocity;
            power.y = jumpPush;
            rigid.velocity = power;
        }
        rigid.AddForce(new Vector3(0f, extraGravity, 0f));
    }
}
