using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public PlayerController playerController;
    public Interaction interactionScript;

    public bool raycastTrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (raycastTrigger)
            {
                playerController.SetRaycastActive(this.gameObject);
            }
            else
            {
                interactionScript.Interact();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && raycastTrigger)
        {
            playerController.DisableRaycast(this.gameObject);
        }
    }
}
