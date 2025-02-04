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

    public partial class PoolManager : MonoBehaviour // Inner CLass
    {
        public class Pool
        {
            private Transform parent;
            private GameObject originalPrefab;
            private List<GameObject> poolingObjectList;
            private int spawnCount;
            private Transform canvasTransform;
            public Pool(GameObject originalPrefabValue, int spawnCountValue = 1)
            {
                poolingObjectList = new List<GameObject>();
                originalPrefab = originalPrefabValue;
                parent = new GameObject() { name = $"Root_{originalPrefab.name}" }.transform;
                spawnCount = spawnCountValue;
                canvasTransform = FindObjectOfType<Canvas>().transform;
            }

            public void Register()
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    GameObject poolObject = Instantiate(originalPrefab, parent.position, Quaternion.identity, parent);
                    poolObject.name = originalPrefab.name;
                    poolObject.SetActive(false);
                    poolingObjectList.Add(poolObject);
                }
            }

            public GameObject Spawn(Transform activeParentValue = null, Vector2 spawnPosition = default, bool isUI = false)
            {
                GameObject poolObject = null;

                if (poolingObjectList.Count > 0)
                {
                    poolObject = poolingObjectList[0];
                    poolingObjectList.Remove(poolObject);

                    if (isUI)
                        poolObject.transform.SetParent(canvasTransform, true);
                    else
                        poolObject.transform.SetParent(activeParentValue, true);

                    if (spawnPosition != default)
                        poolObject.transform.position = spawnPosition;

                    poolObject.SetActive(true);
                }
                else
                {
                    if (spawnPosition != default)
                        poolObject = Instantiate(originalPrefab, spawnPosition, Quaternion.identity, activeParentValue);
                    else
                        poolObject = Instantiate(originalPrefab, activeParentValue);

                    poolObject.name = originalPrefab.name;
                }
                return poolObject;
            }

            public void Despawn(GameObject poolObject)
            {
                poolObject.transform.SetParent(parent);
                poolingObjectList.Add(poolObject);
                poolObject.SetActive(false);
            }
        }
    } // Inner Class
    public partial class PoolManager : MonoBehaviour // Data Field
    {
        private Dictionary<string, Pool> poolDictionary = default;
    }
    public partial class PoolManager : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            poolDictionary = new Dictionary<string, Pool>();
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
    public partial class PoolManager : MonoBehaviour // Property
    {
        public void Register()
        {
            poolDictionary.Clear();
            List<GameObject> poolableObjectList = MainSystem.Instance.SceneManager.ActiveScene.poolableObjectList;
            for (int i = 0; i < poolableObjectList.Count; i++)
            {
                Pool pool = new Pool(poolableObjectList[i]);
                pool.Register();
                poolDictionary.Add(poolableObjectList[i].name, pool);
            }
        }

        public GameObject Spawn(string name, Transform parent = null, Vector2 spawnPositon = default, bool isUI = false)
        {
            return poolDictionary[name].Spawn(parent, spawnPositon, isUI);
        }

        public void Despawn(GameObject poolObject)
        {
            poolDictionary[poolObject.name].Despawn(poolObject);
        }
    }
}
