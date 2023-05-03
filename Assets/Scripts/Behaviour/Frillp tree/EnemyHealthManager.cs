using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnemyManager
{
    public enum BlockResult
    {
        Blocked,
        DeflectedLeft,
        DeflectedRight,
        Broken,
        Down
    }


    public class EnemyHealthManager : MonoBehaviour
    {
        public bool beingDamaged = false, beingHit = false;
        public float blockTimer = 0f;
        public bool isBlocking = false, blockAttack = false;

        public BlockResult _BlockResult;

        [Header("Health and Stun")]
        public float _CurrentHealth, _MaxHealth;
        public float _CurrentStun, _MaxStun;

        public bool dashing = false;
        public float dashTimer;

        public GameObject _HealthBar;
        Material _HealthBarMat;
        float fillPercent;
        float duration = 0.2f;

        [SerializeField] float _I_Frames = 0f;

        Animator _Anim;

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

            if(_CurrentHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }

        void FixedUpdate()
        {
            if(isBlocking)
            {
                BlockTimer();
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
            if (blockchance >= 0.3f)
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



                _CurrentHealth -= attackDamage;
                beingDamaged = true;
                _I_Frames = 0.35f;
            }

            return;
        }

    }
}




