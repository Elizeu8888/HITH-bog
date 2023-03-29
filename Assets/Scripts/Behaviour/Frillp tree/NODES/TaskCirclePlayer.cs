using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskCirclePlayer : Node
    {
        private Animator _Anim;
        Transform _transform;
        Vector3 _desVelocity;

        public TaskCirclePlayer(Transform transform)
        {
            //_Anim = transform.GetComponent<Animator>();
            _transform = transform;
        }


        public override NodeState LogicEvaluate()
        {

            if (EnemyMediumBT._NavMesh.enabled == false)
            {
                EnemyMediumBT._NavMesh.enabled = true;
            }

            if (EnemyMediumBT._Dir_Change_Timer <= 0f)
            {
                EnemyMediumBT._Dir_Change_Timer = 1f;
            }

            Vector3 lookPos;
            Quaternion targetRot;

            EnemyMediumBT._NavMesh.destination = EnemyMediumBT._directions[1].position;
            _desVelocity = EnemyMediumBT._NavMesh.desiredVelocity;

            lookPos = EnemyMediumBT._Player.transform.position - _transform.position;
            lookPos.y = 0;
            targetRot = Quaternion.LookRotation(lookPos);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * 3f);

            EnemyMediumBT._charControl.Move(_desVelocity.normalized * 2f * Time.deltaTime);

            EnemyMediumBT._NavMesh.velocity = EnemyMediumBT._charControl.velocity;

            Debug.Log("circling");

            state = NodeState.RUNNING;
            return state;
        }

    }
}
