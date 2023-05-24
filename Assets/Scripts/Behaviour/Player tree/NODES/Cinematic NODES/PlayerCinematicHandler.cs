using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


namespace PlayerManager
{
    public class PlayerCinematicHandler : MonoBehaviour
    {

        public static event Action OnInMenu;
        public static event Action OnLeaveMenu;

        public CinemaCam cineCam;

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

        public bool _EnteredCutScene = true;
        public bool _InputPressed;

        public Transform checkPoint;

        Vector3 posRef = Vector3.zero;

        PlayableDirector direct;

        public bool _InCutScene = false;

        CharacterContGravity grav;

        public Animator[] mainMenuAnimator;
        public string[] mainMenuAnimations;


        void Start()
        {
            _CharCont = gameObject.GetComponent<CharacterController>();
            grav = gameObject.GetComponent<CharacterContGravity>();
        }

        void FixedUpdate()
        {
            if(gameObject.GetComponent<PlayerBT>().inMenu)
            {
                _EnteredCutScene = true;
            }
            if(PlayerBT.deathPressed)
            {
                RestartScene();
                _EnteredCutScene = true;
            }
        }


        void Update()
        {
            
            if (_EnteredCutScene && _CinematicOBJ != null)
            {
                if(PlayerBT.menuPressed)
                {
                    Debug.Log("pressedMENU");
                    _InputPressed = true;
                }
            }
        }

        public void RestartScene()
        {
            _EnteredCutScene = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale;
            PlayerBT._CanPressDeath = false;
            PlayerBT.deathPressed = false;
            _EnteredCutScene = true;
            
        }
    
        public void playMainMenuAnimation()
        {

            /*foreach (Animator i in _CinematicInScene._CutSceneAnimators)
            {
                i.Play(_CinematicInScene._CutSceneAnimations[i], 2);
                i.SetLayerWeight(2, 1);
            }*/

            grav.enabled = false;

            for (int i = 0; i < mainMenuAnimator.Length; i++)
            {
                mainMenuAnimator[i].Play(mainMenuAnimations[i], 2);
                mainMenuAnimator[i].SetLayerWeight(2, 1);
            }
            //direct.Play();


                  
        }

        public void EventEndMainMenuAnim()
        {
            _InputPressed = false;      

            grav.enabled = true;

            for (int i = 0; i < _CinematicInScene._CutSceneTransforms.Length; i++)
            {
                _CinematicInScene._CutSceneTransforms[i].position = _CinematicInScene._CutSceneEndTransform[i].position;
            }

            for (int i = 0; i < _CinematicInScene._CutSceneAnimators.Length; i++)
            {
                //_CinematicInScene._CutSceneAnimators[i].Play(_CinematicInScene._CutSceneAnimations[i], 2);
                _CinematicInScene._CutSceneAnimators[i].SetLayerWeight(2, 0);
            }

            _CinematicInScene._CutSceneTransforms[0].rotation = _CinematicInScene._CutSceneEndTransform[0].rotation;


            OnLeaveMenu?.Invoke();

            cineCam.SetCamTargetToPlayer();

            //transform.position = _CinematicInScene._EndPosition.position;
            //transform.position = _CinematicInScene._EndPosition.position;

            _CharCont.enabled = true;
            _InCutScene = false;
            
            _EnteredCutScene = false;
            Destroy(_CinematicInScene.GetComponent<BoxCollider>());
            Destroy(_CinematicInScene);
            
        }

        public void SetCamTargetToTarget()
        {
            cineCam.target = _CinematicInScene._CutSceneEndTransform[0];
            cineCam.SetCamTarget(); 
        }
        public void SetCamTargetPriority()
        {
            //cineCam.target = _CinematicInScene._CutSceneEndTransform[0];
            cineCam.freeCamera.m_Priority = 15; 
        }
        public void PlayCutSceneTimeLine()
        {
 

            grav.enabled = false;

            for (int i = 0; i < _CinematicInScene._CutSceneAnimators.Length; i++)
            {
                _CinematicInScene._CutSceneAnimators[i].Play(_CinematicInScene._CutSceneAnimations[i], 2);
                _CinematicInScene._CutSceneAnimators[i].SetLayerWeight(2, 1);
            }
            //_CineMachine.transform.position = _CinematicInScene._EndPosition.position;
            //direct.Play();
                  
        }

        public void EventEndCutScene()
        {
            _InputPressed = false;

            grav.enabled = true;

            //_CineMachine.transform.position = _CinematicInScene._EndPosition.position;

            for (int i = 0; i < _CinematicInScene._CutSceneTransforms.Length; i++)
            {
                _CinematicInScene._CutSceneTransforms[i].position = _CinematicInScene._CutSceneEndTransform[i].position;
            }

            for (int i = 0; i < _CinematicInScene._CutSceneAnimators.Length; i++)
            {
                //_CinematicInScene._CutSceneAnimators[i].Play(_CinematicInScene._CutSceneAnimations[i], 2);
                _CinematicInScene._CutSceneAnimators[i].SetLayerWeight(2, 0);
            }

            //transform.position = _CinematicInScene._EndPosition.position;
            //transform.position = _CinematicInScene._EndPosition.position;

            _CharCont.enabled = true;
            _InCutScene = false;
            
            cineCam.SetCamTargetToPlayer();

            _EnteredCutScene = false;
            Destroy(_CinematicInScene.GetComponent<BoxCollider>());
            Destroy(_CinematicInScene);
            
        }

        public void OnTriggerStay(Collider col)
        {

            if (col.transform.gameObject.tag == "cutscene")
            {
                Debug.Log("triggering");
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
            if(_CinematicInScene != null && _CinematicOBJ != null)
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
                  
            }



            return false;
        }

    }

}

