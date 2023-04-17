using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerManager
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Cinematic")]
    public class CinematicScriptableOBJ : ScriptableObject
    {
        public string _CutSceneName;
        public bool _EndInCombat;
        public bool _StartInCombat;
        public bool _WaitForPlayerInput;
        public string _InputWanted;

        void Start()
        {

        }


        void Update()
        {

        }
    }
}


