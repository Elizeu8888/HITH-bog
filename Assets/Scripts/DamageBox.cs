using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public Collider _Coll;
    public float lifeTime = 1f, damage = 15f;
    
    void Start()
    {
        _Coll = gameObject.GetComponent<BoxCollider>();
    }


    void Update()
    {
        Destroy(gameObject, lifeTime);

        Collider[] cols = Physics.OverlapBox(_Coll.bounds.center, _Coll.bounds.extents, _Coll.transform.rotation, LayerMask.GetMask("HitBox"));

        foreach (Collider c in cols)
        {

            if (c.transform.parent == transform)
            {
                continue;
            }

            Debug.Log(c.tag);

            switch (c.tag)
            {
                case "Player":
                    c.SendMessageUpwards("HitByAttack", damage);
                    break;
                default:
                    break;

            }

            

        }
    }
}
