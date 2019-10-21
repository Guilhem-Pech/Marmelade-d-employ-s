using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour , Usable
{
   public SpriteRenderer[] alive;
   
   public SpriteRenderer youButDed;
   private OutlineEffect _outlineEffect;
   
   public void SetOutline()
   {
      if (!GetComponent<OutlineEffect>())
      {
         gameObject.AddComponent<OutlineEffect>();
         gameObject.tag = "Usable"; 
      }
                
   }
   
   public void Use(PlayerController user)
   {
      // TODO MUSIC AND FADE
      user.GetComponentInChildren<BlackPanelEffect>().enabled = true;
      foreach (SpriteRenderer sprite in alive)
      {
         sprite.enabled = false;
      }
      AkSoundEngine.PostEvent("Play_Gun_Fire", gameObject);
      youButDed.enabled = true;
        
   }
}
