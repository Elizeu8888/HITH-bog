using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

using BehaviorTree;
using PlayerManager;

namespace BehaviorTree
{
    public class TaskCutScene : Node
    {
        private Animator _Anim;

        private Transform cam;

        private CharacterController _CharacterController;

        float elapsedTime;
        float desiredTime = 1f;

        Transform _transform;
        Transform _Camera;

        PlayerCinematicHandler _CineMan;

        public TaskCutScene(Transform transform, Transform cam)
        {
            _transform = transform;
            _Camera = cam;
            _Anim = transform.GetComponent<Animator>();
            _CineMan = transform.GetComponent<PlayerCinematicHandler>();
            _CharacterController = transform.GetComponent<CharacterController>();
        }


        public override NodeState LogicEvaluate()
        {

            _transform.rotation = _CineMan._CinematicInScene._StartPosition.rotation;

            // here is the movement
            _CineMan.PlayCutSceneTimeLine();


            //_transform.position = _CineMan._CinematicInScene._EndPosition.position;

            //_CharacterController.enabled = true;

            
            //_Camera.position = _CineMan._CamPosition.position;
            //_Camera.SetParent(_CineMan._CamPosition, true);
            //_Camera.localPosition = new Vector3(0f, 0f, 0f);
            //_Camera.localRotation = Quaternion.Euler(-90, 0, 0);

            state = NodeState.RUNNING;
            return state;
        }


    }
}

