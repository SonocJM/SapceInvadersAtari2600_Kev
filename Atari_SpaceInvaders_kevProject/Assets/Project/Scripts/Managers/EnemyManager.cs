using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Config Wave")]
    [SerializeField] private EnemyData[] enemyTypes;
    [SerializeField] private ObjectPoolManager enemyPool;
    [SerializeField] private ObjectPoolManager bulletPool;

    [SerializeField] private int columns = 7;
    [SerializeField] private int rows = 6;

    [SerializeField] private Vector2 startPos = new Vector2(-4, 3);
    [SerializeField] private Vector2 spacing = new Vector2(1, -1);

    [SerializeField] private EnemyFormationController formationController;

    private int currentRound = 1;
    private float speedMultiplier = 1f;
    private bool roundActive = true;

    private void Start()
    {
        StartRound();
    }

    private void StartRound()
    {
        foreach (var enemy in formationController.GetEnemies())
        {
            enemy.ResetPosition();
            enemy.gameObject.SetActive(false);
        }

        GenerateWave();

        formationController.MultiplySpeed(speedMultiplier);
    }

    private void GenerateWave()
    {
        int index = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 spawnPos = new Vector3(startPos.x + col * spacing.x, startPos.y + row * spacing.y, 0);

                GameObject enemyObj = enemyPool.AskForObject(spawnPos);
                if (enemyObj == null) return;

                enemyObj.transform.SetParent(formationController.transform);

                EnemyController controller = enemyObj.GetComponent<EnemyController>();
                EnemyData data = enemyTypes[index % enemyTypes.Length];

                controller.Setup(data, bulletPool);
                formationController.AddEnemy(controller);

                index++;
            }
        }
    }

    private void Update()
    {
        if (roundActive && formationController.AllEnemiesDisabled())
        {
            roundActive = false;
            currentRound++;
            speedMultiplier += 0.5f;
            StartRound();
            roundActive = true;
        }
    }
}

