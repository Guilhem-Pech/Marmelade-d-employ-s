using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour , Usable
{
   public SpriteRenderer[] alive;
   
   public SpriteRenderer youButDed;
   private OutlineEffect _outlineEffect;

   private void Awake()
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
      foreach (SpriteRenderer sprite in alive)
      {
         sprite.enabled = false;
      }

      youButDed.enabled = true;
   }
}
