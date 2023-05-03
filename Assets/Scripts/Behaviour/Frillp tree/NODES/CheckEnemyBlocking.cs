using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using EnemyManager;
using Unity.VisualScripting;

namespace BehaviorTree
{
    public class CheckEnemyBlocking : Node
    {
        //ENEMY NODE
        private Animator _Anim;
        private Transform _transform;
        EnemyHealthManager enemyHealthMan;
        float refDistance;
        float _Distance;
        bool isBlocking;



        public CheckEnemyBlocking(Transform transform)
        {
            _transform = transform;
            _Anim = _transform.GetComponent<Animator>();
            enemyHealthMan = _transform.GetComponent<EnemyHealthManager>();
            refDistance = _transform.GetComponent<EnemyMediumBT>().blockDistance;
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;

            

            if (enemyHealthMan.isBlocking == true && _Distance <= refDistance && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt") && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            {
                Debug.Log("blocking");

                enemyHealthMan.blockAttack = true;
                enemyHealthMan.dashing = false;


                enemyHealthMan.dashing = false;
                _transform.GetComponent<WeaponAttack>().canBlock = true;

                _Anim.SetBool("Blocking", true);
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                enemyHealthMan.blockAttack = false;
                _Anim.SetBool("Blocking", false);
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}