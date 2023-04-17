using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;


namespace BehaviorTree
{
    public class TaskGuardBroken : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public TaskGuardBroken(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            _transform.GetComponent<PlayerHealthAndDamaged>()._CurrentPosture = 0f;

            state = NodeState.RUNNING;
            return state;
            

        }


    }
}

