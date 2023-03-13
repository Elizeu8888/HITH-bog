using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskHurt : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public TaskHurt(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            _Anim.SetBool("Blocking", false);
            _Anim.SetBool("Moving", false);
            state = NodeState.RUNNING;
            return state;
        }


    }
}
