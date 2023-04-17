using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerManager;
using EnemyManager;

public class DamageBox : MonoBehaviour
{
    public Collider _Coll;
    public float lifeTime = 1f, damage = 15f;
    public int leftright = 1;
    public string tagName = "1";
    public GameObject damageInitiator;
    
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

            if(c.transform.parent == null)
            {
                break;
            }

            if(c.tag == tagName)
            {
                if (c.transform.parent.GetComponent<PlayerHealthAndDamaged>() != null)
                {

                    c.transform.parent.GetComponent<PlayerHealthAndDamaged>().HitByAttack(damage, leftright,damageInitiator);

                }
                else if(c.transform.parent.GetComponent<EnemyHealthManager>() != null)
                {
                    c.transform.parent.GetComponent<EnemyHealthManager>().HitByAttack(damage, leftright,damageInitiator);

                }
            }


        }
    }
}
