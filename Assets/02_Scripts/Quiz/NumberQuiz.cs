using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class NumberQuiz : QuizBase
{
    [SerializeField] int[] correctNumbers;
    [SerializeField] TextMeshProUGUI[] textDisplays;
    int[] currentNumbers;

    public void AddNumber(int index)
    {
        if (currentNumbers == null || currentNumbers.Length != correctNumbers.Length)
        {
            currentNumbers = new int[correctNumbers.Length];
        }

        currentNumbers[index]++;
        currentNumbers[index] %= 10;

        textDisplays[index].text = currentNumbers[index].ToString();

        CheckAnswer();
    }

    public override void CheckAnswer()
    {
        if (currentNumbers.SequenceEqual(correctNumbers))
        {
            onCorrect?.Invoke();
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }
}
