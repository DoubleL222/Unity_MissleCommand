using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDroppingScript : MonoBehaviour {
    private static BombDroppingScript _instance;
    public static BombDroppingScript instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BombDroppingScript>();
            }
            return _instance;
        }
    }

    private float minWaitingTime, maxWaitingTime;
    private int minMissileSpawns, maxMissileSpawns, missileCount, missilesInRound, missilesDestroyed;
    private bool roundRunning = false;
    private float nextReleaseTime = float.MinValue;
    private float releaseRange = 6.5f;
    private GameObject bombPrefab;
    int roundCount = 0;

    public void BombDestroyed()
    {
        missilesDestroyed++;
        if (missilesDestroyed >= missilesInRound)
        {
            roundRunning = false;
            GameController.instance.PlaySuccessSound();
            Debug.Log("all bombs destroyed");
            GameController.instance.RoundEnded(maxWaitingTime);
        }
    }

    void Start ()
    {
        _instance = this;
		bombPrefab = Resources.Load("E_Bomb") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (roundRunning)
        {
            if (Time.time > nextReleaseTime)
            {
                DoBombRelease();
                nextReleaseTime = Time.time + Random.Range(minWaitingTime, maxWaitingTime);
            }
        }
	}

    public void StartRound()
    {
        if (roundCount == 0)
        {
            SetInitialDifficulty();
        }
        else
        {
            IncreaseDifficulty();
        }
        ResetRound();
        roundRunning = true;
        roundCount++;
    }

    private void IncreaseDifficulty()
    {
        minWaitingTime -= 0.2f;
        maxWaitingTime -= 0.4f;
        //minMissileSpawns += 1;
        maxMissileSpawns += 1;
        missilesInRound += 5;
    }

    private void ResetRound()
    {
        missileCount = missilesInRound;
        missilesDestroyed = 0;
    }

    private void DoBombRelease()
    {
        int bombsToRelease = Random.Range(minMissileSpawns, maxMissileSpawns+1);
        if (bombsToRelease > missileCount)
        {
            bombsToRelease = missileCount;
        }
        List<Vector2> possibleDestinations = GameController.instance.getLiveBuildingLocations();
        if (possibleDestinations.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < bombsToRelease; i++)
        {
            ReleaseSingleBomb(possibleDestinations[Random.Range(0, possibleDestinations.Count)]);
        }
    }

    private void ReleaseSingleBomb(Vector2 _dest)
    {
        Vector2 bombSource = new Vector2(Random.Range(-releaseRange, releaseRange), transform.position.y);
        float angle = Vector2.Angle(_dest - bombSource, Vector2.down);
        if (_dest.x - bombSource.x < 0)
        {
            angle = -angle;
        }
        GameObject bombInstance = Instantiate(bombPrefab, bombSource, Quaternion.Euler(0, 0, angle)) as GameObject;
        bombInstance.GetComponent<E_BombController>().Launch(_dest);
        missileCount--;
    }

    private void SetInitialDifficulty()
    {
        minWaitingTime = 2.5f;
        maxWaitingTime = 5f;
        minMissileSpawns = 1;
        maxMissileSpawns = 2;
        missilesInRound = 10;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(releaseRange, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-releaseRange, 0, 0));
    }
}
