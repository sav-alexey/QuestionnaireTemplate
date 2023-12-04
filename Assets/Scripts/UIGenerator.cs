using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGenerator : MonoBehaviour
{

    public GameObject buttonPrefab;
    List<GameObject> buttonsList = new List<GameObject>();

    public delegate void CheckAnswerDelegate(string answerId, string rightAnswerId);
    
    public void GenerateButtons(Dictionary<string, string> answers, string rightAnswerId, CheckAnswerDelegate CheckAnswer)
    {

        if (buttonsList.Count > 0)
        {
            foreach (var button in buttonsList)
            {
                Destroy(button);
            }
            buttonsList.Clear();
        }

        foreach (var answer in answers)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(transform, false);    
            button.GetComponentInChildren<TextMeshProUGUI>().text = answer.Value;
            buttonsList.Add(button);
            button.GetComponent<Button>().onClick.AddListener(() => { CheckAnswer(answer.Key, rightAnswerId); });
        }
    }

    public void SetQuestion(string question)
    {
        GameObject questionText = GameObject.Find("Question");
        questionText.GetComponent<Text>().text = question;
    }

}
