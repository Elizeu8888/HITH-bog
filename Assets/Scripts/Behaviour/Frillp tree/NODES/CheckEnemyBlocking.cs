using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using EnemyManager;

namespace BehaviorTree
{
    public class CheckEnemyBlocking : Node
    {
        //ENEMY NODE
        private Animator _Anim;
        private Transform _transform;
        EnemyHealthManager enemyHealthMan;

        public CheckEnemyBlocking(Transform transform)
        {
            _transform = transform;
            _Anim = _transform.GetComponent<Animator>();
            enemyHealthMan = _transform.GetComponent<EnemyHealthManager>();
        }

        public override NodeState LogicEvaluate()
        {
            if(enemyHealthMan.isBlocking == true)
            {
                _transform.GetComponent<EnemyHealthManager>().dashing = false;
                _transform.GetComponent<WeaponAttack>().canBlock = true;

                _Anim.SetBool("Blocking", true);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                _Anim.SetBool("Blocking", false);
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}