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

    public partial class EnemyAnimation : MonoBehaviour // Data Field
    {
        private Enemy enemy;
        [SerializeField] private Animator animator;
    }
    public partial class EnemyAnimation : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize(Enemy enemyValue)
        {
            enemy = enemyValue;
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }
    public partial class EnemyAnimation : MonoBehaviour // Property
    {
        public void GetDamage()
        {
            animator.SetTrigger(EnemyState.Hit.ToString());
        }
        public void ReturnState()
        {
            enemy.EnemyState = EnemyState.Move;
        }
    }
    public partial class EnemyAnimation : MonoBehaviour // Progress
    {
        public void LateProgress()
        {
            bool isFlip = enemy.PlayerTransform.position.x > transform.position.x ? true : false;
            float rotation = isFlip ? 180 : 0;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}
