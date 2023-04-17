using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskEnemyAttacking : Node
    {

        private Animator _Anim;
        private string[] _Animations;
        private Transform _transform;

        float _2ndLayerWeight;


        NavMeshAgent _NavMesh;
        CharacterController _charControl;
        Vector3 rootMotion;

        public TaskEnemyAttacking(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = transform.GetComponent<NavMeshAgent>();
            _charControl = _transform.GetComponent<CharacterController>();
        }


        public override NodeState LogicEvaluate()
        {
            _NavMesh.enabled = true;
            if(_transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance <= 2.2f)
            {
                _NavMesh.destination = _transform.position;
                _NavMesh.velocity = Vector3.zero;
            }
            else
            {
                _NavMesh.destination = EnemyMediumBT._Player.transform.position;

                _NavMesh.velocity = 1.2f * (_Anim.deltaPosition / Time.deltaTime);

                Vector3 lookPos;
                Quaternion targetRot;

                lookPos = EnemyMediumBT._Player.transform.position - _transform.position;
                lookPos.y = 0;
                targetRot = Quaternion.LookRotation(lookPos);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * 5f);
            }

            //_NavMesh.stoppingDistance = 1f;
            //Vector3 rootmotion = _Anim.deltaPosition;
            //rootmotion.y = _NavMesh.nextPosition.y;
            //_transform.position = rootmotion;
            //_NavMesh.nextPosition = _transform.position;

            //rootMotion = _Anim.deltaPosition;
            //_charControl.Move(rootMotion * 1f);
            

            state = NodeState.RUNNING;
            return state;

        }






    }
}
