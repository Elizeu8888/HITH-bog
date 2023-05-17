using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class TaskHurt : Node
    {
        //PLAYER NODE
        private Animator _Anim;
        private Transform _transform;
        PlayerHealthAndDamaged plyHealth;
        CharacterController characterController;
        EventSwitch eventSwitch;

        PlayerBT _plyBT;
        public TaskHurt(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            characterController = _transform.GetComponent<CharacterController>();
            eventSwitch = _transform.GetComponent<EventSwitch>();
            plyHealth = _transform.GetComponent<PlayerHealthAndDamaged>();
            _plyBT = _transform.GetComponent<PlayerBT>();
        }

        public override NodeState LogicEvaluate()
        {
            characterController.velocity.Set(0f,0f,0f);           

            if(_plyBT._InCombat)           
                eventSwitch.WeaponRightHand();           
            else
                eventSwitch.WeaponHolster();



            if (!_Anim.GetCurrentAnimatorStateInfo(1).IsName("Hurt"))
            {
                _Anim.Play("Hurt", 0);
                _Anim.Play("Hurt", 1);
            }

            if (_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") || _Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw"))
            {
                _Anim.SetBool("InCombat", true);
                _plyBT._InCombat = true;
                PlayerBT._EventSwitch.WeaponRightHand();
            }

            PlayerBT._WeapAttack.ComboReset();
            plyHealth.EndDashEvent();


            _Anim.SetBool("Blocking", false);
            _Anim.SetBool("Moving", false);


            state = NodeState.RUNNING;
            return state;
        }


    }
}
