using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskSecondLightAttack : Node
    {

        private Animator _Anim;
        private string[] _Animations;
        private Transform _transform;

        float _2ndLayerWeight;

        public TaskSecondLightAttack(Transform transform, string[] animation)
        {
            _Animations = animation;
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }


        public override NodeState LogicEvaluate()
        {

            _Anim.Play(_Animations[1], 1);
            _Anim.Play(_Animations[1], 0);
            PlayerBT.combostep = 1;
            PlayerBT.attackCooldown = 0.6f;

            state = NodeState.RUNNING;
            return state;

        }



    }
}
