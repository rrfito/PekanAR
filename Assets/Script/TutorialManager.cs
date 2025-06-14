using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialSlides;
    public Button nextButton;
    public Button prevButton;
    public string nextSceneName = "MainMenu";


    private int currentSlideIndex = 0;

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

    void ShowSlide(int index)
    {
        for (int i = 0; i < tutorialSlides.Length; i++)
            tutorialSlides[i].SetActive(i == index);
            
    }

    void PrevSlide()
    {
        if (currentSlideIndex > 0)
        {
            currentSlideIndex--;
            ShowSlide(currentSlideIndex);
        }
    }

    void NextSlide()
    {
        currentSlideIndex++;
        if (currentSlideIndex < tutorialSlides.Length)
        {
            ShowSlide(currentSlideIndex);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
