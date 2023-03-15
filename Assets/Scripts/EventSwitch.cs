using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventSwitch : MonoBehaviour
{
    public UnityEvent weaponHolster, weaponRight, weaponLeft;

    private Animator _Anim;

    private PlayerBT _plyBT;

    private GameObject _vfx;

    void Start()
    {
        _Anim = gameObject.GetComponent<Animator>();
        _plyBT = GetComponent<PlayerBT>();
    }

    public void InCombat()
    {
        _Anim.SetBool("InCombat", true);
    }

    public void SparkLeft()
    {
        _plyBT.SpawnVFX(_vfx, _plyBT._VFX[0], transform);
    }
    public void SparkRight()
    {
        _plyBT.SpawnVFX(_vfx, _plyBT._VFX[1], transform);
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
