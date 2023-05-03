using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;
using EnemyManager;

namespace BehaviorTree
{
    public class TaskEnemyBlock : Node
    {
        //ENEMY NODE
        private Animator _Anim;
        private Transform _transform;
        NavMeshAgent _NavMesh;

        public TaskEnemyBlock(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = _transform.GetComponent<NavMeshAgent>();
        }

        public override NodeState LogicEvaluate()
        {

            //_NavMesh.velocity = _Anim.deltaPosition / Time.deltaTime;

            _Anim.SetBool("Blocking", false);

            _NavMesh.destination = _transform.position;


            _transform.GetComponent<WeaponAttack>().comboPossible = false;
            _transform.GetComponent<WeaponAttack>().comboStep = 0;

            _transform.GetComponent<EnemyHealthManager>().dashing = false;

            _Anim.SetBool("Moving", false);


            state = NodeState.RUNNING;
            return state;
        }


    }
}
