using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield") || other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
