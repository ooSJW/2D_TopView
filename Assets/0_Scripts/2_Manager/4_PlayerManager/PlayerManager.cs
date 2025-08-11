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
                    // 게임 진행 중 캐릭터가 바뀌었을 때 이름 저장
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
            // 게임 시작 후 매니저 클래스 초기화 시 저장된 정보 받아오기
            // 게임 진행 중 캐릭터가 바뀐 경우 프로퍼티로 처리
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
        public void SignOutPlayer()
        {
            Player = null;
        }
    }
}
