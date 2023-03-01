using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskSprinting : Node
    {
        private Animator _Anim;

        private Transform cam;

        private CharacterController _CharacterController;

        float turnsmoothing = 0.1f;
        float turnsmoothvelocity = 0.5f;

        Transform _transform;

        public TaskSprinting(Transform transform, Transform camera)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            cam = camera;
            _CharacterController = transform.GetComponent<CharacterController>();
        }


        public override NodeState LogicEvaluate()
        {

            // uses input to find direction
            float horizontal = Input.GetAxisRaw("Horizontal") * 0.4f;
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


            // finds direction of movement
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            // here rotates the player to face camera
            float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, cam.transform.eulerAngles.y, ref turnsmoothvelocity, turnsmoothing);
            _transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // here is the movement
            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            _CharacterController.Move(movedir.normalized * 15 * Time.deltaTime);

            //here it sets the animation parameters for strafing. it first gets input from the mouses X input so it can add to make the character move their feet when rotating

            _Anim.SetBool("Moving", true);
            _Anim.SetBool("Sprinting", true);

            state = NodeState.RUNNING;
            return state;
        }


    }
}

