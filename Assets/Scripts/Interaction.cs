using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameManager gameManager;

    public string conditionName;

    public int objectiveID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        // Check we're on the right objective
        if (gameManager.activeObjective._ID == objectiveID)
        {
            gameManager.ConditionMet(conditionName);
        }
    }
}
