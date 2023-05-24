using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckPlayerDying : Node
    {
        private Transform _transform;

        public CheckPlayerDying(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {

            if (PlayerBT._HealthScript._dying == true)
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
