using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidInputService : IInputService
{
    private Joystick joystick;

    public AndroidInputService(Joystick joystick)
    {
        this.joystick = GameObject.Instantiate(joystick);

        Debug.Log("Android Input service is setted up and ready");
    }


    Vector3 IInputService.MoveDirection => joystick.Direction;

    Vector2 IInputService.RotateDirection => joystick.Rotation;

    public event Action OnJump;
    public event Action OnSprintStart;
    public event Action OnSprintEnd;
    public event Action OnShot;

    public void HandleKeysUpdate()
    {
        throw new NotImplementedException();
    }
}