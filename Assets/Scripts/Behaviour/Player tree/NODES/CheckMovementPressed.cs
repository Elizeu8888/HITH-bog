using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckMovementPressed : Node
    {
        private Animator _Anim;

        Transform _transform;

        public CheckMovementPressed(Transform transform)
        {
            _Anim = transform.GetComponent<Animator>();
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
            {
                state = NodeState.FAILURE;
                return state;
            }


            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
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

