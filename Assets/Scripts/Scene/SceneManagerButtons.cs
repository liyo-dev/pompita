using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerButtons : MonoBehaviour
{
    public void ButtonPressed()
    {
        AudioManager.Instance.PlayButton();
    }

    /// <summary>
    /// Cambia a la escena especificada por nombre.
    /// </summary>
    /// <param name="sceneName">Nombre de la escena a cargar.</param>
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Cambia a la escena especificada por �ndice.
    /// </summary>
    /// <param name="sceneIndex">�ndice de la escena a cargar (seg�n Build Settings).</param>
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Recarga la escena actual.
    /// </summary>
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    /// <summary>
    /// Cierra la aplicaci�n (funciona solo en builds, no en el editor).
    /// </summary>
    public void QuitApplication()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
