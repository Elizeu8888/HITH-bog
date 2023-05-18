using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckBeingDamaged : Node
    {
        private Transform _transform;
        private Animator _Anim;

        public CheckBeingDamaged(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (PlayerBT._HealthScript.beingDamaged && !_Anim.GetCurrentAnimatorStateInfo(1).IsTag("Dash Attack"))
            {
                _Anim.SetBool("BeingHurt", true);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                _Anim.SetBool("BeingHurt", false);
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}
