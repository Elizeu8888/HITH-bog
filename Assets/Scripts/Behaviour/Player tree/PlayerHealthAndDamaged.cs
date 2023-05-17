using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerManager
{
    public enum BlockResult
    {
        Blocked,
        DeflectedLeft,
        DeflectedRight,
        Broken,
        Down
    }


    public class PlayerHealthAndDamaged : MonoBehaviour
    {
        public bool beingDamaged = false, beingHit = false;
        public float blockTimer = 0f;
        public bool isBlocking = false;
        bool _CanBlock;

        public BlockResult _BlockResult;

        [Header("Health and Stun")]
        public float _CurrentHealth, _MaxHealth;
        public GameObject _HealthBar;
        Material _HealthBarMat;
        float healthFillPercent;

        public bool dashing, rolling;
        public bool rollPossible;
        public float dashTimer;

        public float _CurrentPosture, _MaxPosture;
        public GameObject _PostureBar;
        Material _PostureBarMat;
        float PostureFillPercent;
        public bool postureBroken = false;
        public float stunTimer = 0f;

        public float _CurrentKenetic, _MaxKenetic;

        public bool isDashing;
        [SerializeField] float _I_Frames = 0f;

        Animator playerAnim;

        GameObject hurtInstant;
        public GameObject hurtVFX;
        public Transform bloodSpawn;
        public void SpawnVFX(GameObject item, GameObject prefab, Transform loc)// This is used in other scripts
        {
            Destroy(item);
            item = Instantiate(prefab, loc.position, Quaternion.identity);
            item.transform.parent = loc;
            item.transform.localPosition = new Vector3(0, 0, 0);
            item.transform.localRotation = Quaternion.identity;
            Destroy(item, 0.5f);
        }



        void Start()
        {
            _CurrentHealth = _MaxHealth;
            HealthBarStart();
            playerAnim = transform.GetComponent<Animator>();
            PostureBarStart();
        }

        void HealthBarStart()
        {
            _HealthBarMat = _HealthBar.GetComponent<MeshRenderer>().material;

            _HealthBarMat.SetFloat("_CurrentFillPercent", healthFillPercent);

        }
        void PostureBarStart()
        {
            _PostureBarMat = _PostureBar.GetComponent<MeshRenderer>().material;

            _PostureBarMat.SetFloat("_CurrentFillPercent", PostureFillPercent);

        }

        void Update()
        {
            float scriptHealthPercent = (_CurrentHealth / _MaxHealth);
            healthFillPercent = scriptHealthPercent;
            _HealthBarMat.SetFloat("_CurrentFillPercent", healthFillPercent);

            float scriptPosturePercent = (_CurrentPosture / _MaxPosture);
            PostureFillPercent = scriptPosturePercent;
            _PostureBarMat.SetFloat("_CurrentFillPercent", PostureFillPercent);
        }

        void FixedUpdate()
        {
            if(dashing)
            {
                dashTimer -= Time.deltaTime;
            }


            if (_I_Frames >= 0f)
            {
                _I_Frames -= Time.deltaTime;
            }

            if (stunTimer >= 0f)
            {
                stunTimer -= Time.deltaTime;
            }
            //--------------------------------
            if (isBlocking == false)
            {
                blockTimer = 0;
                _BlockResult = BlockResult.Down;
            }
            //--------------------------------
            if(_CurrentPosture > _MaxPosture * 0.8f)
            {
                _CanBlock = false;
            }
            else
            {
                _CanBlock = true;
            }

            if(_CurrentPosture < _MaxPosture && stunTimer <= 0f)
            {
                postureBroken = false;
            }

            //----------------------------------
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

        public void EndDashEvent()
        {
            dashTimer = 0f;
            dashing = false;
            rolling = false;
            transform.GetComponent<WeaponAttack>().canBlock = true;
            playerAnim.SetBool("Dashing", false);
        }


        void RollPossible()
        {
            StartCoroutine(RollTimerCorou());
        }

        public IEnumerator RollTimerCorou()
        {
            rollPossible = true;
            yield return new WaitForSeconds(0.5f);
            rollPossible = false;
            yield return null;
        }


        public void HitByAttack(float attackDamage, int _LeftRight, GameObject attacker)
        {


            if (_I_Frames == 0f && rolling == false)
            {

                if(postureBroken == false)
                {

                    if (isBlocking == true && blockTimer > 0.2f && _CanBlock == true)//this will BLOCK
                    {
                        _BlockResult = BlockResult.Blocked;
                        _CurrentPosture += attackDamage * 0.5f;
                        blockTimer = 1f;
                        _I_Frames = 0.2f;
                        return;
                    }
                    else if (isBlocking == true && blockTimer < 0.2f && _LeftRight == 1)//this will DEFLECT LEFT
                    {
                        _BlockResult = BlockResult.DeflectedLeft;
                        _CurrentPosture += attackDamage * 0.2f;
                        blockTimer = 1f;
                        _I_Frames = 0.3f;
                        return;
                    }
                    else if (isBlocking == true && blockTimer < 0.2f  && _LeftRight == 2)//this will DEFLECT RIGHT
                    {
                        _BlockResult = BlockResult.DeflectedRight;
                        _CurrentPosture += attackDamage * 0.2f;
                        blockTimer = 1f;
                        _I_Frames = 0.3f;
                        return;
                    }
                    else if (isBlocking == true && blockTimer > 0.2f && _CanBlock == false)
                    {
                        _BlockResult = BlockResult.Broken;
                        _CurrentPosture = _MaxPosture;
                        _I_Frames = 0f;
                        stunTimer = 1.9f;
                        postureBroken = true;
                        isBlocking = false;
                        return;
                    }
                }


                SpawnVFX(hurtInstant, hurtVFX, bloodSpawn);
                _CurrentHealth -= attackDamage;
                beingDamaged = true;
                _I_Frames = 0.25f;
            }

            return;
        }

    }
}

