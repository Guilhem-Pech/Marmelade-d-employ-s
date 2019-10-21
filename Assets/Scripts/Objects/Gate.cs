using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject open;
    public GameObject close;
    public void Open()
    {
        AkSoundEngine.PostEvent("Play_Gate_Ok_Bip", gameObject);
        open.SetActive(true);
        close.SetActive(false);
    }
}
