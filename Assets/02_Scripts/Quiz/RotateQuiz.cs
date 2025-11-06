using System.Collections;
using UnityEngine;

public class RotateQuiz : QuizBase
{
    [SerializeField] int[] correctRotations;
    [SerializeField] Transform[] targets;

    [SerializeField] int maxRotation = 3;
    int RotateAmount { get => 360 / maxRotation; }
    [SerializeField] float rotateTime = .5f;

    bool isRotating = false;

    public void Rotate(int index)
    {
        if (isRotating) return;

        StartCoroutine(CoRotate(index));
    }

    IEnumerator CoRotate(int index)
    {
        isRotating = true;
        float timer = 0f;
        Quaternion startRotation = targets[index].rotation;

        while (timer < rotateTime)
        {
            timer += Time.deltaTime;
            targets[index].rotation = Quaternion.Lerp(startRotation, startRotation * Quaternion.Euler(0, RotateAmount, 0), timer / rotateTime);
            yield return null;
        }

        targets[index].rotation = startRotation * Quaternion.Euler(0, RotateAmount, 0);
        isRotating = false;
        CheckAnswer();
    }

    public override void CheckAnswer()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            int currentRotation = Mathf.RoundToInt(targets[i].eulerAngles.y) % 360;
            if (currentRotation != correctRotations[i])
            {
                Debug.Log("Wrong!");
                return;
            }
        }

        Debug.Log("Correct!");
        onCorrect?.Invoke();
    }
}
