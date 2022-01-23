using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doormat : Interaction
{
    private Vector3 startPos = new Vector3(-1.65f, 1.8f, -15.75f);

    private Vector3 endPos = new Vector3(-1f, 1.8f, -15.75f);
    private Vector3 endRot = new Vector3(0f, 17f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = startPos;
        this.gameObject.transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        // Check we're on the right objective
        if(gameManager.activeObjective._ID == objectiveID)
        {
            // Move doormat aside
            this.gameObject.transform.position = endPos;
            this.gameObject.transform.eulerAngles = endRot;

            // Update objective
            gameManager.ConditionMet(conditionName);
        }
    }
}
