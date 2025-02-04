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

    public partial class PlayerMovement : MonoBehaviour // Data Field
    {
        private Player player;

        [SerializeField] private Rigidbody2D rigid;

        private float moveSpeed;
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    }
    public partial class PlayerMovement : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            MoveSpeed = player.PlayerStatInformation.moveSpeed;
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
    public partial class PlayerMovement : MonoBehaviour // Property
    {
        public void Move()
        {
            Vector2 movelVector = player.InputVector * moveSpeed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + movelVector);
        }
    }
}
