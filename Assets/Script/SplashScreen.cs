using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    public Image logoImage;
    public VideoPlayer[] tutorialVideo;
    public AudioSource audioSource;
    public float fadeDuration = 3f;
    public string nextSceneName;
    private AsyncOperation asyncLoad;

    void Start()
    {
        for (int i = 0; i < tutorialVideo.Length; i++)
        {
            tutorialVideo[i].Prepare();
        }
        logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, 0f);
        audioSource.Play();

        
        logoImage.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            StartCoroutine(LoadNextScene());
        });
    }

    IEnumerator LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            asyncLoad = SceneManager.LoadSceneAsync("Tutorial");
            asyncLoad.allowSceneActivation = false;

            yield return new WaitForSeconds(2f);

            asyncLoad.allowSceneActivation = true;
        }
        else
        {
            Debug.LogWarning("Next scene name belum diisi di inspector.");
        }
    }
}
