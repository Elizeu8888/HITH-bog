using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskIdle : Node
    {
        private Animator _Anim;

        public TaskIdle(Transform transform)
        {
            _Anim = transform.GetComponent<Animator>();
            
        }


        public override NodeState LogicEvaluate()
        {
            if(PlayerBT.attackCooldown <= -0.5)
                PlayerBT.combostep = 1;
            _Anim.SetBool("Moving", false);
            state = NodeState.RUNNING;
            return state;
        }

    }
}

