using project02;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class LoadingScene : BaseScene // Data Field
{
    [Header("Own Member")]
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    private float fadeSpeed;
    private float alpha;
    private bool fadeIn;
}
public partial class LoadingScene : BaseScene // Initialize
{
    private void Allocate()
    {

    }
    public override void Initialize()
    {
        base.Initialize();
        Allocate();
        Setup();
    }
    private void Setup()
    {
        fadeSpeed = 1f;
        alpha = 0.3f;
        fadeIn = true;
    }
}
public partial class LoadingScene : BaseScene // Main
{
    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    private void Update()
    {
        if (fadeIn)
        {
            alpha += fadeSpeed * Time.deltaTime;
            if (alpha >= 1f)
            {
                alpha = 1f;
                fadeIn = false;
            }
            else
                alpha -= fadeSpeed * Time.deltaTime;
            if (alpha <= 0.3f)
            {
                alpha = 0.3f;
                fadeIn = true;
            }
        }
        Color color = loadingText.color;
        color.a = alpha;
        loadingText.color = color;
    }
}
public partial class LoadingScene : BaseScene // Coroutine
{
    IEnumerator LoadSceneProcess()
    {
        SceneName sceneName = MainSystem.Instance.SceneManager.LoadSceneName;
        AsyncOperation loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName.ToString());
        loadScene.allowSceneActivation = false;
        float timer = 0f;
        while (!loadScene.isDone)
        {
            yield return null;
            if (loadScene.progress < 0.9f)
            {
                progressBar.fillAmount = loadScene.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    loadScene.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
