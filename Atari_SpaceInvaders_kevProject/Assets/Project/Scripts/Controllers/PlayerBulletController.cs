using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.PlayHitAnimation();
            ScoreManager.Instance.AddScore(20);

            gameObject.SetActive(false);
        }
    }
}
