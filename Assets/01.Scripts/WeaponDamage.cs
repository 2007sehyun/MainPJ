using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private bool IsEnemy = false;
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private int damage;

    private float knockbackPower;
    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }
        //if (alreadyCollidedWith.Contains(other)) { return; }
        if (IsEnemy && other.CompareTag("Enemy")) return;

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockbackPower);
        }
    }

    public void SetAttack(int damage, float KnockbackPower)
    {
        this.knockbackPower = KnockbackPower;
        this.damage = damage;
    }
}
