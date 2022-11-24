using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;

    void Start() 
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }
    void Awake()
    {
        quiz        = FindObjectOfType<Quiz>();
        endScreen   = FindObjectOfType<EndScreen>();
    }

    
    void Update()
    {
        if(quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void onReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this restarts the game by reloading the first scene
    }
}
