using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceRestart : MonoBehaviour
{
    

    public Slider slider;
    bool restartbeingPressed, updateImage;

    float holddowntimer;

    void OnEnable()
    {
        InputManager.OnRestartHeld += RestartTrigger;
        InputManager.OnRestartReleased += RestartReleased;
    }
    void OnDisable()
    {
        InputManager.OnRestartHeld -= RestartTrigger;
        InputManager.OnRestartReleased -= RestartReleased;
    }

    void RestartTrigger()
    {
        restartbeingPressed = true;
        updateImage= true;
    }
    void RestartReleased()
    {
        slider.value = 0; 
        updateImage = false;
        if(holddowntimer >= 3f)
        {
            PlayerBT.deathPressed = true;
        }
            
        restartbeingPressed = false;    
    }


    void Update()
    {

        if(restartbeingPressed)
        {
            holddowntimer += Time.deltaTime;
        }
        if(updateImage)
        {
            slider.value = holddowntimer / 3; 
        }

    }
}
