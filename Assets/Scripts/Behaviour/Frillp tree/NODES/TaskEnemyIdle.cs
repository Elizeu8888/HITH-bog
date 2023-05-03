using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskEnemyIdle : Node
    {
        private Animator _Anim;
        Transform _transform;
        NavMeshAgent _NavMesh;

        public TaskEnemyIdle(Transform transform, NavMeshAgent nav)
        {
            //_Anim = transform.GetComponent<Animator>();
            _transform = transform;
            _NavMesh = nav;
        }


        public override NodeState LogicEvaluate()
        {

            _NavMesh.enabled = true;
            _NavMesh.destination = _transform.position;
            _NavMesh.speed = 0f;
            _NavMesh.velocity = Vector3.zero;
            

            state = NodeState.RUNNING;
            return state;
        }

    }
}

