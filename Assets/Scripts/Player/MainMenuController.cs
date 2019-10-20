using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class MainMenuController : MonoBehaviour
{
    public CinemachineBrain brain;
    public CinemachineVirtualCamera _camera;
    public CinemachineVirtualCamera _cameraAfter;
    public float timeOpening = 0f;
    public PlayableDirector director;
    public PlayerController playerController;
    public GameObject disableMask;
    public GameObject main;
    private bool singleTime = false;
    public void OnClick(InputAction.CallbackContext context)
    {
        if(!singleTime)
            StartCoroutine(Open(timeOpening));
    }

    private IEnumerator Open(float time)
    {
        yield return new WaitForSeconds(0.2f);
        singleTime = true;
        main.SetActive(true);
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        director.Play();
        yield return new WaitForSeconds(time);
        this.enabled = false;
        _camera.Priority = 9;
        _cameraAfter.Priority = 10;
        playerController.StartAutoMove(-43f);
        playerController.enabled = true;
    }
    
    private void OnDisable()
    {
        disableMask.SetActive(false);
    }
}
