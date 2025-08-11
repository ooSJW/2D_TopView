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
    using System.Linq;
    using UnityEngine;

    public partial class WeaponController : MonoBehaviour // Data Field
    {
        public List<Weapon> WeaponList { get; private set; }
        public WeaponRoot WeaponRoot { get; private set; }
    }
    public partial class WeaponController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            WeaponList = new List<Weapon>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();

        }
        private void Setup()
        {

        }
    }
    public partial class WeaponController : MonoBehaviour // Property
    {
        public void AddWeapon(WeaponPrefabName prefabName)
        {
            Weapon weapon = WeaponList.SingleOrDefault(elem => elem.PrefabName == prefabName);
            if (weapon == null)
            {
                weapon = MainSystem.Instance.PoolManager.
                    Spawn(WeaponPrefabName.Weapon.ToString(), WeaponRoot.transform).GetComponent<Weapon>();
                weapon.name = prefabName.ToString();
                SignUpWeapon(weapon, prefabName);
            }
            else
                weapon.LevelUp();
        }
    }

    public partial class WeaponController : MonoBehaviour // Sign
    {

        public void SignUpWeaponRoot(WeaponRoot weaponRoot)
        {
            WeaponRoot = weaponRoot;
            WeaponRoot.Initialize();
        }
        public void SignOutWeaponRoot()
        {
            WeaponRoot = null;
        }
        public void SignUpWeapon(Weapon weapon, WeaponPrefabName prefabName)
        {
            weapon.Initialize(prefabName);
            WeaponList.Add(weapon);
        }
        public void SignOutWeapon(Weapon weapon)
        {
            WeaponList.Remove(weapon);
        }
    }
}
