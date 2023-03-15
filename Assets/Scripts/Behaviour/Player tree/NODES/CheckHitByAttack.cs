using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckHitByAttack : Node
    {
        private Transform _transform;

        public CheckHitByAttack(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {

            if (PlayerBT._HealthScript.beingHit == true)
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
