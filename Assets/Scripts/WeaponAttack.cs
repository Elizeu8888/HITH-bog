using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{

    public static event Action<int> OnAttack;
    public static event Action<int> OnComboReset;

    public Collider _Coll;
    public string tagName;

    public float damage;

    public string[] _AttackLightAnimNames;
    public string[] _DashAttacks;
    public ParticleSystem _LeftAttackParticle, _RightAttackParticle;

    public bool comboPossible, attackPossible = true;
    public int comboStep = 0;

    private Animator _Anim;

    GameObject damageBox;
    public GameObject damageBoxPrefab;
    public Transform weaponLoc;
    float missedCombo = 0f;
    public bool canBlock = true;

    public int dashDir;

    int attacker;

    void Start()
    {
        _Anim = gameObject.GetComponent<Animator>();

        if(gameObject.GetComponent<EnemyMediumBT>() != null)
        {
            attacker = 1;
        }
        if (gameObject.GetComponent<PlayerBT>() != null)
        {
            attacker = 0;
        }



    }

    void Update()
    {
        if(missedCombo >= 0f)
            missedCombo -= Time.deltaTime;
    }

    public void DashAttack()
    {
        if(comboPossible)
        {
            canBlock = false;
            OnAttack?.Invoke(attacker);
            _Anim.Play(_DashAttacks[dashDir], 0);
            _Anim.Play(_DashAttacks[dashDir], 1);
            comboStep = 0;
            comboPossible = false;
        }

    }
    public void Attack()
    {
        canBlock = false;
        if (comboStep == 0)
        {
            OnAttack?.Invoke(attacker);
            _Anim.Play(_AttackLightAnimNames[0], 0);
            _Anim.Play(_AttackLightAnimNames[0], 1);
            comboStep = 1;
            return;
        }
        if (comboStep != 0)
        {
            if(missedCombo >= 0f && comboPossible == true)
            {
                comboStep += 1;
                
                comboPossible = false;

                Combo();

                missedCombo = 0f;
            }

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
            OnAttack?.Invoke(attacker);
            _Anim.Play(_AttackLightAnimNames[1], 0);
            _Anim.Play(_AttackLightAnimNames[1], 1);
            return;
        }
        if (comboStep == 3)
        {
            OnAttack?.Invoke(attacker);
            _Anim.Play(_AttackLightAnimNames[2], 0);
            _Anim.Play(_AttackLightAnimNames[2], 1);
            return;
        }
        if (comboStep == 4)
        {
            OnAttack?.Invoke(attacker);
            _Anim.Play(_AttackLightAnimNames[3], 0);
            _Anim.Play(_AttackLightAnimNames[3], 1);
            return;
        }
        if (missedCombo <= 0f)
        {
            missedCombo = 0.2f;
        }

        canBlock = true;
    }

    public void EnemyCombo()
    {
        float chance = UnityEngine.Random.Range(0f, 1f);

        if(chance <= 0.6f)
        {
            comboStep += 1;
            comboPossible = false;
        }
    }

    public void ComboReset()
    {
        StartCoroutine(ComboResetCor());
    }

    public IEnumerator ComboResetCor()
    {
        yield return new WaitForSeconds(0.2f);
        OnComboReset?.Invoke(attacker);
        comboPossible = false;
        comboStep = 0;
        canBlock = true;
    }


    void DealDamageLeft()// this is called in an animation
    {
        SpawnDamageBox(damage, damageBox, damageBoxPrefab, weaponLoc, 1, tagName, gameObject);

    }
    void DealDamageRight()// this is called in an animation
    {
        SpawnDamageBox(damage, damageBox, damageBoxPrefab, weaponLoc, 2, tagName, gameObject);
    }

    void SpawnDamageBox(float dam, GameObject item, GameObject prefab, Transform loc, int leftright, string tagname, GameObject initiator)// this is mainly used to spawn hitbox
    {
        Destroy(item);
        item = Instantiate(prefab, loc.position, Quaternion.identity);
        item.transform.parent = loc;
        item.GetComponent<DamageBox>().leftright = leftright;
        item.GetComponent<DamageBox>().damage = dam;
        item.GetComponent<DamageBox>().tagName = tagname;
        item.GetComponent<DamageBox>().damageInitiator = initiator;
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localRotation = Quaternion.identity;
    }

    public void TurnOnMeleeEffect_Right()
    {
        _RightAttackParticle.Play();
        _RightAttackParticle.Clear();

    }
    public void TurnOnMeleeEffect_Left()
    {
        _LeftAttackParticle.Play();
        _RightAttackParticle.Clear();
    }



}
