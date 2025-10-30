using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WallController : MonoBehaviour
{
    [SerializeField] private TMP_Text loseText;
    private bool gameOver = false;

    void Start()
    {
        loseText.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out var enemy) && !gameOver)
        {
            StartCoroutine(Defeat());
        }
    }

    private IEnumerator Defeat()
    {
        gameOver = true;

        if (loseText != null)
        {
            loseText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Menu");
    }
}


