using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskIdle : Node
    {
        private Animator _Anim;
        Transform _transform;
        CharacterController _CharacterController;
        public TaskIdle(Transform transform)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            _CharacterController = transform.GetComponent<CharacterController>();
        }


        public override NodeState LogicEvaluate()
        {

            //here it makes the character strafe left adn right when rotating the camera
            if(_CharacterController.enabled)
            {
                _CharacterController.SimpleMove(Vector3.zero);
            }
            
            _Anim.SetFloat("InputX", 0f);
            _Anim.SetFloat("InputY", 0f);


            _Anim.SetBool("Moving", false);
            state = NodeState.RUNNING;
            return state;
        }

    }
}

