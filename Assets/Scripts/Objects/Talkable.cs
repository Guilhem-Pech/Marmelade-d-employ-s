using System;
using UnityEngine;

namespace Objects
{
    public class Talkable : MonoBehaviour , Usable
    {
        [SerializeField] private Dialogue firstDialogue;
        public float distance = 2;

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
           
            Vector2 userPos = user.transform.position;
            if (Math.Abs(userPos.x - transform.position.x) >= distance)
            {
               user.StartAutoMove(this,distance);
            } else {
                firstDialogue.TriggerDialogue();
            }
        }

       
    }
}