using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoad : MonoBehaviour
{
    public AudioSource btnSound;

    private static SceneLoad instance;

    public void LoadSceneWithSound(string sceneName)
    {
        StartCoroutine(DelayAndLoad(sceneName));
    }

    private IEnumerator DelayAndLoad(string sceneName)
    {
        if (btnSound != null)
        {
            btnSound.Play();
            yield return new WaitForSeconds(btnSound.clip.length);
        }
        SceneManager.LoadScene(sceneName);
    }
}
