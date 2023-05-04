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

        public CheckRollPressed(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            plyHealth = _transform.GetComponent<PlayerHealthAndDamaged>();
        }

        public override NodeState LogicEvaluate()
        {

            if (Input.GetKeyDown("space") && _transform.GetComponent<PlayerHealthAndDamaged>().stunTimer <= 0f && !_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
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