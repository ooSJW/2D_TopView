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
    using UnityEngine.SceneManagement;

    public partial class MainSystem : GenericSingleton<MainSystem> // Data Field
    {
        public DataManager DataManager { get; private set; } = default;
        public SceneManager SceneManager { get; private set; } = default;
        public PoolManager PoolManager { get; private set; } = default;
        public TileMapManager TileMapManager { get; private set; } = default;
        public PlayerManager PlayerManager { get; private set; } = default;
        public StageManager StageManager { get; private set; } = default;
        public EnemyManager EnemyManager { get; private set; } = default;
        public WeaponManager WeaponManager { get; private set; } = default;
        public UIManager UIManager { get; private set; } = default;
        public ItemManager ItemManager { get; private set; } = default;
        public GameManager GameManager { get; private set; } = default;
        public SoundManager SoundManager { get; private set; } = default;
    }
    public partial class MainSystem : GenericSingleton<MainSystem> // Initialize
    {
        private void Allocate()
        {
            DataManager = gameObject.AddComponent<DataManager>();
            SceneManager = gameObject.AddComponent<SceneManager>();
            PoolManager = gameObject.AddComponent<PoolManager>();
            TileMapManager = gameObject.AddComponent<TileMapManager>();
            PlayerManager = gameObject.AddComponent<PlayerManager>();
            StageManager = gameObject.AddComponent<StageManager>();
            EnemyManager = gameObject.AddComponent<EnemyManager>();
            WeaponManager = gameObject.AddComponent<WeaponManager>();
            UIManager = gameObject.AddComponent<UIManager>();
            ItemManager = gameObject.AddComponent<ItemManager>();
            GameManager = gameObject.AddComponent<GameManager>();
            SoundManager = gameObject.AddComponent<SoundManager>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();

            DataManager.Initialize();
            SceneManager.Initialize();
            PoolManager.Initialize();
            TileMapManager.Initialize();
            PlayerManager.Initialize();
            StageManager.Initialize();
            EnemyManager.Initialize();
            WeaponManager.Initialize();
            UIManager.Initialize();
            ItemManager.Initialize();
            GameManager.Initialize();
            SoundManager.Initialize();
        }
        private void Setup()
        {

        }
    }
    public partial class MainSystem : GenericSingleton<MainSystem> // Property
    {
        public void MainSystemStart()
        {
            Initialize();
            SceneManager.LoadScene(SceneName.LobbyScene);
        }
    }
}
