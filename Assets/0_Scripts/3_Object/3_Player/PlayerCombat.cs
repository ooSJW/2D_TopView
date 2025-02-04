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

    public partial class PlayerCombat : MonoBehaviour // Data Field
    {
        private Player player;
    }
    public partial class PlayerCombat : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize(Player playerValue)
        {
            player = playerValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class PlayerCombat : MonoBehaviour // Property
    {
        public void Attack(CombatObjectBase target, int weaponDamage = 0)
        {
            player.SendDamage(target, player.PlayerStatInformation, weaponDamage);
        }
    }
}
