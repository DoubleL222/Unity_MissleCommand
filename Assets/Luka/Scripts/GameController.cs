using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour {
    public LayerMask enemyLayerMask;
    public Text roundText;
    private bool gameOver = false;
    private static GameController _instance;
    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
            }
            return _instance;
        }
    }
    private List<WeaponGeneric> allWeapons;
    private Dictionary<BuildingGeneric, bool> allBuildings = new Dictionary<BuildingGeneric, bool>();             //DICTIONARY OF ALL BUILDINGS WITH VALUES BEING IF THEY ARE ALIVE OR NOT
    private AudioSource successSource;
    // Use this for initialization
	void Start () {
        _instance = this;
        roundText.text = "0";
        successSource = GetComponent<AudioSource>();
        allWeapons = FindObjectsOfType<WeaponGeneric>().ToList();
        BuildingGeneric[] initBuildings = FindObjectsOfType<BuildingGeneric>();
        foreach (BuildingGeneric _b in initBuildings)
        {
            allBuildings.Add(_b, true);
        }
        gameOver = false;
	}

    public void LogDeath(BuildingGeneric _deadBuilding)
    {
        allBuildings[_deadBuilding] = false;

        foreach (BuildingGeneric _key in allBuildings.Keys)
        {
            if (allBuildings[_key] == true && _key.GetType().BaseType != typeof(WeaponGeneric))
            {
                return;
            }
        }
        GameOver();
    }

    private void GameOver()
    {
        gameOver = true;
        UIPanelScript.instance.GameEnded();
        Debug.Log("GAME OVER");
    }

    private void ResetAfterRound()
    {
        foreach (WeaponGeneric _w in allWeapons)
        {
            _w.ResetWeapon();
        }

        List<BuildingGeneric> keys = new List<BuildingGeneric>(allBuildings.Keys);

        foreach (var _key in keys)
        {
            _key.ResetBuilding();
            allBuildings[_key] = true;
        }
    }

    private bool IsThereMoreAmmo()
    {
        bool IsThereStillAmmo = false;
        foreach (WeaponGeneric _w in allWeapons)
        {
            if (_w.IsThereMoreAmmo())
            {
                IsThereStillAmmo = true;
            }
        }
        return IsThereStillAmmo;
    }

    public List<Vector2> getLiveBuildingLocations()
    {
        List<Vector2> posList = new List<Vector2>();
        foreach (var _kvp in allBuildings)
        {
            if (_kvp.Value)
            {
                posList.Add(_kvp.Key.gameObject.transform.position);
            }
        }
        return posList;
    }
    /*
    public void ClickDetected(Vector2 _clickLocation)
    {
        if (!IsThereMoreAmmo())
        {
            Debug.Log("NO MORE AMMO!");
            return;
        }
        
        float minDist = float.MaxValue;
        WeaponGeneric selectedWeapon = null;
        foreach (WeaponGeneric _w in allWeapons)
        {
            if (_w.alive)
            {
                float tempDist = _w.DistanceToDest(_clickLocation);
                if (tempDist < minDist)
                {
                    minDist = tempDist;
                    selectedWeapon = _w;
                }
            }
        }
        if (selectedWeapon != null)
        {
            selectedWeapon.FireWeapon(_clickLocation);
        } 

        WeaponGeneric selectedWeapon = null;

        foreach (WeaponGeneric _w in allWeapons)
        {
            if (_w.alive)
            {
                if (_w.IsPressed())
                {
                    selectedWeapon = _w;
                }
            }
        }
        if (selectedWeapon != null)
        {
            selectedWeapon.FireWeapon(_clickLocation);
        }
}
*/
    public void DragDetected(Vector2 _clickLocation, WeaponGeneric _selectedWeapon)
    {
        if (_selectedWeapon == null || !_selectedWeapon.alive || !IsThereMoreAmmo())
        {
            return;
        }
        else
        {
            _selectedWeapon.FireWeapon(_clickLocation);
        }
    }

    public void PlaySuccessSound()
    {
        successSource.Play();
    }

    public void RoundEnded(float currentMaxTime)
    {
        StartCoroutine(NextRoundAfterDelay(currentMaxTime));
    }

    IEnumerator NextRoundAfterDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NextRound();
        yield return null;
    }

    private void NextRound()
    {

        int roundCount = int.Parse(roundText.text);
        roundCount++;
        roundText.text = roundCount.ToString();
        if (!gameOver)
        {
            ResetAfterRound();
            BombDroppingScript.instance.StartRound();
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextRound();
        }
	}
}
