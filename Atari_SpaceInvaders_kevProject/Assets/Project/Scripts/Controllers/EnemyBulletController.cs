using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ShieldController>(out ShieldController shield))
        {
            gameObject.SetActive(false);
            shield.gameObject.SetActive(false);
            return;
        }

        if (other.TryGetComponent<WallController>(out WallController wall))
        {
            gameObject.SetActive(false);
            return;
        }
    }
}

