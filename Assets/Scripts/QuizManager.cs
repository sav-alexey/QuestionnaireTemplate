using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    // Start is called before the first frame update

    JsonParser jsonParser;

    [SerializeField] GameObject resultCanvas;

    UIGenerator uiGenerator;
    int rightAnswersNumber = 0;
    Queue<JsonParser.Question> questionsQueue = new Queue<JsonParser.Question>();
    

    void Start()
    {        
        jsonParser = GetComponent<JsonParser>();
        var questionnaire = jsonParser.GetQuestionnaire();
        foreach (var question in questionnaire.questions)
        {
            questionsQueue.Enqueue(question);
        }
        GenerateNextQuesqtion();
    }


    void GenerateNextQuesqtion()
    {
        if (questionsQueue.Count > 0)
        {
            var nextQuestion = questionsQueue.Dequeue();
            Dictionary<string, string> answers_dict = new Dictionary<string, string>();
            foreach (var answer in nextQuestion.answers)
            {
                answers_dict.Add(answer.answer_id, answer.answer_text);
            }
            UIGenerator.CheckAnswerDelegate checkAnswerDelegate = CheckAnswer;
            uiGenerator = FindObjectOfType<UIGenerator>();
            uiGenerator.GenerateButtons(answers_dict, nextQuestion.right_answer_id, checkAnswerDelegate);
            uiGenerator.SetQuestion(nextQuestion.question_text);
        }
        else
        {
            Debug.Log("Quiz finished!");
            resultCanvas.SetActive(true);
            resultCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "You answered " + rightAnswersNumber + " questions correctly!";

        }

    }



    void CheckAnswer(string answerId, string rightAnswerId)
    {
        if (answerId == rightAnswerId)
        {
            Debug.Log("Correct!");
            rightAnswersNumber++;
            GenerateNextQuesqtion();
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }
}

