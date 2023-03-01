using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskEnteringCombat : Node
    {

        private Animator _Anim;
        private Transform _transform;

        public TaskEnteringCombat(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>(); 
        }

        public override NodeState LogicEvaluate()
        {

            state = NodeState.RUNNING;
            return state;

        }


    }
}
