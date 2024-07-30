using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    // Buttons for options
    // 0 -> option A; 1 -> option B; 2 -> option C; 3 -> option D; 
    public GameObject[] choices;
    public int currrentQuestion;

    // Buttons that proceed to the bonus panel/main game according to answer
    [SerializeField]
    private GameObject correctOption;
    [SerializeField]
    private GameObject wrongOption;
    [SerializeField]
    private GameObject quizPanel;

    [SerializeField] private Button Answer0Button;
    [SerializeField] private Button Answer1Button;
    [SerializeField] private Button Answer2Button;
    [SerializeField] private Button Answer3Button;


    private void Start()
    {
        generateQuestion();
        quizPanel.SetActive(false);
    }

    public void correct()
    {
        // if the player chooses a correct option, set all buttons to be not interactable and show proceed button
        GeneStats.QnA.RemoveAt(currrentQuestion);
        correctOption.SetActive(true);
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponent<Button>().interactable = false;
        }
    }

    public void incorrect()
    {
        // if the player chooses a wrong option, set all buttons to be not interactable and show proceed button
        choices[GeneStats.QnA[currrentQuestion].CorrectAnswer].GetComponent<Image>().color = Color.green;
        GeneStats.QnA.RemoveAt(currrentQuestion);
        wrongOption.SetActive(true);
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponent<Button>().interactable = false;
        }
    }

    void setAnswers()
    {
        // Set correct option
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].GetComponent<Answer>().isCorrect = false;
            choices[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GeneStats.QnA[currrentQuestion].Answers[i];

            if (GeneStats.QnA[currrentQuestion].CorrectAnswer == i)
            {
                choices[i].GetComponent<Answer>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        currrentQuestion = 0;

        setAnswers();

        quizPanel.SetActive(true);
    }
}
