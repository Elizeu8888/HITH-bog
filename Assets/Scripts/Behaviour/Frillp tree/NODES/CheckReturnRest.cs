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
        float _Distance;
        NavMeshAgent _NavMesh;

        public CheckReturnRest(Transform transform, NavMeshAgent nav,float distance)
        {
            _transform = transform;
            _NavMesh = nav;
        }

        public override NodeState LogicEvaluate()
        {

            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;


            if (_Distance >= 30f && _NavMesh.destination != _transform.position)
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