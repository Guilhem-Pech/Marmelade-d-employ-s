using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MainMenuController : MonoBehaviour
{
    public CinemachineVirtualCamera _camera;
    public float timeOpening = 0f;
    public BoxCollider2D door;
    public PlayerController playerController;
    public GameObject disableMask;
    
    public void OnClick(InputAction.CallbackContext context)
    {
        StartCoroutine(Open(timeOpening));
    }

    public IEnumerator Open(float time)
    {
        playerController.enabled = true;
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        yield return new WaitForSeconds(time);
        this.enabled = false;
    }
    
    private void OnDisable()
    {
        door.enabled = false;
        disableMask.SetActive(false);
    }
}
