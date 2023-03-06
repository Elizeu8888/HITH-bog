using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskLightAttack : Node
    {

        private Animator _Anim;
        private string[] _Animations;
        private Transform _transform;

        float _2ndLayerWeight;

        public TaskLightAttack(Transform transform, string[] animation)
        {
            _Animations = animation;
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }


        public override NodeState LogicEvaluate()
        {
            if(!_Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                PlayerBT._WeapAttack.Attack();
            }

            state = NodeState.RUNNING;
            return state;

        }






    }
}
