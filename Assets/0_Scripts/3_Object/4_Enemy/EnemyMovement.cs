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

    public partial class EnemyMovement : MonoBehaviour // Data Field
    {
        private Enemy enemy;
        private float moveSpeed;
        [SerializeField] private Rigidbody2D rigid;
    }
    public partial class EnemyMovement : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            moveSpeed = enemy.EnemyStatInformation.moveSpeed;
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
    public partial class EnemyMovement : MonoBehaviour // Progress
    {
        public void FixedProgress()
        {
            if (enemy.EnemyState == EnemyState.Move)
            {
                rigid.linearVelocity = Vector2.zero;
                Vector2 direction = (enemy.PlayerTransform.position - transform.position).normalized;
                Vector2 resultPosition = direction * moveSpeed * Time.fixedDeltaTime;
                rigid.MovePosition(rigid.position + resultPosition);
            }

        }

    }

    public partial class EnemyMovement : MonoBehaviour // Property
    {
        public void KnockBack()
        {
            rigid.linearVelocity = Vector2.zero;
            enemy.EnemyState = EnemyState.Hit;
            Vector3 playerPos = MainSystem.Instance.PlayerManager.Player.transform.position;
            Vector3 direction = (transform.position - playerPos).normalized;
            rigid.AddForce(direction * 0.3f, ForceMode2D.Impulse);
        }
    }
}
