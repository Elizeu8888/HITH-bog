using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using EnemyManager;

namespace BehaviorTree
{
    public class CheckEnemyBeingDamaged : Node
    {
        //ENEMY NODE
        private Transform _transform;
        private Animator _Anim;
        EnemyHealthManager _EnemyMedium;

        public CheckEnemyBeingDamaged(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _EnemyMedium = _transform.GetComponent<EnemyHealthManager>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_EnemyMedium.beingDamaged == true)
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
