using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckBeingDamaged : Node
    {
        private Transform _transform;

        public CheckBeingDamaged(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {

            if (PlayerBT._HealthScript.beingDamaged == true)
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
