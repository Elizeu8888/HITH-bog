using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskMoveToPlayer : Node
    {

        Transform _transform;
        Vector3 _desVelocity;

        public TaskMoveToPlayer(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState LogicEvaluate()
        {
            //EnemyMediumBT._NavMesh.destination = EnemyMediumBT._Player.transform.position;


            if(EnemyMediumBT._NavMesh.enabled == false)
            {
                EnemyMediumBT._NavMesh.enabled = true;
            }         

            Vector3 lookPos;
            Quaternion targetRot;

            EnemyMediumBT._NavMesh.destination = EnemyMediumBT._Player.transform.position;
            _desVelocity = EnemyMediumBT._NavMesh.desiredVelocity;

            lookPos = EnemyMediumBT._Player.transform.position - _transform.position;
            lookPos.y = 0;
            targetRot = Quaternion.LookRotation(lookPos);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRot, Time.deltaTime * 3f);

            EnemyMediumBT._charControl.Move(_desVelocity.normalized * 2f * Time.deltaTime);

            EnemyMediumBT._NavMesh.velocity = EnemyMediumBT._charControl.velocity;

            Debug.Log("alking");

            state = NodeState.RUNNING;
            return state;
            

        }


    }
}
