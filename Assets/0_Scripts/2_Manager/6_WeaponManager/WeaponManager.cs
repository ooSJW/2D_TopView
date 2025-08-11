/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class WeaponManager : MonoBehaviour // Data Field
    {
        public WeaponController WeaponController { get; private set; }
    }
    public partial class WeaponManager : MonoBehaviour
    {
        private void Allocate()
        {

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
    public partial class WeaponManager : MonoBehaviour // Sign
    {
        public void SignUpWeaponController(WeaponController weaponController)
        {
            WeaponController= weaponController;
            WeaponController.Initialize();
        }
        public void SignOutWeaponController()
        {
            WeaponController = null;
        }
    }
}
