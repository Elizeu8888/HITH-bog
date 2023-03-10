using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class TaskBlockReaction : Node
    {
        private Animator _Anim;
        private Transform _transform;

        public TaskBlockReaction(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
        }

        public override NodeState LogicEvaluate()
        {

            if(PlayerBT._HealthScript._BlockResult == BlockResult.Deflected)
            {
                _Anim.Play("Deflect Right", 1);
            }
            if (PlayerBT._HealthScript._BlockResult == BlockResult.Blocked)
            {
                _Anim.Play("sword guard Block", 1);
            }

            state = NodeState.RUNNING;
            return state;

        }


    }
}

