using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckGuardBroken : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public CheckGuardBroken(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_transform.GetComponent<PlayerHealthAndDamaged>().stunTimer > 0f)
            {
                Debug.Log("stun");
                _Anim.SetBool("Stunned", true);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                _Anim.SetBool("Stunned", false);
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}

