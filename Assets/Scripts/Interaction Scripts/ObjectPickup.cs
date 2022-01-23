using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : Interaction
{
    public InventoryManager inventoryManager;
    public string itemName;
    public bool hasChildren;

    public GameObject truckObject;

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
        Debug.Log("Pickup interaction");
        // Check we're on the right objective
        if (gameManager.activeObjective._ID == objectiveID)
        {
            // Try adding object to inventory
            if (inventoryManager.AddItem(itemName, truckObject))
            {
                // Remove key from game.
                if(hasChildren)
                {
                    this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    this.gameObject.SetActive(false);
                }

                // Condition met
                gameManager.ConditionMet(conditionName);
            }
            else
            {
                Debug.Log("Inventory too full for item...");
            }
        }
    }
}
