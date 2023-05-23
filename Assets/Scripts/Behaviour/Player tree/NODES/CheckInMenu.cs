using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class CheckInMenu : Node
    {
        private Animator _Anim;
        private Transform _transform;
        PlayerHealthAndDamaged plyHealth;
        WeaponAttack weapAttack;

        CharacterController _charCont;

        PlayerBT _plyBT;
        public CheckInMenu(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            plyHealth = _transform.GetComponent<PlayerHealthAndDamaged>();
            _charCont = _transform.GetComponent<CharacterController>();
            weapAttack = _transform.GetComponent<WeaponAttack>();
            _plyBT = _transform.GetComponent<PlayerBT>();
        }

        public override NodeState LogicEvaluate()
        {

            if (_plyBT.inMenu)
            {
                plyHealth.enabled = false;
                _charCont.enabled = false;
                state = NodeState.SUCCESS;
                return state;      

            }
            else
            {
                plyHealth.enabled = true;
                _charCont.enabled = true;
                state = NodeState.FAILURE;
                return state;
            }

        }


    }
}