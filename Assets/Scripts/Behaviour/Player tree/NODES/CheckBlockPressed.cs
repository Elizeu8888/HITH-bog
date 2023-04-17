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

        public CheckBlockPressed(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (Input.GetMouseButton(1) && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Left") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Right") && _transform.GetComponent<PlayerHealthAndDamaged>().stunTimer <= 0f && _transform.GetComponent<WeaponAttack>().canBlock == true)
            {
                PlayerBT._HealthScript.blockTimer += Time.deltaTime;
                _Anim.SetBool("Blocking", true);
                _Anim.SetLayerWeight(1, 1);
                state = NodeState.SUCCESS;
                return state;
            }
            else if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Left") || _Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Right"))
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

