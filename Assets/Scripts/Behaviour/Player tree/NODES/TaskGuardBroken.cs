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

            if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("Guard Break"))
            {
                _Anim.SetBool("Blocking", false);
                _Anim.Play("Guard Break", 0);
                _Anim.Play("Guard Break", 1);
            }


            state = NodeState.RUNNING;
            return state;
            

        }


    }
}

