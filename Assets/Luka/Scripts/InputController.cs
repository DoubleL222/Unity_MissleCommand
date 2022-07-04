using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    private Camera mainCamera;
	// Use this for initialization
	void Start () {
        mainCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        /*   NAJA: Should be deleted, as it is not used anymore, because of touch control
         *   if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                GameController.instance.ClickDetected(clickWorldPos);
            } */
    }
}
