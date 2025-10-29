using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Sprites")]
    public Sprite spriteA;
    public Sprite spriteB;

    [Header("Movement")]
    public float moveSpeed;
    public float waitTime;

    [Header("Shoot")]
    public GameObject bulletPrefab;
    public float minDelay;
    public float maxDelay;
}

