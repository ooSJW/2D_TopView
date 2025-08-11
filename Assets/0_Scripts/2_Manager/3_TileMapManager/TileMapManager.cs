/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public partial class TileMapManager : MonoBehaviour // Data Field
    {
        public TileMap[] TileMaps { get; private set; } = default;
    }
    public partial class TileMapManager : MonoBehaviour // Initialize
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

        }
    }
    public partial class TileMapManager : MonoBehaviour // Sign
    {
        public void SignUpTileMap(TileMap[] tileMapsValue)
        {
            TileMaps = tileMapsValue;
            for (int i = 0; i < TileMaps.Length; i++)
                TileMaps[i].Initialize();
        }
        public void SignOutTileMap()
        {
            TileMaps = null;
        }
    }
}
