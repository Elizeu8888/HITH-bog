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

        float x = 0f, xmot = 0f;
        float z = 0f, zmot = 0f;

        public TaskBackAway(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (EnemyMediumBT._NavMesh.enabled == false)
            {
                EnemyMediumBT._NavMesh.enabled = true;
            }

            Vector3 lookPos;
            Quaternion targetRot;

            Vector3 samplePoint = _transform.position + Random.insideUnitSphere * 5f -_transform.forward * 10;          

            if(EnemyMediumBT._Dir_Change_Timer <= 0.5f)
            {
                if (NavMesh.SamplePosition(samplePoint, out NavMeshHit hit, 8f, NavMesh.AllAreas))
                {
                    EnemyMediumBT._NavMesh.destination = hit.position;
                    EnemyMediumBT._Dir_Change_Timer = 1f;
                }
            }

            _desVelocity = EnemyMediumBT._NavMesh.desiredVelocity;

            lookPos = EnemyMediumBT._Player.transform.position - _transform.position;
            lookPos.y = 0;
            targetRot = Quaternion.LookRotation(lookPos);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * 3f);

            EnemyMediumBT._charControl.Move(_desVelocity.normalized * 6f * Time.deltaTime);

            EnemyMediumBT._NavMesh.velocity = EnemyMediumBT._charControl.velocity;


            xmot = Vector3.Dot(EnemyMediumBT._charControl.velocity, _transform.right);
            zmot = Vector3.Dot(EnemyMediumBT._charControl.velocity, _transform.forward);

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
