using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyBulletController>(out EnemyBulletController bullet))
        {
            gameObject.SetActive(false);

            bullet.gameObject.SetActive(false);
        }
    }
}

