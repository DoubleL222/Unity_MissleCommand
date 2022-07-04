using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_AntiMissle : WeaponGeneric {
    private GameObject missilePrefab;
    private int MaxAmmo = 10;
    private int CurrentAmmo = 0;
    private bool isTurretPressed = false; //NAJA: IS THIS TURRET PRESSED
    private Camera mainCamera; //NAJA
    private Animator myAnimator;



    // Use this for initialization
    public override void Start () {
        base.Start();
        missilePrefab = Resources.Load("A_Missile") as GameObject;
        mainCamera = FindObjectOfType<Camera>(); //NAJA
        myAnimator = GetComponentInChildren<Animator>();
        ResetWeapon();
    }

    public override void ResetWeapon()
    {
        CurrentAmmo = MaxAmmo;
        Released();
    }

    public override bool IsThereMoreAmmo()
    {
        if (CurrentAmmo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void FireWeapon(Vector2 _dest)
    {
        float angle = Vector2.Angle(_dest - (Vector2)transform.position, Vector2.up);
        if (_dest.x - transform.position.x > 0)
        {
            angle = -angle;
        }
        if (CurrentAmmo > 0)
        {
            CurrentAmmo--;
        }
        else
        {
            OutOfAmmo();
            Debug.Log("ERROR - CURRENT AMMO SHOULD NOT BE NEGATIVE");
            return;
        }
        GameObject ammoInstance = Instantiate(missilePrefab, transform.position, Quaternion.Euler(0,0,angle));
       
        ammoInstance.GetComponent<AmmunitionGeneric>().Launch(_dest);

        isTurretPressed = false; //NAJA
        if (CurrentAmmo <= 0)
        {
            OutOfAmmo();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}


    public override bool IsPressed() //NAJA: Keeps track of if this turret is pressed
    {
        return isTurretPressed;
    }


    void OnMouseDown() //When pressed
    {
        if (CurrentAmmo > 0)
        {
            // Debug.Log("A turret has been pressed");
            isTurretPressed = true; //NAJA
            ClickedOn();
        }
    }

    void OnMouseUp() //When released
    {
        if (CurrentAmmo > 0)
        {
            // Debug.Log("Drag ended!");
            Vector3 clickWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // GameController.instance.ClickDetected(clickWorldPos);
            GameController.instance.DragDetected(clickWorldPos, this);
            Released();
        }
    }

    void ClickedOn()
    {
        myAnimator.SetTrigger("Clicked");
    }

    void Released()
    {
        myAnimator.SetTrigger("Released");
    }

    void OutOfAmmo()
    {
        myAnimator.SetTrigger("OutOfAmmo");
    }

}
