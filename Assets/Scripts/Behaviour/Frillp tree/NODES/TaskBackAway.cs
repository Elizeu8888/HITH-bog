using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskBackAway : Node
    {

        private Animator _Anim;

        Transform _transform;
        Vector3 _desVelocity;

        NavMeshAgent _NavMesh;
        CharacterController _charControl;
        float _changeTime;


        EnemyMediumBT enemyBT;

        public TaskBackAway(Transform transform, float changetime)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = transform.GetComponent<NavMeshAgent>();
            _charControl = transform.GetComponent<CharacterController>();
            enemyBT = transform.GetComponent<EnemyMediumBT>();
            changetime = _changeTime;
        }


        public override NodeState LogicEvaluate()
        {
            if (_NavMesh.enabled == false)
            {
                _NavMesh.enabled = true;
            }


            //_NavMesh.velocity = Vector3.zero;

            Vector3 lookPos;
            Quaternion targetRot;
            _changeTime -= Time.deltaTime;

            Vector3 samplePoint = _transform.position + Random.insideUnitSphere * 5f - _transform.forward * 5;

            if (_changeTime <= 0.65f)
            {
                if (NavMesh.SamplePosition(samplePoint, out NavMeshHit hit, 8f, NavMesh.AllAreas))
                {
                    _NavMesh.destination = hit.position;
                    _changeTime = 1f;
                }
            }

            _desVelocity = _NavMesh.desiredVelocity;

            lookPos = EnemyMediumBT._Player.transform.position - _transform.position;
            lookPos.y = 0;
            targetRot = Quaternion.LookRotation(lookPos);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * 3f);

            //_charControl.Move(_desVelocity.normalized * 7f * Time.deltaTime);

            //_NavMesh.velocity = _charControl.velocity;

            _NavMesh.speed = enemyBT.backawaySpeed;;

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
