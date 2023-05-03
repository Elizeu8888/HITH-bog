using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using EnemyManager;

namespace BehaviorTree
{
    public class CheckEnemyBeingHit : Node
    {
        private Transform _transform;
        private Animator _Anim;

        public CheckEnemyBeingHit(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_transform.gameObject.GetComponent<EnemyHealthManager>().beingHit == true)
            {
                _Anim.SetBool("BeingHurt", true);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                _Anim.SetBool("BeingHurt", false);
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}
