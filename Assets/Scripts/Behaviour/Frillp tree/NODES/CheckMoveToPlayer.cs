using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckMoveToPlayer : Node
    {

        Transform _transform;

        public CheckMoveToPlayer(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {

            EnemyMediumBT._PlayerDistance = Vector3.Distance(EnemyMediumBT._Player.transform.position, _transform.position);


            if (EnemyMediumBT._PlayerDistance <= 30f && EnemyMediumBT._PlayerDistance >= 8f && EnemyMediumBT._Dir_Change_Timer <= 0f)
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
