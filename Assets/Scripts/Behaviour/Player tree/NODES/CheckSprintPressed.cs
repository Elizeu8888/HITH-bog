using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckSprintPressed : Node
    {
        public CheckSprintPressed(Transform transform)
        {

        }

        public override NodeState LogicEvaluate()
        {

            if (Input.GetAxisRaw("Vertical") >= 0.1f && Input.GetKey(KeyCode.LeftShift))
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
