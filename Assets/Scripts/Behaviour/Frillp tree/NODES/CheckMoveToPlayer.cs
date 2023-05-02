using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckMoveToPlayer : Node
    {

        Transform _transform;
        float _Distance, refDistance, refDistance2;

        Animator _Anim;

        public CheckMoveToPlayer(Transform transform, float distance)
        {
            _transform = transform;
            _Anim = transform.gameObject.GetComponent<Animator>();
            refDistance = _transform.GetComponent<EnemyMediumBT>().hoverDistance;
            refDistance2 = _transform.GetComponent<EnemyMediumBT>().returnDistance;
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;

            if (_Distance <= refDistance2 && _Distance >= refDistance && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {

                _transform.GetComponent<EnemyMediumBT>()._InCombat = true;

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
