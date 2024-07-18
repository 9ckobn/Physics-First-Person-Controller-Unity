using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public event Action<Bullet> onDestroy;

    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        onDestroy += (x) => ResetBulletPhysic();
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(SelfDestroy());

        _rb.isKinematic = false;
        _rb.AddForce(transform.forward * 100, ForceMode.Impulse);
    }

    private void ResetBulletPhysic()
    {
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(5);
        onDestroy?.Invoke(this);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent<IDamagable>(out var target))
        {
            target.GetDamage(10);
            onDestroy?.Invoke(this);
        }
    }
}