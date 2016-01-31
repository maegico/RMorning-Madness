using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //lists for daily ritual
    public List<int> ritualOrder;
    private List<GameObject> interactables;

    //ints for ritual management
    private int currentTask;
    private int passiveInsanityLevel;
    private int activeInsanityLevel;

    //gets
    public int PassiveInsanityLevel
    {
        get { return passiveInsanityLevel; }
    }
    public int ActiveInsanityLevel
    {
        get { return activeInsanityLevel; }
    }

    private int day;
    private int numIncorrect;
    private int numCompleted;

    public GameObject player;
    public ParticleSystem endSparkle;
    public GameObject knife;

    //did the player complete the order?
    private bool tasksCorrect;

	// Use this for initialization
	void Start () {
        //find all interactable objects and put them in the list
        interactables = new List<GameObject>(GameObject.FindGameObjectsWithTag("interactable"));
        print("interactables: " + interactables.Count);

        //initialize ritual order with amount of interactables
        ritualOrder = new List<int>();
        for (int i = 1; i < interactables.Count + 1; i++)
        {
            ritualOrder.Add(i);
        }

        tasksCorrect = true;

        currentTask = 1;
        day = 1;
        numIncorrect = 0;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void CompleteTask(int taskCompleted)
    {
        numCompleted++;

        //check for the correct task to be completed
        if (taskCompleted == currentTask)
        {
            print("Correct");
            //next task
            if (currentTask < ritualOrder[ritualOrder.Count - 1])
            {
                currentTask = ritualOrder[currentTask];
            }
        }
        else
        {
            print("Incorrect");

            //incorrect task completed
            tasksCorrect = false;
            numIncorrect++;

            //remove task from list
            ritualOrder.Remove(taskCompleted);
        }

        //check if they finished the daily tasks
        if (taskCompleted == interactables.Count)
        {
            //fade to black
            //GetComponent<ScreenFader>().SceneEnd();

            print("Complete.");
            day++;
            currentTask = 1;

            //check to see how many tasks they forgot
            numIncorrect += interactables.Count - numCompleted;

            //update insanity levels
            UpdateInsanity();

            print("Current Sanity Levels: Passive: " + passiveInsanityLevel + "  Active: " + activeInsanityLevel);

            foreach (GameObject g in interactables)
            {
                //print("Loop interact");
                if (g.GetComponent<Items>() != null)
                {
                    g.GetComponent<Items>().React();

                    if (activeInsanityLevel < 3)
                    {
                        g.GetComponent<Items>().used = false;
                    }
                    else
                    {
                        g.GetComponent<Items>().used = true;
                        g.tag = "Untagged";
                        knife.tag = "interactable";
                        endSparkle.enableEmission = true;
                    }
                }
            }

            //reset shit
            player.GetComponent<Player>().ResetPlayer();
        }
    }

    private void UpdateInsanity()
    {
        //natural difficulty increase
        passiveInsanityLevel += 1;
        
        //difficulty increase from failure
        if (!tasksCorrect) { activeInsanityLevel += 1; }

        //reset values
        tasksCorrect = true;
        numIncorrect = 0;
        
    }
}
