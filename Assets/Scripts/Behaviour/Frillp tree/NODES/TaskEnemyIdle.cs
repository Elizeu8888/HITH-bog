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

            _NavMesh.enabled = false;
            
            Debug.Log("idle");

            state = NodeState.RUNNING;
            return state;
        }

    }
}

