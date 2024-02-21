using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour, InputControls.IDefaultActions
{
    public static GameInputManager Instance { get; private set; }
    public InputControls inputControls;

    public Action OnButtonPressedEvent;
    public Action OnButtonCanceledEvent;

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationFocus(bool focusStatus) {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable() {
        if (inputControls == null)
        {
            inputControls = new InputControls();
            inputControls.Default.SetCallbacks(this);
        }
        inputControls.Default.Enable();
    }

    void OnDisable() {
        inputControls.Default.Disable();
    }

    public void OnButton(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                OnButtonPressedEvent?.Invoke();
                Debug.Log("Button Pressed");
                break;
            case InputActionPhase.Canceled:
                OnButtonCanceledEvent?.Invoke();
                Debug.Log("Button Canceled");
                break;
        }
    }
}
