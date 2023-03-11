using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealthAndManager : MonoBehaviour
{
    public float _CurrentHealth, _MaxHealth;
    public Collider _Coll;

    [SerializeField] float _I_Frames = 0f;

    void Start()
    {
        _CurrentHealth = _MaxHealth;
    }

    void FixedUpdate()
    {
        if(_I_Frames > 0f)
        {
            _I_Frames -= Time.deltaTime;
        }

        if(_I_Frames < 0f)
            _I_Frames = 0f;
    }

    void HitByAttack(float attackDamage)
    {
        if(_I_Frames == 0f)
        {
            _CurrentHealth -= attackDamage;
            _I_Frames = 0.2f;
        }

        return;
    }

    void DealDamage()
    {
        LaunchDamage(_Coll, 15f);
    }


    public void LaunchDamage(Collider col, float damage)
    {

        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBox"));

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
                    Debug.Log("HIT");
                    break;
                default:
                    Debug.Log("nopedidntHIT");
                    break;

            }

            c.SendMessageUpwards("HitByAttack", damage);

        }

    }

}

