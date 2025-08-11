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
    using UnityEngine.EventSystems;

    public partial class PlayerInput : MonoBehaviour  // Data Field
    {
        private Player player;
    }
    public partial class PlayerInput : MonoBehaviour // Intialize
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
    public partial class PlayerInput : MonoBehaviour // Progress
    {
        public void Progress()
        {
            if (player.Moveable)
            {
                GetInputValue();
                if (player.InputVector.magnitude == 0)
                    player.PlayerState = PlayerState.Idle;
                else
                    player.PlayerState = PlayerState.Move;
            }
        }
    }
    public partial class PlayerInput : MonoBehaviour // Property
    {
        public void GetInputValue()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 inputValue = new Vector2(horizontal, vertical);
            player.InputVector = inputValue;
        }
    }

}
