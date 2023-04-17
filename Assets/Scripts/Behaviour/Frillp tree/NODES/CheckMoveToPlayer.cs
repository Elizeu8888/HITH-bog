using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckMoveToPlayer : Node
    {

        Transform _transform;
        float _Distance;

        Animator _Anim;

        public CheckMoveToPlayer(Transform transform, float distance)
        {
            _transform = transform;
            _Anim = transform.gameObject.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;

            if (_Distance <= 30f && _Distance >= 10f && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
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
