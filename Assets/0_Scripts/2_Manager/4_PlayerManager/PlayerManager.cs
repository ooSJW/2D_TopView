/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerManager : MonoBehaviour // Data Field
    {
        public Player Player { get; private set; } = default;
        private PlayerName playerName;
        public PlayerName PlayerName
        {
            get => playerName;
            private set
            {
                if (playerName != value)
                {
                    playerName = value;
                    MainSystem.Instance.DataManager.SaveLoadSelecteCharacter(playerName);
                }
            }
        }
    }
    public partial class PlayerManager : MonoBehaviour // Initialize
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
            playerName = Enum.Parse<PlayerName>(MainSystem.Instance.DataManager.SaveLoadSelecteCharacter());
        }
    }
    public partial class PlayerManager : MonoBehaviour // Sign
    {
        public void SignUpPlayerName(PlayerName playerNameValue)
        {
            PlayerName = playerNameValue;
        }
        public void SignUpPlayer(Player playerValue)
        {
            Player = playerValue;
            Player.Initialize();
        }
        public void SignDownPlayer()
        {
            Player = null;
        }
    }
}
