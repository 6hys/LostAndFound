using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : Interaction
{
    public Animator doorAnimator;
    public InventoryManager inventoryManager;
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
        // Check we're on the right objective
        if (gameManager.activeObjective._ID == objectiveID && inventoryManager.CheckForItem("Front Door Key"))
        {
            StartCoroutine(DoorUnlock());
        }
    }

    IEnumerator DoorUnlock()
    {
        // Remove key from inventory
        inventoryManager.RemoveItem("Front Door Key");

        // Play unlocking sound
        unlockSound.Play();

        while (unlockSound.isPlaying)
        {
            yield return null;
        }

        // Animate door open
        doorAnimator.SetTrigger("OpenDoor");

        // Play door sound
        openSound.Play();

        // Update objective
        gameManager.ConditionMet(conditionName);
    }
}
