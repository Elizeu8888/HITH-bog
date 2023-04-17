using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{

    public class CheckAttack : Node
    {
        //ENEMY NODE
        Transform _transform;
        float _Distance;
        Animator _Anim;

        public CheckAttack(Transform transform)
        {
            _transform = transform;
            _Anim = _transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;

            if (_transform.gameObject.GetComponent<EnemyMediumBT>()._CanAttack == true && _Distance >= 1.8f && _Distance <= 9f)
            {
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
    

}