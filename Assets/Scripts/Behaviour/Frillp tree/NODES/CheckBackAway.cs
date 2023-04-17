using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckBackAway : Node
    {

        Transform _transform;
        float _Distance;

        public CheckBackAway(Transform transform, float distance)
        {
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;

            if (_Distance <= 6f)
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
