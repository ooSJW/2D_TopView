/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    public enum SceneName
    {
        InitializeScene,
        LoadingScene,
        LobbyScene,
        Stage01Scene,
        Stage02Scene,
        StageTestScene
    }
    public enum StageIndex
    {
        Stage01,
        Stage02,
    }
    public enum SaveDataName
    {
        PlayerGold,
    }
    #region CombatObject
    public enum PlayerState
    {
        Idle,
        Move,
        Death,
    }
    public enum EnemyState
    {
        Move,
        Hit,
        Death,
    }
    public enum PlayerName
    {
        Knight,
        Ethan,
    }
    public enum EnemyName
    {
        Enemy01,
        Enemy02,
        Enemy03,
        Enemy04,
        Enemy05,
        EnemyLast,
    }
    #endregion

    #region Weapon
    public enum WeaponPrefabName
    {
        Stone,
        Dagger,
        Weapon,
    }
    public enum WeaponType
    {
        Rotate,
        Projectile,
    }
    #endregion

    #region Item
    public enum ItemType
    {
        Weapon,
        Shoes,
        Armor,
        Glove,
        Potion,
        Magnet,
        Exp,
    }
    public enum LootType
    {
        Select,
        Drop,
    }
    public enum ItemName
    {
        Stone,
        Dagger,
        Shoes,
        Armor,
        Glove,
        Potion,
        Magnet,
        Exp0,
        Exp1,
        Exp2,
    }
    #endregion

    #region Sound
    public enum PlayerPrefsName
    {
        IsBgmPlay,
        IsSfxPlay,
    }
    public enum AudioClipName
    {
        Click,
        ThrowDagger,
        EnemyHit,
        EnemyDeath,
        GetExp,
        GetPotion,
        GetMagnet,
        Explosion,
        StageClear,
        Gameover,
    }
    #endregion
}

