using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_BombController : MonoBehaviour {
    public float speed = 5f;

    private bool isLaunched = false;
    private Vector2 destination;

    public void Launch(Vector2 _dest)
    {
        destination = _dest;
        isLaunched = true;
    }

    public void DestroyBomb()
    {
        BombDroppingScript.instance.BombDestroyed();
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isLaunched)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
            if ((Vector2)transform.position == destination)
            {
                DestroyBomb();
            }
        }
	}
}
