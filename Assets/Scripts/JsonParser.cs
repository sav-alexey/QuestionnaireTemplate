using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonParser : MonoBehaviour
{

    [SerializeField] TextAsset jsonFile;

    [System.Serializable]
    public class Answer
    {
        public string answer_id;
        public string answer_text;
        public string answer_image;
    }

    [System.Serializable]
    public class Question
    {
        public string question_text;
        public int question_id;
        public string image;
        public Answer[] answers;
        public string right_answer_id;
    }

    [System.Serializable]
    public class Questionnaire
    {
        public string questionnaire_name;
        public string questionnaire_version;
        public string questionnaire_description;
        public Question[] questions;
    }

    public Questionnaire GetQuestionnaire()
    {
        Questionnaire questionnaire = JsonUtility.FromJson<Questionnaire>(jsonFile.text);
        return questionnaire;
    }
}

