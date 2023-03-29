using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerManager;

public class DamageBox : MonoBehaviour
{
    public Collider _Coll;
    public float lifeTime = 1f, damage = 15f;
    public int leftright = 1;
    
    void Start()
    {
        _Coll = gameObject.GetComponent<BoxCollider>();
    }

    void Update()// this exists only to deal damage then disappear 
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
                    c.transform.parent.GetComponent<PlayerHealthAndDamaged>().HitByAttack(damage, leftright);
                    break;
                default:
                    break;

            }        


        }
    }
}
