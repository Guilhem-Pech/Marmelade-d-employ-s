using System.Collections;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Scroll());
    }

    public float yEndPos = 843;
    // Update is called once per frame
    IEnumerator Scroll()
    {
        Transform transform1 = transform;
        
        while (transform1.position.y < yEndPos)
        {
            Vector3 position = transform1.position;
            Vector2 pos = new Vector2(position.x, position.y + 1);
            transform1.position = pos;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
