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

        public BlockResult _BlockResult;

        [Header("Health and Stun")]
        public float _CurrentHealth, _MaxHealth;
        public float _CurrentStun, _MaxStun;

        public GameObject _HealthBar;
        Material _HealthBarMat;
        float fillPercent;

        [SerializeField] float _I_Frames = 0f;

        void Start()
        {
            _CurrentHealth = _MaxHealth;
            HealthBarStart();
        }

        void HealthBarStart()
        {
            _HealthBarMat = _HealthBar.GetComponent<SkinnedMeshRenderer>().material;

            _HealthBarMat.SetFloat("_CurrentFillPercent", fillPercent);

        }

        void Update()
        {
            float scriptHealthPercent = (_CurrentHealth / _MaxHealth);
            fillPercent = scriptHealthPercent;
            _HealthBarMat.SetFloat("_CurrentFillPercent", fillPercent);
        }

        void FixedUpdate()
        {
            if (_I_Frames > 0f)
            {
                _I_Frames -= Time.deltaTime;
            }
            if (isBlocking == false)
            {
                blockTimer = 0;
                _BlockResult = BlockResult.Down;
            }


            if (_I_Frames == 0)
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



        public void HitByAttack(float attackDamage, int _LeftRight)
        {


            if (_I_Frames == 0f)
            {
                if (isBlocking == true && blockTimer > 0.2f)//this will BLOCK
                {
                    _BlockResult = BlockResult.Blocked;
                    blockTimer = 1f;
                    _I_Frames = 0.2f;
                    return;
                }
                else if (isBlocking == true && blockTimer < 0.2f && _LeftRight == 1)//this will DEFLECT LEFT
                {
                    _BlockResult = BlockResult.DeflectedLeft;
                    blockTimer = 1f;
                    _I_Frames = 0.3f;
                    return;
                }
                else if (isBlocking == true && blockTimer < 0.2f && _LeftRight == 2)//this will DEFLECT RIGHT
                {
                    _BlockResult = BlockResult.DeflectedRight;
                    blockTimer = 1f;
                    _I_Frames = 0.3f;
                    return;
                }



                _CurrentHealth -= attackDamage;
                beingDamaged = true;
                _I_Frames = 0.25f;
            }

            return;
        }

    }
}

