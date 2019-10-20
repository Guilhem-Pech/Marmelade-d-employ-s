using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerInAnimation : MonoBehaviour
{
    public void Footstep()
    {
        AkSoundEngine.PostEvent("Play_Walking", gameObject);
    }
}
