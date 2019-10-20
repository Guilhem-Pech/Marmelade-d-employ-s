using UnityEngine;

namespace Trigger
{
    public class TriggerDeskAlicia : MonoBehaviour
    {
        private static readonly int Write = Animator.StringToHash("Write");
        public Vector2 oldPos;
        private void OnTriggerEnter2D(Collider2D other)
        {
            //other.gameObject.GetComponentInParent<PlayerController>().blockMovement = false;
            oldPos = transform.position;
            other.gameObject.GetComponent<Animator>().SetTrigger(Write);
            other.transform.parent.position= new Vector3(-29.17f,-0.08f,0);
            other.transform.parent.localScale = new Vector3(1, 1, 1);
            Destroy(this);
        }
    }
}