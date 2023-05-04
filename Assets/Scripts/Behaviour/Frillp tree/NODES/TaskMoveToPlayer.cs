using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskMoveToPlayer : Node
    {
        private Animator _Anim;

        Transform _transform;
        Vector3 _desVelocity;

        float speed;

        NavMeshAgent _NavMesh;
        CharacterController _charControl;


        float _Distance, _changeTimer;

        public TaskMoveToPlayer(Transform transform, float changetime)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = transform.GetComponent<NavMeshAgent>();
            _charControl = transform.GetComponent<CharacterController>();
            changetime = _changeTimer;
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;


            if (_NavMesh.enabled == false)
            {
                _NavMesh.enabled = true;
            }

            //_NavMesh.velocity = Vector3.zero;

            Vector3 lookPos;
            Quaternion targetRot;


            speed = 7f;
            _NavMesh.destination = EnemyMediumBT._Player.transform.position;
            _changeTimer = 1f;

            

            _desVelocity = _NavMesh.desiredVelocity;

            lookPos = EnemyMediumBT._Player.transform.position - _transform.position;
            lookPos.y = 0;
            targetRot = Quaternion.LookRotation(lookPos);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * 3f);

            //_charControl.Move(_desVelocity.normalized * speed * Time.deltaTime);

            //_NavMesh.velocity = _charControl.velocity;

            _NavMesh.speed = speed;
            //_NavMesh.velocity = Vector3.zero;

            _transform.gameObject.GetComponent<EnemyMediumBT>().MovementAnim();

            if (_NavMesh.velocity.magnitude <= 0.1f)
            {
                _Anim.SetBool("Moving", false);
            }
            else
            {
                _Anim.SetBool("Moving", true);
            }


            state = NodeState.RUNNING;
            return state;
            

        }


    }
}
