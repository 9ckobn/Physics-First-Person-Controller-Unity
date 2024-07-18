using System;
using UnityEngine;

public class DesktopInputService : IInputService
{
    public DesktopInputService()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //TIPS Здесь может быть любая карта управления. Например Dictionary<Keycode, OnKeyAction>, может быть даже new InputSystem
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const string HorizontalRotationAxisName = "Mouse X";
    private const string VerticalRotationAxisName = "Mouse Y";

    public Vector3 MoveDirection => new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));

    public Vector2 RotateDirection => new Vector2(Input.GetAxisRaw(HorizontalRotationAxisName), Input.GetAxisRaw(VerticalRotationAxisName));

    public event Action OnJump;
    public event Action OnSprintStart;
    public event Action OnSprintEnd;
    public event Action OnShot;


    //TIPS: Если потребуется пересесть на новую инпутсистему или кастомную, то это решение максимально бесболезнено,
    // т.к. никто не зависит на конкретных bool-ах, а только лишь на On'SomeAction' 
    public void HandleKeysUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnSprintStart?.Invoke();
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnSprintEnd?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            OnShot?.Invoke();
        }
    }
}
