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
        private readonly float tileMapSize = 30f;
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
        }
    }
    public partial class TileMap : MonoBehaviour // Private Property
    {
        private void FixedUpdate()
        {
            Vector3 playerPosition = playerTransform.position;
            Vector3 resultPosition = transform.position;
            Vector3 distance = playerPosition - resultPosition;

            resultPosition.x += Mathf.Round(distance.x / tileMapSize) * tileMapSize;
            resultPosition.y += Mathf.Round(distance.y / tileMapSize) * tileMapSize;
            transform.position = resultPosition;
        }
    }


}
