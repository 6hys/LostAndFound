using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : Interaction
{
    public InventoryManager inventoryManager;
    public GameObject endScreen;
    public PlayerController playerController;
    public AudioSource truckItemSound;

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
        // Putting items in the truck
        if (gameManager.activeObjective._ID == objectiveID)
        {
            List<GameObject> truckItems = new List<GameObject>();

            // Check inventory
            truckItems = inventoryManager.ReturnTruckItems();

            if(truckItems.Count > 0)
            {
                truckItemSound.Play();
            }

            // Display items in truck and progress conditions
            foreach (GameObject item in truckItems)
            {
                item.SetActive(true);
                gameManager.ConditionMet(item.GetComponent<TruckObject>().condition);
            }
        }
        // Leaving
        else if (gameManager.activeObjective._ID == objectiveID + 1)
        {
            // Game end.
            endScreen.SetActive(true);
            playerController.ToggleMovement(false);
        }
    }
}
