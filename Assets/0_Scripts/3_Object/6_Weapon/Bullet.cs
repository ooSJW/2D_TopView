using project02;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Bullet : MonoBehaviour // Data Field
{
    private Rigidbody2D rigid;
    private Weapon weapon;
    private Vector2 direction;
    private Transform lastHitEnemy;

    private bool isProjectile = false;
    private int damage;
    private int bounceCount;
}

public partial class Bullet : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Initialize(Weapon weaponValue, Vector3 directionValue = default)
    {
        weapon = weaponValue;
        direction = directionValue;
        Allocate();
        Setup();
        MoveBullet();
        if (isProjectile)
            MainSystem.Instance.SoundManager.SoundController.SoundEffect.PlaySfx(AudioClipName.ThrowDagger);
    }
    private void Setup()
    {
        SetDamage();
        bounceCount = weapon.WeaponInformation.bounceCount[weapon.Level];

        if (weapon.WeaponInformation.weaponType == WeaponType.Projectile.ToString())
            isProjectile = true;
        else
            isProjectile = false;
    }
}

public partial class Bullet : MonoBehaviour // Property
{
    private void IsProjectile(string weaponType)
    {
        if (weaponType == WeaponType.Projectile.ToString())
            isProjectile = true;
        else
            isProjectile = false;
    }
    private void MoveBullet()
    {
        if (isProjectile)
            rigid.linearVelocity = direction * weapon.WeaponInformation.speed;
    }
    private void SetNewTarget()
    {
        float scanRange = 50;
        float distance = scanRange;
        LayerMask enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        RaycastHit2D[] targetArray = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, enemyLayer);
        if (targetArray != null)
        {
            for (int i = 0; i < targetArray.Length; i++)
            {
                if (lastHitEnemy == targetArray[i].transform) continue;

                Vector2 position = transform.position;
                Vector2 targetPosition = targetArray[i].transform.position;
                float currentDistance = Vector2.Distance(position, targetPosition);
                if (distance > currentDistance)
                {
                    distance = currentDistance;
                    direction = (targetPosition - position).normalized;
                }
            }
        }
    }
    private void BounceBullet()
    {
        if (bounceCount > 0)
        {
            SetNewTarget();
            MoveBullet();
            bounceCount--;
        }
        else
            MainSystem.Instance.PoolManager.Despawn(gameObject);
    }
    public void SetDamage()
    {
        damage = weapon.WeaponInformation.damage[weapon.Level];
    }
}

public partial class Bullet : MonoBehaviour // TriggerEvent
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CombatObjectBase enemy = collision.GetComponent<CombatObjectBase>();
            weapon.Owner.PlayerCombat.Attack(enemy, damage);
            if (isProjectile)
            {
                lastHitEnemy = collision.transform;
                BounceBullet();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
            MainSystem.Instance.PoolManager.Despawn(gameObject);
    }
}
