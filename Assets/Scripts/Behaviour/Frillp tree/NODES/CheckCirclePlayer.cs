using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckCirclePlayer : Node
    {

        Transform _transform;

        public CheckCirclePlayer(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {

            if (EnemyMediumBT._PlayerDistance <= 9f)
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
