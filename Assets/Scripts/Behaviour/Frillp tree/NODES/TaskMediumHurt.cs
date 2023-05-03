using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;
using EnemyManager;

namespace BehaviorTree
{
    public class TaskMediumHurt : Node
    {
        //ENEMY NODE
        private Animator _Anim;
        private Transform _transform;
        NavMeshAgent _NavMesh;
        EnemyHealthManager enemyHealthMan;
        public TaskMediumHurt(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = _transform.GetComponent<NavMeshAgent>();
            enemyHealthMan = _transform.GetComponent<EnemyHealthManager>();

        }

        public override NodeState LogicEvaluate()
        {

            _NavMesh.destination = EnemyMediumBT._Player.transform.position;

            _NavMesh.velocity = 1.4f * (_Anim.deltaPosition / Time.deltaTime);

            enemyHealthMan.dashing = false;

            if (!_Anim.GetCurrentAnimatorStateInfo(1).IsName("Hurt"))
            {
                _Anim.Play("Hurt", 0);
            }

            _transform.GetComponent<WeaponAttack>().ComboReset();


            _Anim.SetBool("Moving", false);


            state = NodeState.RUNNING;
            return state;
        }


    }
}
