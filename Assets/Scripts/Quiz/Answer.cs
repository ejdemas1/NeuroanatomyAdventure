using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    // Audios played when choosing an answer
    [SerializeField]
    AudioSource correctAudio;
    [SerializeField]
    AudioSource wrongAudio;
    public bool isCorrect = false;
    public QuizManager quizManager;

    // If the correct answer is chosen, the correct audio is played and the answer turns green, then proceed to bonus panel
    // If the wrong answer is chosen, the wrong audio is played and the answer turns red, then turn back to the main game
    public void ProcessAnswer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            correctAudio.Play();
            quizManager.correct();
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            wrongAudio.Play();
            quizManager.incorrect();
        }
    }
}
