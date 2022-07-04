using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_MissileController : AmmunitionGeneric {


    public float speed = 1.0f;
    
    public float explodeSpeed = 1.0f;
    public GameObject explosionSprite;

    private float explosionScale = 10f;
    private bool hasLaunched, isMoving, isExploding, isImploding = false;
    private Vector2 destination;
    private Vector3 initialScale;

    public override void Launch(Vector2 _dest)
    {
        destination = _dest;
        hasLaunched = true;
        isMoving = true;
    }
    // Use this for initialization
    void Start () {
        initialScale = explosionSprite.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasLaunched)
        {
            if (isMoving)
            {
               

                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, destination, step);
                if ((Vector2)transform.position == destination)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    explosionSprite.SetActive(true);
                    isMoving = false;
                    isExploding = true;
                }

               
            }
            else if (isExploding)
            {
                float step = explodeSpeed * Time.deltaTime;
                explosionSprite.transform.localScale = Vector3.MoveTowards(explosionSprite.transform.localScale, initialScale * explosionScale, step);
                if (explosionSprite.transform.localScale == initialScale * explosionScale)
                {
                    isExploding = false;
                    isImploding = true;
                }

                
            }
            else if (isImploding)
            {

                //NAJA: ADDED SOUND
                //START IMPLODING SOUND
                float step = explodeSpeed * Time.deltaTime;
                explosionSprite.transform.localScale = Vector3.MoveTowards(explosionSprite.transform.localScale, Vector3.one * 0.1f, step);
                if (explosionSprite.transform.localScale == Vector3.one * 0.1f)
                {
                    Destroy(this.gameObject);
                }

                //NAJA: ADDED SOUND
                //STOP IMPLODING SOUND
            }
        }   
    }
}
