using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckBlockPressed : Node
    {
        private Animator _Anim;
        private Transform _transform;
        private WeaponAttack _weaponAttack;

        public CheckBlockPressed(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _weaponAttack = _transform.GetComponent<WeaponAttack>();
        }

        public override NodeState LogicEvaluate()
        {

            if (Input.GetMouseButton(1) && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Left") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Right") && _transform.GetComponent<PlayerHealthAndDamaged>().stunTimer <= 0f)
            {

                PlayerBT._HealthScript.blockTimer += Time.deltaTime;

                if(!_Anim.GetCurrentAnimatorStateInfo(1).IsTag("Blocking") && !_Anim.GetCurrentAnimatorStateInfo(1).IsTag("BlockReaction"))
                {
                    _weaponAttack.ComboReset();
                    _Anim.Play("Block Start", 1);
                }
                if (_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                {
                    _Anim.Play("Idle", 0);
                }


                _Anim.SetBool("Blocking", true);
                _Anim.SetLayerWeight(1, 1);
                state = NodeState.SUCCESS;
                return state;
            }
            else if(_Anim.GetCurrentAnimatorStateInfo(1).IsTag("Block Reaction"))
            {
                PlayerBT._HealthScript.blockTimer += Time.deltaTime;
                PlayerBT._HealthScript.isBlocking = true;
                _Anim.SetBool("Blocking", true);
                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                PlayerBT._HealthScript.isBlocking = false;
                _Anim.SetBool("Blocking", false);
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}

