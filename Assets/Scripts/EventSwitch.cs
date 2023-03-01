using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventSwitch : MonoBehaviour
{
    public UnityEvent weaponHolster, weaponRight, weaponLeft;

    private Animator _Anim;

    void Start()
    {
        _Anim = gameObject.GetComponent<Animator>();
    }

    public void InCombat()
    {
        _Anim.SetBool("InCombat", true);
    }

    public void WeaponHolster()
    {
        weaponHolster.Invoke();
    }

    public void WeaponRightHand()
    {
        weaponRight.Invoke();
    }

    public void WeaponLeftHand()
    {
        weaponLeft.Invoke();
    }
}
