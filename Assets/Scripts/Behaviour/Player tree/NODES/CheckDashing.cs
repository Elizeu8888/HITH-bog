using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckDashing : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public CheckDashing(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_transform.GetComponent<PlayerHealthAndDamaged>().dashing == true)
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

