using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

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

            if (Input.GetMouseButton(1) && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && PlayerBT.attackCooldown <= 0)
            {
                PlayerBT._HealthScript.blockTimer += Time.deltaTime;
                _Anim.SetBool("Blocking", true);                
                state = NodeState.SUCCESS;
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

