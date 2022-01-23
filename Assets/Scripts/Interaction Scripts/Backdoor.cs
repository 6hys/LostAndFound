using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdoor : Interaction
{
    public Animator doorAnimator;
    public InventoryManager inventoryManager;
    public Light tvLight;
    public AudioSource openSound;
    public AudioSource unlockSound;

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
        Debug.Log("backdoor interaction");

        // Check we're on the right objective
        if (gameManager.activeObjective._ID == objectiveID)
        {
            StartCoroutine(DoorUnlock());
        }
        else if(gameManager.hasEnteredHouse)
        {
            // Play door rattle sound

            // Display message
            Debug.Log("It looks like it locked behind you...");
        }
    }

    IEnumerator DoorUnlock()
    {
        // Remove key from inventory
        inventoryManager.RemoveItem("Backdoor Key");

        // Play unlocking sound
        unlockSound.Play();

        while(unlockSound.isPlaying)
        {
            yield return null;
        }

        // Animate door open
        doorAnimator.SetTrigger("OpenDoor");

        // Play door sound
        openSound.Play();

        // Update objective
        gameManager.ConditionMet(conditionName);

        // Turn "TV" on
        tvLight.enabled = true;
    }
}
