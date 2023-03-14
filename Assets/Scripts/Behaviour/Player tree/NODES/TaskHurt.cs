using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskHurt : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public TaskHurt(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {         

            if (_Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Draw") || _Anim.GetCurrentAnimatorStateInfo(1).IsName("Sword Redraw"))
            {
                _Anim.SetBool("InCombat", true);
                PlayerBT._InCombat = true;
                PlayerBT._EventSwitch.WeaponRightHand();
            }

            PlayerBT._WeapAttack.ComboReset();

            _Anim.Play("Hurt", 0);
            _Anim.Play("Hurt", 1);

            _Anim.SetBool("Blocking", false);
            _Anim.SetBool("Moving", false);

            state = NodeState.RUNNING;
            return state;
        }


    }
}
