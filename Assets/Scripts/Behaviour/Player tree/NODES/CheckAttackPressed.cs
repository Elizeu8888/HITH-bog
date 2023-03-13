using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckAttackPressed : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public CheckAttackPressed(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (Input.GetMouseButtonDown(0) && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && PlayerBT.attackCooldown <= 0 && PlayerBT._HealthScript.beingHit == false)
            {
                PlayerBT._HealthScript.isBlocking = false;
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}
