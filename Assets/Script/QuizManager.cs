using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel, resultPanel;
    public TextMeshProUGUI questionText, timerText;
    public Button[] btnAnswer;
    public Button btnNext, btnRetry, btnContinue, btnQuit;
    public TextMeshProUGUI scoreText, completionText, totalQuestionsText, correctAnswersText, wrongAnswersText, informationText;

    public List<QuizQuestionData> allQuestions;
    public int numberQuestion = 10;
    public float timeQuestion = 90f;

    private List<QuizQuestionData> quizQuestions;
    private int currentIndex = 0, score = 0, correctCount = 0, wrongCount = 0;
    private float currentTime;
    private bool timerOn = false, answered = false;

    void Start()
    {
        btnNext.onClick.AddListener(NextQuestion);
        btnRetry.onClick.AddListener(StartQuiz);
        btnContinue.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        btnQuit.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        StartQuiz();
    }

    void Update()
    {
        if (timerOn)
        {
            currentTime -= Time.deltaTime;
            timerText.text = FormatTime(currentTime);

            if (currentTime <= 0)
            {
                TimeUp();
            }
        }
       
        informationText.text = "Pertanyaan " + (currentIndex +1) +" dari " + quizQuestions.Count.ToString();
        

    }

    void StartQuiz()
    {
        quizPanel.SetActive(true);
        resultPanel.SetActive(false);
        score = correctCount = wrongCount = currentIndex = 0;

        quizQuestions = allQuestions.OrderBy(q => Random.value).Take(numberQuestion).ToList();
        LoadQuestion();
    }

    void LoadQuestion()
    {
        answered = false;
        timerOn = true;
        currentTime = timeQuestion;
        timerText.text = FormatTime(currentTime);
        btnNext.gameObject.SetActive(false);

        var q = quizQuestions[currentIndex];
        questionText.text = q.Question;

        for (int i = 0; i < btnAnswer.Length; i++)
        {
            if (i < q.Answer.Length)
            {
                btnAnswer[i].gameObject.SetActive(true);
                btnAnswer[i].GetComponentInChildren<TextMeshProUGUI>().text = q.Answer[i];
                btnAnswer[i].interactable = true;
                btnAnswer[i].GetComponent<Image>().color = Color.white;

                int index = i;
                btnAnswer[i].onClick.RemoveAllListeners();
                btnAnswer[i].onClick.AddListener(() => SelectAnswer(index));
            }
            else
            {
                btnAnswer[i].gameObject.SetActive(false);
            }
        }
    }

    void SelectAnswer(int index)
    {
        if (answered) return;

        answered = true;
        timerOn = false;
        var q = quizQuestions[currentIndex];

        if (index == q.correctOptionIndex)
        {
            score++;
            correctCount++;
            btnAnswer[index].GetComponent<Image>().color = Color.green;
        }
        else
        {
            wrongCount++;
            btnAnswer[index].GetComponent<Image>().color = Color.red;
            btnAnswer[q.correctOptionIndex].GetComponent<Image>().color = Color.green;
        }

        foreach (var btn in btnAnswer)
            btn.interactable = false;

        btnNext.gameObject.SetActive(true);
    }

    void NextQuestion()
    {
        currentIndex++;
        if (currentIndex < quizQuestions.Count)
        {
            LoadQuestion();
        }
        else
        {
            ShowResult();
        }
    }

    void TimeUp()
    {
        if (answered) return;

        answered = true;
        timerOn = false;
        wrongCount++;

        var q = quizQuestions[currentIndex];
        btnAnswer[q.correctOptionIndex].GetComponent<Image>().color = Color.green;

        foreach (var btn in btnAnswer)
            btn.interactable = false;

        btnNext.gameObject.SetActive(true);
    }

    void ShowResult()
    {
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);
        btnNext.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        scoreText.text = (score*10).ToString();
        completionText.text = "100%";
        totalQuestionsText.text = numberQuestion.ToString();
        correctAnswersText.text = correctCount.ToString();
        wrongAnswersText.text = wrongCount.ToString();
    }

    string FormatTime(float t)
    {
        t = Mathf.Max(0, t);
        int min = Mathf.FloorToInt(t / 60);
        int sec = Mathf.FloorToInt(t % 60);
        return $"{min:00}:{sec:00}";
    }
}
