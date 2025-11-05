using UnityEngine;

public class RGBQuiz : QuizBase
{
    [SerializeField] Renderer target;
    Color color = Color.black;

    public void AddR()
    {
        color.r += 1;
        color.r %= 2;
        CheckAnswer();
    }
    public void AddG()
    {
        color.g += 1;
        color.g %= 2;
        CheckAnswer();
    }
    public void AddB()
    {
        color.b += 1;
        color.b %= 2;
        CheckAnswer();
    }

    public override void CheckAnswer()
    {
        target.material.color = color;

        if (color == new Color(1, 1, 0, 1))
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
