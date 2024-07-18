using System;
using UnityEngine;
using Zenject;

public class CameraHandler : MonoBehaviour
{
    private Camera cam;
    private IInputService _inputService;

    private float sensitivity = 1f;

    private float rotationX;
    private float rotationY;

    private bool inSprint = false;

    [Inject]
    public void Construct(IInputService inputService, PlayerSettings settings)
    {
        _inputService = inputService;
        sensitivity = settings.MouseSensitivity;

        cam = GetComponentInChildren<Camera>();

        _inputService.OnSprintStart += () => inSprint = true;
        _inputService.OnSprintEnd += () => inSprint = false;
    }

    void Update()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, inSprint ? 75 : 60, 10 * Time.deltaTime);
        
        rotationY += _inputService.RotateDirection.x * sensitivity;
        rotationX -= _inputService.RotateDirection.y * sensitivity;

        rotationX = Mathf.Clamp(rotationX, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}