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

        PlayerBT _plyBT;

        public CheckSprintPressed(Transform transform)
        {
            _transform = transform;
            _Anim = _transform.GetComponent<Animator>();
            _plyBT = _transform.GetComponent<PlayerBT>();
        }

        public override NodeState LogicEvaluate()
        {

            if (InputManager.movementInput.y >= 0.1f && _plyBT.sprintPressed && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == true && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw"))
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
