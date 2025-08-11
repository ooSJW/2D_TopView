/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class DataManager : MonoBehaviour // Data Field
    {
        public StageData StageData { get; private set; } = default;
        public WeaponData WeaponData { get; private set; } = default;
        public PlayerStatData PlayerStatData { get; private set; } = default;
        public EnemyStatData EnemyStatData { get; private set; } = default;
        public ItemData ItemData { get; private set; } = default;
    }
    public partial class DataManager : MonoBehaviour
    {
        private void Allocate()
        {
            StageData = new StageData();
            WeaponData = new WeaponData();
            PlayerStatData = new PlayerStatData();
            EnemyStatData = new EnemyStatData();
            ItemData = new ItemData();
        }
        public void Initialize()
        {
            Allocate();
            Setup();

            StageData.Initialize();
            WeaponData.Initialize();
            PlayerStatData.Initialize();
            EnemyStatData.Initialize();
            ItemData.Initialize();
        }
        private void Setup()
        {

        }
    }
    public partial class DataManager : MonoBehaviour // Property
    {


        #region Json
        private Wrapper<T> LoadJson<T>(string path) where T : BaseInformation
        {
            string jsonStringData = Resources.Load<TextAsset>(path).ToString();
            return JsonConvert.DeserializeObject<Wrapper<T>>(jsonStringData);
        }

        public void SetUpData<T>(Dictionary<string, T> dataDict, string path) where T : BaseInformation
        {
            dataDict.Clear();
            Wrapper<T> jsonData = LoadJson<T>(path);
            foreach (T data in jsonData.array)
            {
                dataDict.Add(data.index, data);
            }
        }
        #endregion


        public void SaveData()
        {
            int gold = MainSystem.Instance.GameManager.Gold;
            PlayerPrefs.SetInt(SaveDataName.PlayerGold.ToString(), gold);
            PlayerPrefs.Save();
        }
        public void SaveClearStage(StageIndex stageIndexValue)
        {
            string stageIndex = stageIndexValue.ToString();

            PlayerPrefs.SetInt(stageIndex, 1);
            PlayerPrefs.Save();
        }
        public bool IsClearStage(StageIndex stageIndexValue)
        {
            if (PlayerPrefs.GetInt(stageIndexValue.ToString(), 0) == 0)
                return false;
            else
                return true;
        }
        public string SaveLoadSelecteCharacter(PlayerName? playerNameValue = null)
        {
            if (playerNameValue is not null)
            {
                PlayerPrefs.SetString("SelectedCharacter", playerNameValue.ToString());
                PlayerPrefs.Save();
                return playerNameValue.ToString();
            }
            // 초기 실행 예외처리
            return PlayerPrefs.GetString("SelectedCharacter", PlayerName.Knight.ToString());
        }

        public void SaveUnrockCharacter(PlayerName playerNameValue)
        {
            string playerName = playerNameValue.ToString();

            PlayerPrefs.SetInt(playerName, 1);
            PlayerPrefs.Save();
        }
        public bool IsCharacterUnrock(PlayerName playerName)
        {
            if (PlayerPrefs.GetInt(playerName.ToString(), 0) == 0)
                return false;
            else
                return true;
        }
        public int LoadPlayerGold()
        {
            return PlayerPrefs.GetInt(SaveDataName.PlayerGold.ToString(), 0);
        }

        public void DeleteData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
