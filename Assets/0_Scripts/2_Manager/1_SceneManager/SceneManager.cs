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

    public partial class SceneManager : MonoBehaviour
    {
        public BaseScene ActiveScene { get; private set; } = default;
        public SceneName LoadSceneName { get; private set; } = default;
    }
    public partial class SceneManager : MonoBehaviour
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
    public partial class SceneManager : MonoBehaviour // Sign
    {
        public void SignUpActiveScene(BaseScene activeSceneValue)
        {
            ActiveScene = activeSceneValue;
            ActiveScene.Initialize();
        }
        public void SignDownActiveScene()
        {
            ActiveScene = null;
        }
    }
    public partial class SceneManager : MonoBehaviour // Property
    {
        public void LoadScene(SceneName sceneName)
        {
            LoadSceneName = sceneName;
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.LoadingScene.ToString());
        }
    }
}
