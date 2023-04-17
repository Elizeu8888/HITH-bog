using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using EnemyManager;

namespace BehaviorTree
{
    public class CheckEnemyRoll : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public CheckEnemyRoll(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_transform.GetComponent<EnemyHealthManager>().dashing == true)
            {
                _Anim.SetBool("Dashing", true);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                _Anim.SetBool("Dashing", false);
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}