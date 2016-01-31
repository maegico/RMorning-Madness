using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Items : MonoBehaviour {

    //gm reference
    public GameManager gm;

    //identifiers
    public bool isPassive;
    public bool isInteractable;
    public int taskNumber;
    public bool used;

    //insanity checks
    private int currentPassiveInsanity;
    private int currentActiveInsanity;

	//Moving around based on insanity levels
    public Transform[] locations;

    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	void Start () {
        used = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //get current related insanity value
    private void CheckInsanity()
    {
        currentPassiveInsanity = gm.PassiveInsanityLevel;
        currentActiveInsanity = gm.ActiveInsanityLevel;
    }

    //method to react to insanity levels
    public void React()
    {
        if (locations.Length > 0)
        {
            //print("React");
            if (isPassive)
            {
                if (locations[gm.PassiveInsanityLevel] != null)
                {
                    transform.position = locations[gm.PassiveInsanityLevel].position;
                }
            }
            else
            {
                if (locations[gm.ActiveInsanityLevel - 1] != null)
                {
                    transform.position = locations[gm.ActiveInsanityLevel - 1].position;
                }
            }
        }
    }

    //limits one time use to each task
    public void Interacted()
    {
        //check if it can still be used
        if (!used)
        {
            used = true;
            print("I poked a " + gameObject.name);
            gm.CompleteTask(taskNumber);
        }
    }
}
