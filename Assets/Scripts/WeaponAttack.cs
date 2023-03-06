using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{

    public static bool comboPossible, attackPossible = true;
    public int comboStep = 0;

    private Animator _Anim;

    void Start()
    {
        _Anim = gameObject.GetComponent<Animator>();
    }

    public void Attack()
    {
        if (comboStep == 0)
        {
            _Anim.Play("sword Light 1", 0);
            _Anim.Play("sword Light 1", 1);
            comboStep = 1;
            return;
        }
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboStep += 1;
                comboPossible = false;

            }
        }
    }
    public void ComboPossible()
    {
        comboPossible = true;
    }
    public void Combo()
    {
        if (comboStep == 2)
        {
            _Anim.Play("Sword Light 2",0);
            _Anim.Play("Sword Light 2",1);
        }
        if (comboStep == 3)
            _Anim.Play("attack3");

    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
    }
    public void LaunchDamage(Collider col, float damage)
    {

        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBoxes"));
        foreach (Collider c in cols)
        {
            print("damageLaunched");

            if (c.transform.parent == transform)
            {
                continue;
            }

            Debug.Log(c.tag);

            switch (c.tag)
            {
                case "enemy":
                    _Anim.SetTrigger("hit");
                    break;
                default:
                    Debug.Log("nopedidntwork");
                    break;

            }

            c.SendMessageUpwards("TakeDamage", damage);

        }

    }
}
