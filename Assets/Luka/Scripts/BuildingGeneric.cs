using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class BuildingGeneric : MonoBehaviour {
    private SpriteRenderer myRenderer;
    public bool alive = true;

    public void ResetBuilding()
    {
        myRenderer.enabled = true;
        alive = true;
    }

    public virtual void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
       // ResetBuilding();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ON TRIGGER ENTER");
        if (GameController.instance.enemyLayerMask == (GameController.instance.enemyLayerMask | (1 << other.gameObject.layer)))
        {
            DestroyBuilding();
        }
    }

    private void DestroyBuilding()
    {
        GameController.instance.LogDeath(this);
        myRenderer.enabled = false;
        alive = false;
    }
}

