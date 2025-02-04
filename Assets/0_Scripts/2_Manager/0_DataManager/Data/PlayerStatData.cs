/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Unity.VisualScripting.FullSerializer;
    using UnityEngine;

    public partial class PlayerStatData // Information
    {
        [System.Serializable]
        public class PlayerStatInformation : CombatStatInformation
        {
            public int level;
            public int maxExp;
            public int cost;

            public float maxExpIncreasePercent;

            public string iconPath;
            public string characterDescription;
        }
    }

    public partial class PlayerStatData // Data Field
    {
        private Dictionary<string, PlayerStatInformation> playerStatInfoDict = default;
    }

    public partial class PlayerStatData // Initialize
    {
        private void Allocate()
        {
            playerStatInfoDict = new Dictionary<string, PlayerStatInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<PlayerStatInformation>(playerStatInfoDict, "PlayerStatData");
        }
    }

    public partial class PlayerStatData // Property
    {
        public PlayerStatInformation GetData(string index)
        {
            return playerStatInfoDict[index];
        }
        public PlayerStatInformation GetData(PlayerName playerName)
        {
            PlayerStatInformation playerInformation = playerStatInfoDict.Values.
                FirstOrDefault(playerInfo => playerInfo.name == playerName.ToString());

            return playerInformation;
        }
        public int GetPlayerStatInformationCount()
        {
            return playerStatInfoDict.Count;
        }
    }
}
