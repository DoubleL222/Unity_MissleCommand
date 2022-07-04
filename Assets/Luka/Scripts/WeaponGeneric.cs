using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGeneric : BuildingGeneric {

    public virtual void ResetWeapon() { }
    public virtual void FireWeapon(Vector2 _dest){}
    public virtual bool IsThereMoreAmmo()
    {
        return true;
    }
    public virtual float DistanceToDest(Vector2 _dest)
    {
        if (IsThereMoreAmmo())
        {
            return Vector3.Distance(transform.position, _dest);
        }
        else
        {
            return float.MaxValue;
        }
        
    }

    //NAJA: CHECK IF THIS TURRET IS PRESSED
    public virtual bool IsPressed()
    {
        return false;
    }

}
