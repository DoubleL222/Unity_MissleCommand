using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ProximityChecker : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ON TRIGGER ENTER FOR MISSLE");
        if (GameController.instance.enemyLayerMask == (GameController.instance.enemyLayerMask | (1 << other.gameObject.layer)))
        {
            other.GetComponent<E_BombController>().DestroyBomb();
        }
    }
}
