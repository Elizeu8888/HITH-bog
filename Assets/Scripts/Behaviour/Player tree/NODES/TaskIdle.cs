using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskIdle : Node
    {
        private Animator _Anim;

        //float mouseXSmooth = 0f;

        public TaskIdle(Transform transform)
        {
            _Anim = transform.GetComponent<Animator>();
            
        }


        public override NodeState LogicEvaluate()
        {

            //here it makes the character strafe left adn right when rotating the camera

            _Anim.SetBool("Moving", false);
            state = NodeState.RUNNING;
            return state;
        }

    }
}

