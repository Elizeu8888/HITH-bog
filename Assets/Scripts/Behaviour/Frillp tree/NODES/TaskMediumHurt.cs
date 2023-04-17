using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskMediumHurt : Node
    {
        private Animator _Anim;
        private Transform _transform;
        NavMeshAgent _NavMesh;

        public TaskMediumHurt(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = _transform.GetComponent<NavMeshAgent>();
        }

        public override NodeState LogicEvaluate()
        {


            _NavMesh.enabled = true;

            _NavMesh.velocity = _Anim.deltaPosition / Time.deltaTime;


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
