using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskRoll : Node
    {

        private Animator _Anim;
        private string[] _Animations;
        private Transform _transform;

        float _2ndLayerWeight;


        NavMeshAgent _NavMesh;
        CharacterController _charControl;
        Vector3 rootMotion;

        public TaskRoll(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = transform.GetComponent<NavMeshAgent>();
            _charControl = _transform.GetComponent<CharacterController>();
        }


        public override NodeState LogicEvaluate()
        {

            _transform.GetComponent<WeaponAttack>().comboPossible = false;
            _transform.GetComponent<WeaponAttack>().comboStep = 0;

            if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("dash"))
            {
                _Anim.Play("dash");
            }

            _NavMesh.destination = -5f * _transform.forward;

            _NavMesh.velocity = _Anim.deltaPosition / Time.deltaTime;

            Vector3 lookPos;
            Quaternion targetRot;

            lookPos = EnemyMediumBT._Player.transform.position - _transform.position;
            lookPos.y = 0;
            targetRot = Quaternion.LookRotation(lookPos);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * 5f);



            state = NodeState.RUNNING;
            return state;

        }



    }
}
