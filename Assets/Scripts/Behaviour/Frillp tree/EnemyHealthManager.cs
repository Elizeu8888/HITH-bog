using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EnemyManager
{
    public enum BlockResult
    {
        Blocked,
        DeflectedLeft,
        DeflectedRight,
        HurtLeft,
        HurtRight,
        Broken,
        Down
    }


    public class EnemyHealthManager : MonoBehaviour
    {
        public static event System.Action OnEnemyDeath;
        public bool beingDamaged = false, beingHit = false, staggered = false;
        public float blockTimer = 0f;
        public bool isBlocking = false, blockAttack = false;
        public float blockChance = 0.35f;
        public BlockResult _BlockResult;

        [Header("Health and Stun")]
        public float _CurrentHealth, _MaxHealth;
        public TMP_Text healthText;
        public float _CurrentStun, _MaxStun, stunChance = 0.6f;

        public bool dashing = false;
        public float dashTimer;

        public GameObject _HealthBar;
        Material _HealthBarMat;
        float fillPercent;
        float duration = 0.2f;

        GameObject hurtInstant;
        public GameObject hurtVFX;
        public Transform bloodSpawn;

        [SerializeField] float _I_Frames = 0f;

        Animator _Anim;
        bool dyingTrigger = false;

        public string[] deathAnim;
        public int _EnemyType;

        private void OnEnable()
        {
            WeaponAttack.OnAttack += BlockChance;
        }

        private void OnDisable()
        {
            WeaponAttack.OnAttack -= BlockChance;
        }

        void Start()
        {
            _CurrentHealth = _MaxHealth;
            HealthBarStart();



        }


        void StartFindType()
        {
            if(_EnemyType == 0 )
            {

            }
        }
        void HealthBarStart()
        {
            _HealthBarMat = _HealthBar.GetComponent<MeshRenderer>().material;

            _HealthBarMat.SetFloat("_CurrentFillPercent", fillPercent);

            _Anim = gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            float scriptHealthPercent = (_CurrentHealth / _MaxHealth);
            fillPercent = scriptHealthPercent;
            _HealthBarMat.SetFloat("_CurrentFillPercent", fillPercent);


            if (_CurrentHealth <= 0f)
            {
                Dying();
            }
        }

        void Dying()
        {
            if (dyingTrigger == false)
            {
                _Anim.SetLayerWeight(2, 1);
                _Anim.Play("Idle", 0);
                _Anim.Play(deathAnim[Random.Range(0, deathAnim.Length)], 2);
                dyingTrigger = true;
            }



        }

        public void Death()
        {
            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }

        void FixedUpdate()
        {
            if(isBlocking)
            {
                BlockTimer();
            }

            if(dashing)
            {
                dashTimer += Time.deltaTime;
            }


            if(dashTimer >= 0.8f)
            {
                StartCoroutine(DashMakeSureTurnOff());
            }

            if (_I_Frames > 0f)
            {
                _I_Frames -= Time.deltaTime;
            }
            if (isBlocking == false)
            {
                blockTimer = 0;
                _BlockResult = BlockResult.Down;
            }


            if (_I_Frames <= 0)
            {
                beingDamaged = false;
                staggered = false;
                beingHit = false;
            }
            else
                beingHit = true;

            if (_I_Frames < 0f)
                _I_Frames = 0f;
        }

        public void StartBlocking()// this is called in the block animation
        {
            isBlocking = true;
        }
        void BlockTimer()
        {

            duration -= Time.deltaTime;

            if( duration <= 0)
            {
                
                duration = Random.Range(0.4f, 1f);
                isBlocking = false;
                blockAttack = false;
            }


        }

        public void SpawnVFX(GameObject item, GameObject prefab, Transform loc)// This is used in other scripts
        {
            Destroy(item);
            item = Instantiate(prefab, loc.position, Quaternion.identity);
            item.transform.parent = loc;
            item.transform.localPosition = new Vector3(0, 0, 0);
            item.transform.localRotation = Quaternion.identity;
            Destroy(item, 0.5f);
        }

        public IEnumerator HealthNumber()
        {
            healthText.text = _CurrentHealth.ToString();
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            healthText.text = "";
        }

        
        public IEnumerator DashMakeSureTurnOff()
        {
            yield return new WaitForSeconds(0.6f);
            dashTimer = 0f;
            dashing = false;
            transform.GetComponent<WeaponAttack>().canBlock = true;
        }

        public void EnemyEndDashEvent()
        {
            dashing = false;
            transform.GetComponent<WeaponAttack>().canBlock = true;
        }

        void BlockChance(int enemyID)
        {

            if (enemyID != 0 || BlockChanceCheck() == false)
                return;

            float blockchance = Random.Range(0f, 1f);
            if (blockchance >= blockChance)
            {
                isBlocking = true;
            }
        }

        bool BlockChanceCheck()
        {
            if(_Anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
            {
                return false;
            }
            if (_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Block Reaction"))
            {
                return false;
            }
            if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            {
                return false;

            }
            return true;
        }

        public void HitByAttack(float attackDamage, int _LeftRight, GameObject attacker)
        {


            if (_I_Frames <= 0f && dashing == false)
            {


                if (blockAttack == true && blockTimer > 0.1f)//this will BLOCK
                {
                    _BlockResult = BlockResult.Blocked;
                    blockTimer = 1f;
                    _I_Frames = 0.2f;
                    return;
                }
                else if (blockAttack == true && blockTimer < 0.1f && _LeftRight == 1)//this will DEFLECT LEFT
                {
                    _BlockResult = BlockResult.DeflectedLeft;
                    blockTimer = 1f;
                    _I_Frames = 0.2f;
                    return;
                }
                else if (blockAttack == true && blockTimer < 0.1f && _LeftRight == 2)//this will DEFLECT RIGHT
                {
                    _BlockResult = BlockResult.DeflectedRight;
                    blockTimer = 1f;
                    _I_Frames = 0.2f;
                    return;
                }

                if(_LeftRight == 1)
                {
                    _BlockResult = BlockResult.HurtLeft;
                }
                else
                {
                    _BlockResult = BlockResult.HurtRight;
                }





                SpawnVFX(hurtInstant, hurtVFX, bloodSpawn);

                
                if(Random.Range(0f,1f) >= stunChance)
                {
                    staggered = true;
                }

                _CurrentHealth -= attackDamage;
                StartCoroutine(HealthNumber());
                beingDamaged = true;
                _I_Frames = 0.35f;
            }

            return;
        }

    }
}




