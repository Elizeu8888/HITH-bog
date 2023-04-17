using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace PlayerManager
{
    public class PlayerCinematicHandler : MonoBehaviour
    {
        public static bool _InCinematic;

        public Transform _CamPosition;
        public Transform _CameraRig;
        public GameObject _CineMachine;

        CharacterController _CharCont;

        bool _WalkToStartPoint;

        Animator _Anim;

        public CinematicScriptableOBJ _CinematicOBJ;
        public CinematicSceneOBJ _CinematicInScene;

        public LayerMask hitLayer;
        RaycastHit _CutSceneHit;

        public bool _EnteredCutScene;
        public bool _InputPressed;

        public Transform checkPoint;

        Vector3 posRef = Vector3.zero;

        PlayableDirector direct;

        public bool _InCutScene = false;

        void Start()
        {
            _CharCont = gameObject.GetComponent<CharacterController>();
        }

        void Update()
        {

            
            if (_EnteredCutScene && _CinematicOBJ != null)
            {
                if(Input.GetKeyDown(_CinematicOBJ._InputWanted))
                {
                    _InputPressed = true;
                }
            }
        }

        public void PlayCutSceneTimeLine()
        {

            direct.Play();
                  
        }

        public void EventEndCutScene()
        {
            _InputPressed = false;
            transform.position = _CinematicInScene._EndPosition.position;
            _CameraRig.position = _CinematicInScene._EndPosition.position;
            _CharCont.enabled = true;
            _InCutScene = false;
            _CineMachine.SetActive(true);
            _EnteredCutScene = false;
            Destroy(_CinematicInScene.GetComponent<BoxCollider>());
            Destroy(_CinematicInScene);
            direct.enabled = false;
            
        }

        public void OnTriggerEnter(Collider col)
        {

            if (col.transform.gameObject.tag == "cutscene")
            {
                _EnteredCutScene = true;
                _CinematicInScene = col.transform.GetComponent<CinematicSceneOBJ>();
                _CinematicOBJ = _CinematicInScene._CinematicScriptOBJ;
                direct = _CinematicInScene.transform.GetComponent<PlayableDirector>();
            }

        }
        void OnTriggerExit(Collider col)
        {
            if (col.transform.gameObject.tag == "cutscene")
            {
                _EnteredCutScene = false;
                _InputPressed = false;
            }

        }

        public bool CanStartCutScene()
        {
            if(_CinematicInScene != null)
            {

                if (_CinematicOBJ._WaitForPlayerInput == true)
                {
                    if (transform.position == _CinematicInScene._StartPosition.position && _InputPressed == true)
                    {
                        _InCutScene = true;
                        return true;
                    }
                }
                else
                {
                    if (transform.position == _CinematicInScene._StartPosition.position)
                    {
                        _InCutScene = true;
                        return true;
                    }
                }
                if (direct.playableGraph.IsValid() && direct.playableGraph.IsPlaying() == true)
                {
                    _InCutScene = true;
                    return true;
                }
                  
            }



            return false;
        }

    }

}

