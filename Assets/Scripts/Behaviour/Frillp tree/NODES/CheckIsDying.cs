using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckIsDying : Node
    {
        //ENEMY NODE
        Transform _transform;
        float _Distance;
        Animator _Anim;
        NavMeshAgent _NavMesh;

        public CheckIsDying(Transform transform)
        {
            _transform = transform;
            _Anim = _transform.GetComponent<Animator>();
            _NavMesh = _transform.GetComponent<NavMeshAgent>();
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;

            if (_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Death") == true)
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