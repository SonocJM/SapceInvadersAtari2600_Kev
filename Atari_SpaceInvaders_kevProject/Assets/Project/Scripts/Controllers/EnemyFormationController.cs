using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormationController : MonoBehaviour
{
    public List<EnemyController> enemies = new List<EnemyController>();
    public float moveSize = 0.5f;
    public float MoveDelay = 0.5f;
    public float horizontalLimit = 4f;
    public float verticalMove = 1f;

    private bool movingRight = true;
    private bool goDown = false;

    private void OnEnable()
    {
        StartCoroutine(MoveFormation());
    }

    private IEnumerator MoveFormation()
    {
        while (true)
        {
            if (!goDown)
            {
                float move = moveSize * (movingRight ? 1 : -1);
                transform.position += Vector3.right * move;

                foreach (var enemy in enemies)
                {
                    if (!enemy.gameObject.activeInHierarchy) continue;

                    float enemyPos = enemy.transform.position.x;
                    if ((movingRight && enemyPos >= horizontalLimit) || (!movingRight && enemyPos <= -horizontalLimit))
                    {
                        goDown = true;
                        StartCoroutine(DropAndSwitchDirection());
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(MoveDelay);
        }
    }

    private IEnumerator DropAndSwitchDirection()
    {
        yield return new WaitForSeconds(MoveDelay);
        transform.position += Vector3.down * verticalMove;

        yield return new WaitForSeconds(MoveDelay);

        movingRight = !movingRight;
        goDown = false;
    }

    public void AddEnemy(EnemyController enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }

    public List<EnemyController> GetEnemies()
    {
        return enemies;
    }

    public bool AllEnemiesDisabled()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
                return false;
        }
        return true;
    }

    public void MultiplySpeed(float multiplier)
    {
        MoveDelay /= multiplier;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.MultiplySpeed(multiplier);
        }
    }
}

