using UnityEngine;

public class TriggerBeginOpenSpace : MonoBehaviour
{
    public Transform aliciaDesk; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponentInParent<PlayerController>().blockMovement = true;
        other.gameObject.GetComponentInParent<PlayerController>().StartAutoMove(aliciaDesk.position.x);
        Destroy(this);
    }
}
