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
    public partial class CombatObjectBase : MonoBehaviour // Data Field
    {
        protected int hp;
        public virtual int HP { get => hp; set => hp = value; }

        protected int totalDamage;
        protected float damageIncreasePercent;
    }

    public partial class CombatObjectBase : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public virtual void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class CombatObjectBase : MonoBehaviour // Property
    {
        public virtual void CalculateDamage(CombatStatInformation statInfo, float incteasePercent = 0)
        {
            totalDamage = statInfo.attackPower;
            damageIncreasePercent += incteasePercent;
        }

        public virtual void SendDamage(CombatObjectBase target, CombatStatInformation sender, float weaponDamage = 0)
        {
            float resultDamage = totalDamage;
            bool isCritial = false;

            resultDamage += weaponDamage;

            if (UnityEngine.Random.Range(0, 1f) <= sender.criticalPercent)
            {
                isCritial = true;
                resultDamage += resultDamage * sender.criticalIncreasePercent;
            }
            resultDamage += resultDamage * damageIncreasePercent;
            target.ReceiveDamage((int)resultDamage, isCritial);
        }
        public virtual void ReceiveDamage(int damage, bool isCritical = false)
        {
            if (HP <= 0)
                return;

            HP -= damage;
            if (MainSystem.Instance.UIManager.DamageTextActive)
            {
                DamageText damageText = MainSystem.Instance.PoolManager.Spawn("DamageText", null, default, true).GetComponent<DamageText>();
                damageText.Initialize(transform, damage, isCritical);
            }
        }
    }
}
