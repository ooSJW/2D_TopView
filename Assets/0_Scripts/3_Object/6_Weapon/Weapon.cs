/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static project02.WeaponData;

    public partial class Weapon : MonoBehaviour // Data Field
    {
        private int damage;
        private WeaponType weaponType;
        private Action action = null;

        private float intevalTime;
        private float delayTime;
    }

    public partial class Weapon : MonoBehaviour // Data Property
    {
        private Player owner;
        public Player Owner { get => owner; private set => owner = value; }

        private WeaponPrefabName prefabName;
        public WeaponPrefabName PrefabName { get => prefabName; private set => prefabName = value; }

        private WeaponInformation weaponInformation;
        public WeaponInformation WeaponInformation
        {
            get => weaponInformation;
            private set
            {
                weaponInformation = new WeaponInformation()
                {
                    index = value.index,
                    prefabName = value.prefabName,
                    weaponType = value.weaponType,
                    maxLevel = value.maxLevel,
                    damage = value.damage,
                    count = value.count,
                    bounceCount = value.bounceCount,
                    attackDelay = value.attackDelay,
                    speed = value.speed,
                    position = value.position,
                };
                damage = value.damage[Level];
            }
        }

        private int level;
        public int Level
        {
            get => level;
            private set
            {
                if (value > 0 && value <= weaponInformation.maxLevel)
                {
                    if (weaponInformation.maxLevel >= value)
                    {
                        level = value;
                        RefreshWeapon();
                    }
                }
            }
        }
    }

    public partial class Weapon : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            int index = (int)prefabName;
            // WeaponInformation = MainSystem.Instance.DataManager.WeaponData.GetData(index.ToString());
            WeaponInformation = MainSystem.Instance.DataManager.WeaponData.GetData(PrefabName);
            weaponType = Enum.Parse<WeaponType>(weaponInformation.weaponType);
            owner = MainSystem.Instance.PlayerManager.Player;
            Level = 1;
        }
        public void Initialize(WeaponPrefabName prefabNameValue)
        {
            prefabName = prefabNameValue;
            Allocate();
            Setup();
        }
        private void Setup()
        {
            switch (weaponType)
            {
                case WeaponType.Rotate:
                    action += RotateWeapon;
                    break;
                case WeaponType.Projectile:
                    delayTime = weaponInformation.attackDelay[Level];
                    action += ShootBullet;
                    break;
            }
        }
    }

    public partial class Weapon : MonoBehaviour // Main
    {
        private void Update()
        {
            if (Owner.PlayerState == PlayerState.Death)
                gameObject.SetActive(false);
            action?.Invoke();
        }
    }
    public partial class Weapon : MonoBehaviour // Private Property
    {
        private void RotateWeapon()
        {
            transform.Rotate(Vector3.back * WeaponInformation.speed * Time.deltaTime);
        }

        private void ShootBullet() // Projectile
        {
            intevalTime += Time.deltaTime;
            if (intevalTime >= delayTime)
            {
                Bullet bullet = MainSystem.Instance.PoolManager.
                    Spawn(weaponInformation.prefabName, transform, transform.position).GetComponent<Bullet>();

                Transform target = Owner.Target;
                Vector2 direction = Vector2.up;
                if (target != null)
                    direction = (target.position - transform.position).normalized;

                bullet.Initialize(this, direction);
                intevalTime = 0;
            }
        }

        private void RefreshRotateWeapon()
        {
            for (int i = 0; i < weaponInformation.count[Level]; i++)
            {
                if (i < transform.childCount)
                {
                    Bullet bullet = transform.GetChild(i).GetComponent<Bullet>();
                    bullet.SetDamage();
                }
                else
                {
                    Bullet bullet = MainSystem.Instance.PoolManager.
                        Spawn(WeaponInformation.prefabName, transform).GetComponent<Bullet>();
                    bullet.Initialize(this);
                }
            }
            for (int i = 0; i < weaponInformation.count[Level]; i++)
            {
                Transform bulletTransform = transform.GetChild(i);

                bulletTransform.localPosition = Vector3.zero;
                bulletTransform.localRotation = Quaternion.identity;

                Vector3 rotate = Vector3.forward * 360 * i / transform.childCount;

                bulletTransform.Rotate(rotate);
                bulletTransform.Translate(bulletTransform.up * WeaponInformation.position, Space.World);
            }
        }
        private void RefreshProjecileWeapon()
        {
            delayTime = weaponInformation.attackDelay[Level];
        }
        private void RefreshWeapon()
        {
            switch (weaponType)
            {
                case WeaponType.Rotate:
                    RefreshRotateWeapon();
                    break;
                case WeaponType.Projectile:
                    RefreshProjecileWeapon();
                    break;
            }
            transform.localPosition = Vector3.zero;
        }
    }
    public partial class Weapon : MonoBehaviour // Property
    {
        public void LevelUp()
        {
            Level++;
        }
    }
}
