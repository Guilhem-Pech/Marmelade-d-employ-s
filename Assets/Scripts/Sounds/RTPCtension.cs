using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RTPCtension : MonoBehaviour
{
    [SerializeField] private float tension = 0f;
    [SerializeField] private AkAmbient ambient;

    private void Update()
    {
        if (tension < 3f)
        {
            //AkSoundEngine.SetRTPCValue("RTPC_Tension", tension);
            tension += Time.deltaTime * 0.5f;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            AkSoundEngine.PostEvent("Play_Test3D", ambient.gameObject);
        }
    }


}
