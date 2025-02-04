/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public partial class WeaponData  // Information
    {
        [System.Serializable]
        public class WeaponInformation : BaseInformation
        {
            public string prefabName;
            public string weaponType;

            public int maxLevel;
            public int[] count;
            public int[] bounceCount;
            public int[] damage;

            public float[] attackDelay;
            public float position;
            public float speed;
        }
    }
    public partial class WeaponData // Data Field
    {
        private Dictionary<string, WeaponInformation> weaponInfoDict = default;
    }

    public partial class WeaponData // Initialize
    {
        private void Allocate()
        {
            weaponInfoDict = new Dictionary<string, WeaponInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<WeaponInformation>(weaponInfoDict, "WeaponData");
        }
    }
    public partial class WeaponData // Property
    {
        public WeaponInformation GetData(string index)
        {
            return weaponInfoDict[index];
        }
        public WeaponInformation GetData(WeaponPrefabName weaponName)
        {
            WeaponInformation weaponInformation = weaponInfoDict.Values.
                FirstOrDefault(weaponInfo => weaponInfo.prefabName == weaponName.ToString());

            return weaponInformation;
        }
    }
}
