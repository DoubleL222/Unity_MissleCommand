using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelScript : MonoBehaviour {
    public GameObject startPanel;
    public GameObject endPanel;


    private static UIPanelScript _instance;
    public static UIPanelScript instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIPanelScript>();
            }
            return _instance;
        }
    }

    public void StartClicked()
    {
        startPanel.SetActive(false);
        GameController.instance.RoundEnded(2f);
    }

    public void GameEnded()
    {
        endPanel.SetActive(true);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
