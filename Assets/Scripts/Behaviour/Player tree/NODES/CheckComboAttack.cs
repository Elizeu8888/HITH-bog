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

        float _2ndLayerWeight;

        int comboStep;


        public CheckComboAttack(Transform transform, string[] animation)
        {
            _Animations = animation;
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }



        public override NodeState LogicEvaluate()
        {

            if(PlayerBT.combostep == 2)
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
