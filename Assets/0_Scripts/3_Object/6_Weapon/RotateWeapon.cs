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

    public partial class RotateWeapon : MonoBehaviour
    {
        private List<Weapon> rotateWeaponList;
    }
    public partial class RotateWeapon : MonoBehaviour
    {
        private void Allocate()
        {
            rotateWeaponList = new List<Weapon>();
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
    public partial class RotateWeapon : MonoBehaviour // Property
    {
        public void AddWeapon(Weapon weapon)
        {
            rotateWeaponList.Add(weapon);
            weapon.transform.SetParent(transform);
            for (int i = 0; i < rotateWeaponList.Count; i++)
            {
                Transform weaponTransform = rotateWeaponList[i].transform;
                weaponTransform.localPosition = Vector3.zero;
                weaponTransform.localRotation = Quaternion.identity;
                Vector3 rotate = Vector3.forward * 360 * i / rotateWeaponList.Count;
                weaponTransform.Rotate(rotate);
                weaponTransform.Translate(weaponTransform.up * rotateWeaponList[i].WeaponInformation.position, Space.World);
            }
        }
    }
    public partial class RotateWeapon : MonoBehaviour // Main
    {
        private void Update()
        {
            if (rotateWeaponList.Count > 0)
                transform.Rotate(Vector3.back * rotateWeaponList[0].WeaponInformation.speed * Time.deltaTime);
        }
    }
}
