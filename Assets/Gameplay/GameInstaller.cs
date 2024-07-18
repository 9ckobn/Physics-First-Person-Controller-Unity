using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Joystick joystickPrefab;

    public override void InstallBindings()
    {
        Application.targetFrameRate = 60;

        BindNecessaryServices();
    }

    private void BindNecessaryServices()
    {
        Container.Bind<IInputService>().FromMethod(GetInputService()).AsSingle();
    }

    Func<IInputService> GetInputService()
    {
        Dictionary<RuntimePlatform, Func<IInputService>> inputMap = new Dictionary<RuntimePlatform, Func<IInputService>>()
        {
            { RuntimePlatform.Android, ()=> new AndroidInputService(joystickPrefab)},
            { RuntimePlatform.WindowsPlayer, ()=> new DesktopInputService()},
            { RuntimePlatform.OSXPlayer, ()=> new DesktopInputService()},
            { RuntimePlatform.WindowsEditor, ()=> new DesktopInputService()},
        };

        if (inputMap.TryGetValue(Application.platform, out var inputService))
        {
            return inputService;
        }
        else
        {
            throw new Exception("Platform missmatch, could'not setup input service");
        }
    }
}