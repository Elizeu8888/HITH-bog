using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckRollPressed : Node
    {
        private Animator _Anim;
        private Transform _transform;
        PlayerHealthAndDamaged plyHealth;

        PlayerBT _plyBT;
        public CheckRollPressed(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            plyHealth = _transform.GetComponent<PlayerHealthAndDamaged>();
            _plyBT = _transform.GetComponent<PlayerBT>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_plyBT.dashPressed && plyHealth.stunTimer <= 0f && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && plyHealth.rollPossible)
            {

                plyHealth.rolling = true;
                _transform.GetComponent<WeaponAttack>().canBlock = false;

                _Anim.Play("Roll", 0);
                _Anim.Play("Roll", 1);
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