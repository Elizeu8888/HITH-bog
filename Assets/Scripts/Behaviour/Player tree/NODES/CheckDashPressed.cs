using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckDashPressed : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public CheckDashPressed(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if (Input.GetKeyDown("space") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Left") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Right") && _transform.GetComponent<PlayerHealthAndDamaged>().stunTimer <= 0f && !_Anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            {

                _transform.GetComponent<PlayerHealthAndDamaged>().dashing = true;
                _transform.GetComponent<WeaponAttack>().canBlock = false;

                _Anim.Play("Dash", 0);
                _Anim.Play("Dash", 1);
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