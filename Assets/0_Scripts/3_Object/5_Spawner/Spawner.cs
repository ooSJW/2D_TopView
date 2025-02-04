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
    using System.Linq;
    using UnityEngine;
    using static project02.StageData;

    public partial class Spawner : MonoBehaviour // Data Field
    {
        private Transform activeParent;
        [SerializeField] private Transform[] spawnPoint;

        private StageInformation stageInformation;
        public StageInformation StageInformation
        {
            get => stageInformation;
            private set
            {
                stageInformation = new StageInformation()
                {
                    index = value.index,
                    stageName = value.stageName,
                    imagePath = value.imagePath,
                    spawnEnemy = value.spawnEnemy,
                    spawnPercent = value.spawnPercent,
                    spawnTime = value.spawnTime,
                    gameTime = value.gameTime,
                    stageLevel = value.stageLevel,
                };
            }
        }

        private int currentStageLevel;
        public int CurrentStageLevel { get => currentStageLevel; private set => currentStageLevel = value; }

        [SerializeField] private StageIndex stageIndex;
        public StageIndex StageIndex { get => stageIndex; }

        private Transform playerTransform;
        private float gameTimer;
        private float spawnTimer;
    }
    public partial class Spawner : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            activeParent = new GameObject() { name = "Root_Active" }.transform;
            StageInformation = MainSystem.Instance.DataManager.StageData.GetData(((int)stageIndex).ToString());
            playerTransform = MainSystem.Instance.PlayerManager.Player.transform;
            GetSpawnPoint();
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
    public partial class Spawner : MonoBehaviour // Main
    {
        private void Update()
        {
            transform.position = playerTransform.position;

            spawnTimer += Time.deltaTime;
            gameTimer = MainSystem.Instance.GameManager.GameController.SurvivalTime;

            if (!MainSystem.Instance.GameManager.GameController.IsClear
                && MainSystem.Instance.PlayerManager.Player.PlayerState != PlayerState.Death)
            {
                if (gameTimer >= StageInformation.gameTime[CurrentStageLevel])
                    StageLevelUp();

                if (spawnTimer >= StageInformation.spawnTime[CurrentStageLevel])
                {
                    SpawnEnemy();
                    spawnTimer = 0;
                }
            }
        }
    }
    public partial class Spawner : MonoBehaviour // Private Property
    {
        private void GetSpawnPoint()
        {
            Transform[] pointTemp = GetComponentsInChildren<Transform>();
            spawnPoint = pointTemp.Where(point => point != transform).ToArray();
        }

        private void StageLevelUp()
        {
            if (CurrentStageLevel + 1 < StageInformation.stageLevel.Length)
                CurrentStageLevel++;
            else
                MainSystem.Instance.GameManager.GameController.IsClear = true;
        }

        private void SpawnEnemy()
        {
            float randomEnemy = UnityEngine.Random.Range(0, 1f);
            string enemyName = string.Empty;

            for (int i = 0; i < StageInformation.spawnEnemy[CurrentStageLevel].Length; i++)
            {

                float spawnPercent = StageInformation.spawnPercent[CurrentStageLevel][i];
                if (randomEnemy <= spawnPercent)
                {
                    enemyName = StageInformation.spawnEnemy[CurrentStageLevel][i];
                    break;
                }
            }
            int randomPoint = UnityEngine.Random.Range(0, spawnPoint.Length);
            Vector2 randomPosition = spawnPoint[randomPoint].position;

            Enemy enemy = MainSystem.Instance.PoolManager.Spawn(enemyName, activeParent, randomPosition).GetComponent<Enemy>();
            MainSystem.Instance.EnemyManager.SignUpEnemy(enemy);
        }
    }
}
