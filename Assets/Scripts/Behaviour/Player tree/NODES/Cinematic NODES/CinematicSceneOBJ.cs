using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerManager
{
    public class CinematicSceneOBJ : MonoBehaviour
    {
        public CinematicScriptableOBJ _CinematicScriptOBJ;
        public Transform _StartPosition;
        public Transform _EndPosition;

        public Transform _EndCameraPosition;

        [Header("Animation")]

        public Transform[] _CutSceneTransforms;
        public Transform[] _CutSceneEndTransform;

        [Header("Animation")]

        public Animator[] _CutSceneAnimators;
        public string[] _CutSceneAnimations;



    }
}

