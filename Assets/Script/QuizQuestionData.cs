using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName ="Question", menuName = "Data/Quiz Question Data")]
    public class QuizQuestionData : ScriptableObject
    {
        [TextArea(3, 5)]
        public string Question;
        public string[] Answer;
        public int correctOptionIndex;
    }

  

