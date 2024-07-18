using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public Player PlayerPrefab;
    public PlayerSettingsSO PlayerSettings;
    public Transform PlayerSpawnPoint;

    private Player _playerInstance;

    public override void InstallBindings()
    {
        BindPlayerSettings();

        BindPlayer();

        BindPlayerControls();
    }

    private void BindPlayerSettings()
    {
        Container.BindInstance(PlayerSettings.Settings).AsSingle();
    }

    private void BindPlayer()
    {
        _playerInstance = Container.InstantiatePrefabForComponent<Player>(PlayerPrefab, PlayerSpawnPoint.position, PlayerSpawnPoint.rotation, null);
        Container.Bind<Player>().FromInstance(_playerInstance);
    }

    private void BindPlayerControls()
    {
        //Here may be some any IMovable move system
        Container.Bind<PhysicBodyMovement>().FromComponentOn(_playerInstance.gameObject).AsSingle();
        Container.Bind<CameraHandler>().FromComponentOn(_playerInstance.gameObject).AsSingle();
    }

    ~PlayerInstaller()
    {
        Debug.Log("[LOG] Installer Done!<color=green> Player ready to play </color>");
    }
}