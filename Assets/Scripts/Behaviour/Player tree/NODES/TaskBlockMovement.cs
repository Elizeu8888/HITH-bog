using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

namespace BehaviorTree
{
    public class TaskBlockMovement : Node
    {
        private Animator _Anim;

        private Transform cam;

        private CharacterController _CharacterController;

        float turnsmoothing = 0.1f;
        float turnsmoothvelocity = 0.5f;

        float mouseXSmooth = 0f;

        Transform _transform;

        float z = 0f;
        float x = 0f;

        public TaskBlockMovement(Transform transform, Transform camera)
        {
            _transform = transform;
            _Anim = transform.GetComponent<Animator>();
            cam = camera;
            _CharacterController = transform.GetComponent<CharacterController>();
        }


        public override NodeState LogicEvaluate()
        {

            // uses input to find direction
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


            // finds direction of movement
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            // here rotates the player to face camera
            float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, cam.transform.eulerAngles.y, ref turnsmoothvelocity, turnsmoothing);
            _transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // here is the movement
            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            _CharacterController.Move(movedir.normalized * 4 * Time.deltaTime);

            //here it sets the animation parameters for strafing. it first gets input from the mouses X input so it can add to make the character move their feet when rotating


            mouseXSmooth = Mathf.Lerp(mouseXSmooth, Input.GetAxisRaw("Mouse X"), 5.5f * Time.deltaTime);
            _Anim.SetFloat("InputY", z = Mathf.MoveTowards(z, direction.z, 5f * Time.deltaTime));
            _Anim.SetFloat("InputX", mouseXSmooth + (x = Mathf.MoveTowards(x, direction.x, 5f * Time.deltaTime)));

            _Anim.SetBool("Moving", true);
            _Anim.SetBool("Sprinting", false);

            state = NodeState.RUNNING;
            return state;
        }


    }
}
