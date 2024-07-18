using UnityEngine;

public class SomeTarget : MonoBehaviour, IDamagable
{
    private int HP = 100;

    public void GetDamage(int amount)
    {
        if (HP - amount <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            HP -= amount;
        }
    }
}