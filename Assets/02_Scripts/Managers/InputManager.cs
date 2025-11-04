using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // InputActionReference를 사용하여 인풋 액션을 참조합니다.
    [Header(" - Input Action References - ")]
    [SerializeField] InputActionReference moveRef;
    [SerializeField] InputActionReference lookRef;
    [SerializeField] InputActionReference interactRef;
    [SerializeField] InputActionReference escapeRef;

    [SerializeField] InputActionReference mouseLClickRef;
    [SerializeField] InputActionReference mouseRClickRef;


    // 인풋 이벤트를 위한 델리게이트 선언 (함수를 변수처럼 전달 가능)
    public static Action<Vector2> OnMoveInput;
    public static Action<Vector2> OnLookInput;
    public static Action OnInteractInput;
    public static Action OnEscapeInput;

    public static Action OnMouseLClickInput;
    public static Action OnMouseRClickInput;


    private void OnEnable()
    {
        interactRef.action.started += InteractInputStarted;
        escapeRef.action.started += EscapeInputStarted;
        mouseLClickRef.action.started += MouseLClickInputStarted;
        mouseRClickRef.action.started += MouseRClickInputStarted;
    }

    private void OnDisable()
    {
        interactRef.action.started -= InteractInputStarted;
        escapeRef.action.started -= EscapeInputStarted;
        mouseLClickRef.action.started -= MouseLClickInputStarted;
        mouseRClickRef.action.started -= MouseRClickInputStarted;
    }

    private void Update()
    {
        if(moveRef.action.IsPressed())
        {
            Vector2 moveInput = moveRef.action.ReadValue<Vector2>();
            OnMoveInput?.Invoke(moveInput);
        }

        if(lookRef.action.IsPressed())
        {
            Vector2 lookInput = lookRef.action.ReadValue<Vector2>();
            OnLookInput?.Invoke(lookInput);
        }
    }
    private void InteractInputStarted(InputAction.CallbackContext context)
    {
        OnInteractInput?.Invoke();
    }
    private void EscapeInputStarted(InputAction.CallbackContext context)
    {
        OnEscapeInput?.Invoke();
    }

    private void MouseLClickInputStarted(InputAction.CallbackContext context)
    {
        OnMouseLClickInput?.Invoke();
    }
    private void MouseRClickInputStarted(InputAction.CallbackContext context)
    {
        OnMouseRClickInput?.Invoke();
    }
}
