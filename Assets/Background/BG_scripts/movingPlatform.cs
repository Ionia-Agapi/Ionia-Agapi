using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private bool isActivated = false;
    private Transform targetPoint;

    void Start()
    {
        targetPoint = pointB;
    }

    void Update()
    {
        if (isActivated)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // B¿¡ µµ´ÞÇßÀ» ¶§ ÇÃ·§Æû Á¦°Å
        if (targetPoint == pointB && Vector3.Distance(transform.position, pointB.position) < 0.01f)
        {
            Destroy(gameObject); // ÇÃ·§Æû ÆÄ±«
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
