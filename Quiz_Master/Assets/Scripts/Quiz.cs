using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
        
        [SerializeField] TextMeshProUGUI questionText;
        [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
        QuestionSO currentQuestion;

    [Header("Answers")]
        [SerializeField] GameObject[] answerButton;
        int correctAnswerIndex;
        bool hasAnsweredEarly = true;

    [Header("Button Colours")]
        [SerializeField] Sprite defaultAnswerSprite, correctAnswerSprite; 

    [Header("Timer")]
        [SerializeField] Image timerImage;
        Timer timer;

    [Header("Scoring")]
        [SerializeField] TextMeshProUGUI scoreText;
        ScoreKeeper scoreKeeper;
    
    [Header("ProgressBar")]
        [SerializeField] Slider progressBar;
    bool state;
    public bool isComplete;
    void Awake()
    {
        timer                   = FindObjectOfType<Timer>();
        scoreKeeper             = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue    = questions.Count;
        progressBar.value       = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);      // so that we don't show the correct answer, instead fall into the else block
            SetButtonState(false);
        }
    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for(int i = 0; i < answerButton.Length; i++) 
        {
            TextMeshProUGUI buttonText  = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text             = currentQuestion.getAnswer(i);
        }
    }

    void DisplayAnswer(int index) 
    {
        Image buttonImage;

        if(index == currentQuestion.getAnswerIndex())
        {
            questionText.text   = "Correct!";
            buttonImage         = answerButton[index].GetComponent<Image>();
            buttonImage.sprite  = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex      = currentQuestion.getAnswerIndex();
            string correctAnswer    = currentQuestion.getAnswer(correctAnswerIndex);
            questionText.text       = "Sorry, the correct answer was:\n" + correctAnswer;

            buttonImage         = answerButton[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite  = correctAnswerSprite;
        }
    }
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + " %";
        
    }

    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementSeenQuestions();
            progressBar.value++;
        }
       
    }

    private void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButton.Length; i++)
        {
            Button button       = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        Image buttonImage;
        for (int i = 0; i < answerButton.Length; i++)
        {
            buttonImage         = answerButton[i].GetComponent<Image>();
            buttonImage.sprite  = defaultAnswerSprite;
        }
    }

    
}
