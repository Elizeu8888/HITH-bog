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
        float _Distance, refDistance;
        Animator _Anim;

        public CheckAttack(Transform transform)
        {
            _transform = transform;
            _Anim = _transform.GetComponent<Animator>();
            refDistance = _transform.GetComponent<EnemyMediumBT>().attackDistance;
        }

        public override NodeState LogicEvaluate()
        {
            _Distance = _transform.gameObject.GetComponent<EnemyMediumBT>()._PlayerDistance;

            if (_transform.gameObject.GetComponent<EnemyMediumBT>()._CanAttack == true && _Distance <= refDistance && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt") && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Left") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Right"))
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