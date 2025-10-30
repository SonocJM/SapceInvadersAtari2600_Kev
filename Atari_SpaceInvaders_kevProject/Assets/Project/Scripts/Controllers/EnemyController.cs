using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyData data;
    [SerializeField] private ObjectPoolManager bulletPool;
    private SpriteRenderer spriteRenderer;

    private bool activeSpriteA = true;
    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private float hitFrameDuration = 0.2f;

    private bool isHit = false;

    private Coroutine shootingCoroutine;
    private Coroutine spriteCoroutine;

    private Vector3 startPosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (shootingCoroutine == null)
            shootingCoroutine = StartCoroutine(Shooting());

        if (spriteCoroutine == null)
            spriteCoroutine = StartCoroutine(SpriteAlternating());
    }

    public void Setup(EnemyData enemyData, ObjectPoolManager bulletPool)
    {
        startPosition = transform.position;
        this.data = enemyData;
        this.bulletPool = bulletPool;
        activeSpriteA = true;
        isHit = false;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (data != null && data.spriteA != null)
            spriteRenderer.sprite = data.spriteA;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

    private IEnumerator SpriteAlternating()
    {
        while (true)
        {
            if (!isHit && spriteRenderer != null)
                ChangeSprite();

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            if (gameObject.activeInHierarchy && !isHit && bulletPool != null && data != null)
            {
                float delay = Random.Range(data.minDelay, data.maxDelay);
                yield return new WaitForSeconds(delay);

                GameObject bullet = bulletPool.AskForObject(transform.position + Vector3.down * 0.5f);
                if (bullet != null)
                    bullet.SetActive(true);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void ChangeSprite()
    {
        if (data == null || spriteRenderer == null) return;

        activeSpriteA = !activeSpriteA;
        spriteRenderer.sprite = activeSpriteA ? data.spriteA : data.spriteB;
    }

    public void PlayHitAnimation()
    {
        if (isHit) return;
        isHit = true;
        StartCoroutine(HitAnimationCoroutine());
    }

    private IEnumerator HitAnimationCoroutine()
    {
        foreach (var sprite in hitSprites)
        {
            if (spriteRenderer != null)
                spriteRenderer.sprite = sprite;

            yield return new WaitForSeconds(hitFrameDuration);
        }

        gameObject.SetActive(false);
        isHit = false;
    }

    public void MultiplySpeed(float multiplier)
    {
        if (data != null)
            data.moveSpeed *= multiplier;
    }

    private void OnDisable()
    {
        EnemyFormationController formation = GetComponentInParent<EnemyFormationController>();
        if (formation != null)
            formation.RemoveEnemy(this);
    }
}
