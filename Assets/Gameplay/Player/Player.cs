using UnityEngine;
using Zenject;

[RequireComponent(typeof(PhysicBodyMovement), typeof(CameraHandler))]
public class Player : MonoBehaviour
{
    private IInputService _inputService;

    [SerializeField] private Transform weaponOrigin;

    [Inject] private PlayerSettings playerSettings;

    private IShotable currentWeapon;


    [Inject]
    private void Construct(IInputService inputService)
    {
        _inputService = inputService;

        currentWeapon = playerSettings.MP5K.GetWeapon(weaponOrigin);

        _inputService.OnShot += currentWeapon.Shot;
    }

    void Update()
    {
        _inputService.HandleKeysUpdate();
    }

}