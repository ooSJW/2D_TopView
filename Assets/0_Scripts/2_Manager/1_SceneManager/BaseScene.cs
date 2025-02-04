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

    public partial class BaseScene : MonoBehaviour // Data Field
    {
        [Header("Inheritance Member")]
        public List<GameObject> poolableObjectList = new List<GameObject>();
        [field: SerializeField] public Spawner Spawner { get; private set; }
        [field: SerializeField] public TileMap[] TileMaps { get; private set; }
        [field: SerializeField] public UIContoller UIContoller { get; private set; }
        [field: SerializeField] public Player Player { get; protected set; }
        [field: SerializeField] public WeaponController WeaponController { get; private set; }
        [field: SerializeField] public WeaponRoot WeaponRoot { get; protected set; }
        [field: SerializeField] public ItemController ItemController { get; private set; }
        [field: SerializeField] public GameController GameController { get; private set; }
        [field: SerializeField] public SoundController SoundController { get; private set; }
        [field: SerializeField] public VirtualCamara VirtualCamara { get; private set; }
    }
    public partial class BaseScene : MonoBehaviour // Intialzie 
    {
        private void Allocate()
        {

        }
        public virtual void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            //MainSystem.Instance.PoolManager.Register();
            //MainSystem.Instance.UIManager.SignUpUIController(UIContoller);
            //MainSystem.Instance.PlayerManager.SignUpPlayer(Player);
            //Spawner.Initialize();
            //MainSystem.Instance.TileMapManager.SignUpTileMap(TileMaps);
            //MainSystem.Instance.WeaponManager.SignUpWeaponController(WeaponController);
            //MainSystem.Instance.WeaponManager.WeaponController.SignUpWeaponRoot(WeaponRoot);
            //MainSystem.Instance.ItemManager.SignUpItemController(ItemController);
        }
    }

    public partial class BaseScene : MonoBehaviour // Main 
    {
        private void Awake()
        {
            MainSystem.Instance.SceneManager.SignUpActiveScene(this);
        }
    }
}
