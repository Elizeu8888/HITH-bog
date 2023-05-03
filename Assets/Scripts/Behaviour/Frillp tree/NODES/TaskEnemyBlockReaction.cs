using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using EnemyManager;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class TaskEnemyBlockReaction : Node
    {
        //ENEMY NODE
        private Animator _Anim;
        private Transform _transform;
        EnemyHealthManager _EnemyMedium;
        NavMeshAgent _NavMesh;
        public TaskEnemyBlockReaction(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _EnemyMedium = _transform.GetComponent<EnemyHealthManager>();
            _NavMesh = _transform.GetComponent<NavMeshAgent>();
        }

        public override NodeState LogicEvaluate()
        {


            //_NavMesh.velocity = _Anim.deltaPosition / Time.deltaTime;


            _NavMesh.destination = EnemyMediumBT._Player.transform.position;

            _NavMesh.velocity = 1f * (_Anim.deltaPosition / Time.deltaTime);


            _transform.GetComponent<WeaponAttack>().comboPossible = false;
            _transform.GetComponent<WeaponAttack>().comboStep = 0;

            _transform.GetComponent<EnemyHealthManager>().dashing = false;

            _Anim.SetBool("Moving", false);
            _Anim.SetBool("Blocking", false);

            if (_EnemyMedium._BlockResult == BlockResult.DeflectedRight && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Deflect Right"))
            {
                _Anim.Play("Deflect Right", 1);
                _Anim.Play("Deflect Right", 0);

            }
            if (_EnemyMedium._BlockResult == BlockResult.DeflectedLeft && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Deflect Right"))
            {
                _Anim.Play("Deflect Left", 1);
                _Anim.Play("Deflect Left", 0);

            }
            if (_EnemyMedium._BlockResult == BlockResult.Blocked && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Block Impact"))
            {
                _Anim.Play("Block Impact", 1);
                _Anim.Play("Block Impact", 0);
            }

            state = NodeState.RUNNING;
            return state;

        }



    }
}

