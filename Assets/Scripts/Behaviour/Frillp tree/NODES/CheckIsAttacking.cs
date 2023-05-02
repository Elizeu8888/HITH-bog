using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckIsAttacking : Node
    {
        //ENEMY NODE
        Transform _transform;
        float _Distance;
        Animator _Anim;
        NavMeshAgent _NavMesh;

        public CheckIsAttacking(Transform transform)
        {
            _transform = transform;
            _Anim = _transform.GetComponent<Animator>();
            _NavMesh = _transform.GetComponent<NavMeshAgent>();
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;

            if (_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == true)
            {
                _transform.GetComponent<NavMeshAgent>().stoppingDistance = 0.5f;
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                _transform.GetComponent<NavMeshAgent>().stoppingDistance = 0.2f;
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}