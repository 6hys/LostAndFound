using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public XmlLoader xmlLoader;
    public Canvas canvas;

    // Progress checks
    private bool isInCutscene;
    public bool InCutscene
    {
        get { return isInCutscene; }
        set 
        { 
            isInCutscene = value;
            canvas.gameObject.SetActive(!value);
        }
    }

    public bool hasEnteredHouse = false;
    public bool hasUsedTorch = false;

    public Objectives objectives = new Objectives();
    public ObjectiveData activeObjective;
    private bool[] conditionsMet;
    private string xmlFile = "Quests";

    // UI
    public Text activeTitle;
    public Text activeDesc;
    public GameObject emailUI;
    public GameObject inGameUI;
    public bool isEmailOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        objectives = xmlLoader.Load(objectives, xmlFile);
        activeObjective = objectives._objectives[0];

        activeTitle.text = activeObjective._title;
        activeDesc.text = activeObjective._description;

        conditionsMet = new bool[activeObjective._conditions.Length];
        for(int i = 0; i < activeObjective._conditions.Length; i++)
        {
            conditionsMet[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }  

    public void ConditionMet(string cond)
    {
        for(int i = 0; i < activeObjective._conditions.Length; i++)
        {
            if(activeObjective._conditions[i] == cond)
            {
                conditionsMet[i] = true;
                break;
            }
        }

        bool condsMet = true;
        foreach(bool metCond in conditionsMet)
        {
            if(!metCond)
            {
                condsMet = false;
                break;
            }
        }

        if(condsMet)
        {
            NextObjective();
        }
    }

    private void NextObjective()
    {
        activeObjective = objectives._objectives[activeObjective._nextStep];

        activeTitle.text = activeObjective._title;
        activeDesc.text = activeObjective._description;

        conditionsMet = new bool[activeObjective._conditions.Length];
        for (int i = 0; i < activeObjective._conditions.Length; i++)
        {
            conditionsMet[i] = false;
        }
    }

    public void ToggleEmails()
    {
        isEmailOpen = !isEmailOpen;
        emailUI.SetActive(isEmailOpen);

        if (isEmailOpen)
        {
            if (activeObjective._ID == 0)
            {
                ConditionMet("EmailChecked");
            }

            // Hide rest of the UI
            inGameUI.SetActive(false);
        }
        else if(!isEmailOpen)
        {
            // Show rest of UI
            inGameUI.SetActive(true);
        }
    }
}
