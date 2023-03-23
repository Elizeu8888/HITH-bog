using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskEnemyIdle : Node
    {
        private Animator _Anim;
        Transform _transform;

        public TaskEnemyIdle(Transform transform)
        {
            //_Anim = transform.GetComponent<Animator>();
            _transform = transform;
        }


        public override NodeState LogicEvaluate()
        {

            EnemyMediumBT._NavMesh.enabled = false;
            
            Debug.Log("idle");

            state = NodeState.RUNNING;
            return state;
        }

    }
}

