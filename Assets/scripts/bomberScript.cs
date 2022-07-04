using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class bomberScript : MonoBehaviour
{

    public float randomCount = 0;
    public Vector3 bomberPosition;
    System.Random rand = new System.Random();
    float randomX;
    float goalX;
    public GameObject bombPrefab;

    float animationTimer = 0;
    public float bombTimer = 0.5f; //how often the bombs should be thrown
    public GameObject bomb1;

    int[] leftGoals;
    int[] middleGoals;
    int[] rightGoals;
    int[][] allGoals;


    private void Start()
    {
        leftGoals = new int[] { -5, -3, 0 }; //The x  for the goals on the left side
        middleGoals = new int[] { -5, -3, 3, 5 }; // The x position for all goals besides the middle goal
        rightGoals = new int[] { 0, 3, 5 }; //The x position for the goals on the right side
        allGoals = new int[][] { leftGoals, middleGoals, rightGoals };

        bomb1.SendMessage("updateGoals", allGoals); //update goals at start
    }

    void Update()
    {
        moveBomber();
    }

    private void moveBomber()
    {
        animationTimer += Time.deltaTime;
        if (animationTimer >= bombTimer)
        {
            randomX = rand.Next(-6, 6);

            //sets bomber position
            bomberPosition = new Vector3(randomX, 6, 0);
            transform.position = bomberPosition;

            bombTimer += 2f; //next movement should be in 0,5 seconds

            Instantiate(bomb1);

        }
    }


    //TODO: NAJA: I'm working on removing the destroyed goals from the array of goals
    public void updateArrays(float xPos)
    {
      //  Debug.Log("DESTROYED: " + xPos);
        bomb1.SendMessage("updateGoals", allGoals); //update goals after this
    }



}
