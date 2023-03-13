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

    void DealDamage()
    {
        SpawnObj(damageBox, damageBoxPrefab, weaponLoc);

        //LaunchDamage(_Coll, 15f);
    }

    void SpawnObj(GameObject item, GameObject prefab, Transform loc)
    {
        Destroy(item);
        item = Instantiate(prefab, loc.position, Quaternion.identity);
        item.transform.parent = loc;
        //item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localRotation = Quaternion.identity;
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
                    c.SendMessageUpwards("HitByAttack", damage);
                    break;
                default:
                    Debug.Log("nopedidntHIT");
                    break;

            }

            

        }

    }

}

