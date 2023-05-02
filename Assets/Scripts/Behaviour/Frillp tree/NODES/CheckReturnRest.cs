using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckReturnRest : Node
    {

        Transform _transform;
        float _Distance, refDistance;
        NavMeshAgent _NavMesh;

        public CheckReturnRest(Transform transform, NavMeshAgent nav,float distance)
        {
            _transform = transform;
            _NavMesh = nav;
            refDistance = _transform.GetComponent<EnemyMediumBT>().returnDistance;
        }

        public override NodeState LogicEvaluate()
        {

            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;


            if (_Distance >= refDistance && _NavMesh.destination != _transform.position)
            {
                _transform.GetComponent<EnemyMediumBT>()._InCombat = false;

                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                _transform.GetComponent<EnemyMediumBT>()._InCombat = true;
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}