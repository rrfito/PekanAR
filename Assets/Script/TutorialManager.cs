using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialSlides;
    public Button nextButton;
    public Button prevButton;


    private int currentSlideIndex = 0;

    public AudioSource Sfx;

    void Start()
    {
        for (int i = 0; i < tutorialSlides.Length; i++)
        {
            tutorialSlides[i].SetActive(false);
            
        }
        ShowSlide(0);
        nextButton.onClick.AddListener(NextSlide);
        prevButton.onClick.AddListener(PrevSlide);
    }
    IEnumerator Delay()
    {
        Sfx.Play();
        yield return new WaitForSeconds(Sfx.clip.length);
        SceneManager.LoadScene("MainMenu");
    }


    void ShowSlide(int index)
    {
        for (int i = 0; i < tutorialSlides.Length; i++)
            tutorialSlides[i].SetActive(i == index);
            
    }

    void PrevSlide()
    {
        Sfx.pitch = 0.14f;
        Sfx.PlayOneShot(Sfx.clip);
        if (currentSlideIndex > 0)
        {
            currentSlideIndex--;
            ShowSlide(currentSlideIndex);
        }
    }

    void NextSlide()
    {
        Sfx.pitch = 1f;
        Sfx.PlayOneShot(Sfx.clip);
        currentSlideIndex++;
        if (currentSlideIndex < tutorialSlides.Length)
        {
            ShowSlide(currentSlideIndex);
        }
        else
        {
            StartCoroutine(Delay());
        }
    }
}
