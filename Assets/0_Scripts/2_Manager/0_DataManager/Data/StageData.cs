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

    public partial class StageData // Information
    {
        [System.Serializable]
        public class StageInformation : BaseInformation
        {
            public string[][] spawnEnemy;
            public string imagePath;
            public string stageName;
            public float[][] spawnPercent;
            public float[] spawnTime;
            public int[] stageLevel;
            public int[] gameTime;
        }
    }

    public partial class StageData // Data Field
    {
        private Dictionary<string, StageInformation> stageInfoDict = default;
    }

    public partial class StageData  // Initialize
    {
        private void Allocate()
        {
            stageInfoDict = new Dictionary<string, StageInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<StageInformation>(stageInfoDict, "StageData");
        }
    }

    public partial class StageData  // Property
    {
        public StageInformation GetData(string index)
        {
            if (stageInfoDict.ContainsKey(index))
                return stageInfoDict[index];
            else return null;
        }
        public int GetStageInformationCount()
        {
            return stageInfoDict.Count;
        }
    }
}
