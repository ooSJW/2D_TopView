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

    public partial class EnemyManager : MonoBehaviour // Data Field
    {
        public List<Enemy> AllFieldEnemyList { get; private set; }
    }
    public partial class EnemyManager : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            AllFieldEnemyList = new List<Enemy>();
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
    public partial class EnemyManager : MonoBehaviour // Sign
    {
        public void SignUpEnemy(Enemy enemy)
        {
            enemy.Initialize();
            AllFieldEnemyList.Add(enemy);
        }
        public void SignOutEnemy(Enemy enemy)
        {
            AllFieldEnemyList.Remove(enemy);
        }
    }
}
