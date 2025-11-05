using UnityEngine;
using UnityEngine.Events;

public abstract class QuizBase : MonoBehaviour
{
    [SerializeField] protected UnityEvent onCorrect;

    public abstract void CheckAnswer();
}