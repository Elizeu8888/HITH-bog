using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckReturnRest : Node
    {

        Transform _transform;
        float _Distance;

        public CheckReturnRest(Transform transform,float distance)
        {
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {

            _Distance = Vector3.Distance(EnemyMediumBT._Player.transform.position, _transform.position);


            if (_Distance >= 30f)
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