using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : MonoBehaviour
{

    public Transform holster, lefthand, righthand;


    void Start()
    {
        if (holster == null || righthand == null || lefthand == null)
        {
            return;
        }
        else
        {
            transform.SetParent(holster);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
    }



    public void WeaponRightHand()
    {
        if (holster == null || righthand == null || lefthand == null)
        {
            return;
        }
        else
        {
            transform.SetParent(righthand);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(180, 0, -180);

        }

    }

    public void WeaponLeftHand()
    {
        if (holster == null || righthand == null || lefthand == null)
        {
            return;
        }
        else
        {
            transform.SetParent(lefthand);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(180, 0, -180);

        }

    }

    public void WeaponHolster()
    {
        if (holster == null || righthand == null || lefthand == null)
        {
            return;
        }
        else
        {
            transform.SetParent(holster);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }

    }


}
