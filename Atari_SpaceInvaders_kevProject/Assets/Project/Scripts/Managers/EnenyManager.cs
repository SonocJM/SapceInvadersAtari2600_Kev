using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Configuración de Oleada")]
    [SerializeField] private EnemyData[] EnemyScriptables;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform spawnParent;

    void Start()
    {
        GenerateRounds();
    }

    private void GenerateRounds()
    {
        foreach (EnemyData data in EnemyScriptables)
        {
            GameObject enemyGO = new GameObject(data.name);
            enemyGO.transform.SetParent(spawnParent);
            enemyGO.transform.position = waypoints[0].position;

            SpriteRenderer sr = enemyGO.AddComponent<SpriteRenderer>();
            EnemyController enemy = enemyGO.AddComponent<EnemyController>();

            enemy.Inicializar(data, waypoints, sr);
        }
    }
}

