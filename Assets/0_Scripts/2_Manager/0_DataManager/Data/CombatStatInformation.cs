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

    [System.Serializable]
    public partial class CombatStatInformation : BaseInformation
    {
        public string name;

        public float moveSpeed;

        public int maxHp;
        public int attackPower;

        public float criticalPercent;
        public float criticalIncreasePercent;
    }
}
