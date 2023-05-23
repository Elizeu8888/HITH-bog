using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class TaskMoveToCutSceneStart : Node
    {
        private Animator _Anim;

        private Transform cam;

        private CharacterController _CharacterController;

        float elapsedTime;
        float desiredTime = 0.8f;

        Transform _transform;

        PlayerCinematicHandler _CineMan;

        Transform _Cam;
        CharacterContGravity grav;

        public TaskMoveToCutSceneStart(Transform transform, Transform cam)
        {
            _transform = transform;
            _Cam = cam;
            _Anim = transform.GetComponent<Animator>();
            _CineMan = transform.GetComponent<PlayerCinematicHandler>();
            _CharacterController = transform.GetComponent<CharacterController>();
            grav = transform.GetComponent<CharacterContGravity>();
        }


        public override NodeState LogicEvaluate()
        {

            // here is the movement
            grav.enabled = false;

            Vector3 locPoint = new Vector3(_CineMan._CinematicInScene._StartPosition.position.x, _transform.position.y, _CineMan._CinematicInScene._StartPosition.position.z);
            Vector3 movedir = locPoint - _transform.position;
            movedir.y = _transform.position.y;


            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / desiredTime;

            //_CharacterController.Move(movedir * 6f * Time.deltaTime);
            _CharacterController.enabled = false;
            _transform.position = Vector3.Lerp(_transform.position, _CineMan._CinematicInScene._StartPosition.position, percent);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, _CineMan._CinematicInScene._StartPosition.rotation, percent / 2);
            

            //_transform.rotation = Quaternion.LookRotation(_CharacterController.velocity);

            
            _CineMan._CameraRig.position = _CineMan._CinematicInScene._StartPosition.position;
            _CineMan._CameraRig.rotation = _CineMan._CinematicInScene._StartPosition.rotation;

            //_CineMan._CameraRig.transform.rotation = _CineMan._CinematicInScene._StartPosition.rotation;




            //Quaternion campos = Quaternion.EulerAngles(_CineMan._CamPosition.rotation.x,_CineMan._CamPosition.rotation.y, _CineMan._CamPosition.rotation.x)

            //_Cam.SetParent(_CineMan._CamPosition, true);

            //_Cam.localPosition = Vector3.Lerp(_Cam.localPosition, Vector3.zero, percent);

            //_Cam.localRotation = Quaternion.Lerp(_Cam.localRotation, Quaternion.Euler(-90,0,0), percent);

            //here it sets the animation parameters for strafing. it first gets input from the mouses X input so it can add to make the character move their feet when rotating

            _Anim.SetBool("Moving", true);
            _Anim.SetBool("Sprinting", false);

            state = NodeState.RUNNING;
            return state;
        }


    }
}

