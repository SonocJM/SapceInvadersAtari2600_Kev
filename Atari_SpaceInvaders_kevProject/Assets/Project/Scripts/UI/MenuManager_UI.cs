using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager_UI : MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Si estás en el editor, detiene el modo Play
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Si es un build, cierra el juego
            Application.Quit();
#endif
    }
}
