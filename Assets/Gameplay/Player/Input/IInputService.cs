using System;
using UnityEngine;

public interface IInputService
{
    public Vector3 MoveDirection { get; }
    public Vector2 RotateDirection { get; }

    public event Action OnJump;
    public event Action OnSprintStart;
    public event Action OnSprintEnd;
    public event Action OnShot;

    public void HandleKeysUpdate();
}