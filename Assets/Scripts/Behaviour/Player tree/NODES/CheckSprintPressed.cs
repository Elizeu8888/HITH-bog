using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckSprintPressed : Node
    {

        Animator _Anim;
        Transform _transform;

        public CheckSprintPressed(Transform transform)
        {
            _transform = transform;
            _Anim = _transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (Input.GetAxisRaw("Vertical") >= 0.1f && Input.GetKey(KeyCode.LeftShift) && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == true && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw"))
            {
                //_Anim.SetLayerWeight(1, 0);
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
