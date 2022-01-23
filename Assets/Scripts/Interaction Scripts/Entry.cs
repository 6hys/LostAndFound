using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : Interaction
{
    public Animator doorAnimator;
    public AudioSource closeSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("entry interaction");

        if(gameManager.hasEnteredHouse == false)
        {
            gameManager.hasEnteredHouse = true;

            // Animate door closed
            doorAnimator.SetTrigger("CloseDoor");

            closeSound.Play();
        }
    }
}
