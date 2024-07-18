using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameDevClub/PlayerSettings")]
public class PlayerSettingsSO : ScriptableObject
{
    [field: SerializeField] public PlayerSettings Settings { get; private set; }
}

[Serializable]
public struct PlayerSettings
{
    public float GravityScale;
    public float Speed;
    public float JumpHeight;
    public float PlayerMass;
    public float DefaultDrag;
    public float MouseSensitivity;

    public WeaponSettingsSO MP5K;
}