using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    public static Hero instance;
    public float upForce = 200f;
    public AudioClip boostSound;
    public AudioClip goalSound;
    public AudioClip crashSound;

    private bool soundPlayed = false;
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    private AudioSource source;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	    source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
	{
        GameControl.instance.StartGame();
        if (isDead == false)
        {
            if (Input.GetMouseButtonDown (0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Thrust");
                source.PlayOneShot(boostSound, 1f);
            }
        }
	}

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Ceiling")
        {
            rb2d.velocity = Vector2.zero;
            isDead = false;
            return;
        }
            rb2d.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            if (soundPlayed == false)
            {
                source.PlayOneShot(crashSound);
                soundPlayed = true;
            }
        GameControl.instance.HeroDied();
    }

    public void PlayGoalSound()
    {
        source.PlayOneShot(goalSound);
    }
}
