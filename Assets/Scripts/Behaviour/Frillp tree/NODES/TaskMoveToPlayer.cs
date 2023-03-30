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

        float x = 0f, xmot = 0f;
        float z = 0f, zmot = 0f;
        NavMeshAgent _NavMesh;
        CharacterController _charControl;
        float _Distance;

        public TaskMoveToPlayer(Transform transform,float distance, NavMeshAgent nav,CharacterController cha)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = nav;
            _charControl = cha;
            _Distance = distance;
        }

        public override NodeState LogicEvaluate()
        {
            //EnemyMediumBT._NavMesh.destination = EnemyMediumBT._Player.transform.position;


            if(_NavMesh.enabled == false)
            {
                _NavMesh.enabled = true;
            }         

            Vector3 lookPos;
            Quaternion targetRot;

            Vector3 samplePoint = _transform.position + Random.insideUnitSphere * 5f;

            if (EnemyMediumBT._Dir_Change_Timer <= 0f)
            {
                if (_Distance <= 8f)
                {
                    if (NavMesh.SamplePosition(samplePoint, out NavMeshHit hit, 5f, NavMesh.AllAreas))
                    {
                        _NavMesh.destination = hit.position;
                        EnemyMediumBT._Dir_Change_Timer = 0.7f;
                    }
                }
                else
                {
                    _NavMesh.destination = EnemyMediumBT._Player.transform.position;
                }
            }


            _desVelocity = _NavMesh.desiredVelocity;

            lookPos = EnemyMediumBT._Player.transform.position - _transform.position;
            lookPos.y = 0;
            targetRot = Quaternion.LookRotation(lookPos);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * 3f);

            _charControl.Move(_desVelocity.normalized * 3f * Time.deltaTime);

            _NavMesh.velocity = _charControl.velocity;



            xmot = Vector3.Dot(_charControl.velocity, _transform.right);
            zmot = Vector3.Dot(_charControl.velocity, _transform.forward);

            if (xmot < 0.5f)
            {
                _Anim.SetFloat("Xdir", x = Mathf.MoveTowards(x, 1, 3f * Time.deltaTime));
            }
            if (xmot > 0.5f)
            {
                _Anim.SetFloat("Xdir", x = Mathf.MoveTowards(x, -1, 5f * Time.deltaTime));
            }
            if (zmot < 0.5f)
            {
                _Anim.SetFloat("Ydir", z = Mathf.MoveTowards(z, 1, 3f * Time.deltaTime));
            }
            if (zmot > 0.5f)
            {
                _Anim.SetFloat("Ydir", z = Mathf.MoveTowards(z, -1, 3f * Time.deltaTime));
            }




            _Anim.SetBool("Moving", true);

            state = NodeState.RUNNING;
            return state;
            

        }


    }
}
