using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyController : MonoBehaviour
{
    private EnemyData data;
    private Transform[] waypoints;
    private SpriteRenderer spriteRenderer;
    private int currentWaypoint = 0;
    private bool ActiveSpriteA = true;

    public void Inicializar(EnemyData enemyData, Transform[] route, SpriteRenderer sr)
    {
        data = enemyData;
        waypoints = route;
        spriteRenderer = sr;
        spriteRenderer.sprite = data.spriteA;

        StartCoroutine(MoveToWaypoint());
        StartCoroutine(Shooting());
    }

    private IEnumerator MoveToWaypoint()
    {
        while (currentWaypoint < waypoints.Length)
        {
            Transform target = waypoints[currentWaypoint];
            while (Vector2.Distance(transform.position, target.position) > 0.05f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, data.moveSpeed * Time.deltaTime);

                ChangeSprite();
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(data.waitTime);
            currentWaypoint++;
        }
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            float delay = Random.Range(data.minDelay, data.maxDelay);
            yield return new WaitForSeconds(delay);

            GameObject bullet = BulletPoolManager.Instance.ObtenerObjeto(data.bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }
    }

    private void ChangeSprite()
    {
        ActiveSpriteA = !ActiveSpriteA;
        spriteRenderer.sprite = ActiveSpriteA ? data.spriteA : data.spriteB;
    }
}
