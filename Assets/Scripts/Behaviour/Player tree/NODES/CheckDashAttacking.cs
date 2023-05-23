using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckDashAttacking : Node
    {
        private Animator _Anim;
        private Transform _transform;
        PlayerHealthAndDamaged plyHealth;
        WeaponAttack weapAttack;

        PlayerBT _plyBT;
        public CheckDashAttacking(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            plyHealth = _transform.GetComponent<PlayerHealthAndDamaged>();
            weapAttack = _transform.GetComponent<WeaponAttack>();
            _plyBT = _transform.GetComponent<PlayerBT>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_plyBT.attackPressed && !NotDashing() && _transform.GetComponent<WeaponAttack>().comboPossible)
            {

                plyHealth.dashing = true;
                _transform.GetComponent<WeaponAttack>().comboStep = -2;

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