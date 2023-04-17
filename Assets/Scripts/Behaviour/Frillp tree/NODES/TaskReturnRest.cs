using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskReturnRest : Node
    {

        private Animator _Anim;

        Transform _transform;

        float x = 0f, xmot = 0f;
        float z = 0f, zmot = 0f;
        NavMeshAgent _NavMesh;
        float _changeTime;

        public TaskReturnRest(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = transform.GetComponent<NavMeshAgent>();
        }
        public override NodeState LogicEvaluate()
        {

            if (_NavMesh.enabled == false)
            {
                _NavMesh.enabled = true;
            }

            if (_NavMesh.destination != _transform.gameObject.GetComponent<EnemyMediumBT>().enemyRestPos.position)
            {
                _NavMesh.destination = _transform.gameObject.GetComponent<EnemyMediumBT>().enemyRestPos.position;
            }
            else
            {
                _NavMesh.destination = _transform.position;
            }

            _NavMesh.speed = 7f;

            xmot = Vector3.Dot(_NavMesh.velocity, _transform.right);
            zmot = Vector3.Dot(_NavMesh.velocity, _transform.forward);

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
