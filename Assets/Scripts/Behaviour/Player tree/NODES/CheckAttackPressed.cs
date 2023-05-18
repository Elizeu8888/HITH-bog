using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckAttackPressed : Node
    {
        private Animator _Anim;
        private Transform _transform;
        PlayerBT _plyBT;



        public CheckAttackPressed(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _plyBT = _transform.GetComponent<PlayerBT>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_plyBT.attackPressed && NotDashing() && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Roll") && PlayerBT.attackCooldown <= 0 && PlayerBT._HealthScript.beingHit == false)
            {
                PlayerBT._HealthScript.isBlocking = false;
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                state = NodeState.FAILURE;
                return state;
            }

        }


        bool NotDashing()
        {
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Dash Attack.F"))
            {
                return false;
            }
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Dash Attack.B"))
            {
                return false;
            }
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Dash Attack.R"))
            {
                return false;
            }
            return true;
        }

    }
}
