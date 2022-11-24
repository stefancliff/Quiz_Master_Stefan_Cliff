using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    
    int correctAnswers = 0, questionsSeen = 0;

    public int GetCorrectAnswers(){
        return correctAnswers;
    }

    public void IncrementCorrectAnswers(){
        correctAnswers++;
    }

    public int GetQuestionSeen(){
        return questionsSeen;
    }

    public void IncrementSeenQuestions(){
        questionsSeen++;
    }

    public int CalculateScore(){
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
