using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckBeingDamaged : Node
    {
        private Transform _transform;
        private Animator _Anim;

        public CheckBeingDamaged(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (PlayerBT._HealthScript.beingDamaged)
            {

                if(NotDashing())
                {
                    _Anim.SetBool("BeingHurt", true);
                    state = NodeState.SUCCESS;
                    return state;
                }

                
                _Anim.SetBool("BeingHurt", false);
                state = NodeState.FAILURE;
                return state;

            }
            else
            {
                _Anim.SetBool("BeingHurt", false);
                state = NodeState.FAILURE;
                return state;
            }

        }


        bool NotDashing()
        {
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Dash Attack_F_Combo"))
            {
                return false;
            }
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Dash Attack_F"))
            {
                return false;
            }
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Dash Attack_B"))
            {
                return false;
            }
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Dash Attack_R"))
            {
                return false;
            }
            if(_Anim.GetCurrentAnimatorStateInfo(1).IsName("Dash Attack_L"))
            {
                return false;
            }
            return true;
        }

    }
}
