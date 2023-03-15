using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealthAndManager : MonoBehaviour
{
    public float _CurrentHealth, _MaxHealth;
    public Collider _Coll;

    [SerializeField] float _I_Frames = 0f;

    GameObject damageBox;
    public GameObject damageBoxPrefab;
    public Transform weaponLoc;

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

    void DealDamageLeft()// this is called in an animation
    {
        SpawnDamageBox(damageBox, damageBoxPrefab, weaponLoc, 1);

    }
    void DealDamageRight()// this is called in an animation
    {
        SpawnDamageBox(damageBox, damageBoxPrefab, weaponLoc, 2);
    }

    void SpawnDamageBox(GameObject item, GameObject prefab, Transform loc, int leftright)// this is mainly used to spawn hitbox
    {
        Destroy(item);
        item = Instantiate(prefab, loc.position, Quaternion.identity);
        item.transform.parent = loc;
        item.GetComponent<DamageBox>().leftright = leftright;
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localRotation = Quaternion.identity;
    }


}

