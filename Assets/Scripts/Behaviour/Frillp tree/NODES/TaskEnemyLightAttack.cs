using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskEnemyLightAttack : Node
    {
        //ENEMY NODE
        private Animator _Anim;
        private string[] _Animations;
        private Transform _transform;

        float _2ndLayerWeight;


        NavMeshAgent _NavMesh;
        CharacterController _charControl;
        Vector3 rootMotion;

        public TaskEnemyLightAttack(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _NavMesh = transform.GetComponent<NavMeshAgent>();
            _charControl = _transform.GetComponent<CharacterController>();
        }


        public override NodeState LogicEvaluate()
        {

            if (!_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {            
                _transform.gameObject.GetComponent<WeaponAttack>().Attack();
            }

            state = NodeState.RUNNING;
            return state;

        }



    }
}
