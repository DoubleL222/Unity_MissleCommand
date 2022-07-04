using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{


    public float bombSpeed = -0.02f;
    Vector3 bombPos;
    Vector3 goalPos;
    private float goalXpos;
    public float goalY;
    public float speed = 1;
    public GameObject bomber;
    Vector3 pos;
    System.Random randChar = new System.Random();

    //arrays for the goals
    int[] leftGoals;
    int[] middleGoals;
    int[] rightGoals;


    void Start()
    {

        pos = GameObject.Find("BOMBER").transform.position; //finds where the bomb should be dropped from
        goalY = 10;
        transform.position = pos;
        goalXpos = findRandomGoal(); //find the goal for this bomb
        goalPos = new Vector3(goalXpos, -4, pos.z);




    }

    void Update()
    {

        throwBomb();

    }

    public void throwBomb()
    {
        transform.position = Vector3.MoveTowards(transform.position, goalPos, 0.1f); //move towards the goal
    }

    private float findRandomGoal()
    {
        int randNum;
        int[] goalArray;
        int tempGoalX;

        if (pos.x < 0)
        {
            //choose left city, left canon or middle city
            goalArray = new int[] { -5, -3, 0 }; //The x  for the goals on the left side; 
            randNum = randChar.Next(0, 3);
            tempGoalX = goalArray[randNum];
            return tempGoalX;

        }
        else if (pos.x > 0)
        {
            //choose right city, right canon or middle city
            goalArray = new int[] { 0, 3, 5 }; //The x position for the goals on the right side
            randNum = randChar.Next(0, 3);
            tempGoalX = goalArray[randNum];
            return tempGoalX;

        }
        else if (pos.x == 0)
        {
            //choose right city, right canon, left city or left canon
            goalArray = new int[] { -5, -3, 3, 5 }; // The x position for all goals besides the middle goal
            randNum = randChar.Next(0, 4);
            tempGoalX = goalArray[randNum];
            return tempGoalX;
        }

        return 0;
    }



    //DESTROYING THE GOALS (CITY AND CANONS)
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "city")
        {
            Destroy(collision.gameObject);
            bomber.SendMessage("updateArrays", collision.gameObject.transform.position.x);

        }
        if (collision.gameObject.tag == "canons")
        {
            Destroy(collision.gameObject);
            bomber.SendMessage("updateArrays", collision.gameObject.transform.position.x);
        }
    }

    public void updateGoals(int[][] allGoals)
    {
        this.leftGoals = allGoals[0];
        this.middleGoals = allGoals[1];
        this.rightGoals = allGoals[2];

    }



}


