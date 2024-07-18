using UnityEngine;

[CreateAssetMenu(menuName = "GameDevClub/WeaponSettings")]
public class WeaponSettingsSO : ScriptableObject
{
    public Weapon WeaponPrefab;
    public Bullet BulletPrefab;
    public int BulletCount;
    public int BPM;

    public IShotable GetWeapon(Transform origin)
    {
        var currentWeapon = Instantiate(WeaponPrefab, origin);
        currentWeapon.SetupWeapon(BulletPrefab, BulletCount, BPM);

        return currentWeapon;
    }
}