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
        PlayerHealthAndDamaged plyHealth;
        WeaponAttack weapAttack;

        PlayerBT _plyBT;
        public CheckDashPressed(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            plyHealth = _transform.GetComponent<PlayerHealthAndDamaged>();
            weapAttack = _transform.GetComponent<WeaponAttack>();
            _plyBT = _transform.GetComponent<PlayerBT>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_plyBT.dashPressed && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Dash") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Left") && !_Anim.GetCurrentAnimatorStateInfo(1).IsName("Deflect Right") && _transform.GetComponent<PlayerHealthAndDamaged>().stunTimer <= 0f)
            {

                plyHealth.dashing = true;
                _transform.GetComponent<WeaponAttack>().canBlock = false;
                weapAttack.ComboReset();

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