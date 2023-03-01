using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickontoObject : MonoBehaviour
{

    public GameObject objectToStickTo;
    public bool setObjectPositionToZero = false, x90degree = true;

    void Start()
    {
        if(setObjectPositionToZero == false)
        {
            if (objectToStickTo != null)
            {
                transform.SetParent(objectToStickTo.transform, true);
            }
        }
        else
        {
            if (objectToStickTo != null)
            {
                if(x90degree == true)
                {
                    transform.SetParent(objectToStickTo.transform, true);
                    transform.localPosition = new Vector3(0f, 0f, 0f);
                    transform.localRotation = Quaternion.Euler(-90, 0, 0);
                }
                else
                {
                    transform.SetParent(objectToStickTo.transform, true);
                    transform.localPosition = new Vector3(0f, 0f, 0f);
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

            }
        }


    }
}
