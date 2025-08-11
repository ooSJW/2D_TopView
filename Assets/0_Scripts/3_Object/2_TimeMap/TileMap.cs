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
    using UnityEngine.Tilemaps;

    public partial class TileMap : MonoBehaviour // Data Field
    {
        [SerializeField] private Tilemap tilemap;
        private float tileMapSize;
        private Transform playerTransform;
    }
    public partial class TileMap : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();
            SetUp();
        }
        private void SetUp()
        {
            playerTransform = MainSystem.Instance.PlayerManager.Player.transform;
            tileMapSize = tilemap.cellSize.x * tilemap.size.x - 1;
        }
    }
    public partial class TileMap : MonoBehaviour // Private Property
    {
        private void FixedUpdate()
        {
            Vector3 playerPosition = playerTransform.position;
            Vector3 resultPosition = transform.position;
            Vector3 distance = playerPosition - resultPosition;
            if (Mathf.Abs(distance.x) > tileMapSize * 0.5f || Mathf.Abs(distance.y) > tileMapSize * 0.5f)
            {
                resultPosition.x += Mathf.Round(distance.x / tileMapSize) * tileMapSize;
                resultPosition.y += Mathf.Round(distance.y / tileMapSize) * tileMapSize;
                transform.position = resultPosition;
            }
        }
    }


}
