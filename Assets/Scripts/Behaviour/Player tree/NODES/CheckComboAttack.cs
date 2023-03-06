using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class CheckComboAttack : Node
    {

        private Animator _Anim;
        private string[] _Animations;
        private Transform _transform;

        int comboStep;


        public CheckComboAttack(Transform transform, string[] animation)
        {
            _Animations = animation;
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }



        public override NodeState LogicEvaluate()
        {

            if (PlayerBT._WeapAttack.comboStep != 0)
            {
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
