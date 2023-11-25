using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    private static string nextScene;
    [SerializeField] private Transform progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        
        float timer = 0f;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                progressBar.localScale = new Vector3(op.progress, 1,1);
            }
            else
            {
                if (progressBar != null)
                {
                    timer += Time.unscaledDeltaTime;
                    progressBar.localScale = new Vector3(Mathf.Lerp(0.9f, 1f, timer), 1,1);
                    if (progressBar.localScale.x >= 1f)
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
                else if (progressText != null)
                {
                    timer += Time.unscaledDeltaTime;
                    float percent = Mathf.Lerp(0.9f, 1f, timer);
                    progressText.text = $"LOADING...\n{Mathf.Round(percent * 100)}%";
                    if (percent >= 1f)
                    {
                        progressText.text = $"COMPLETE\n100%";
                        for (int i = 0; i < 6; i++)
                        {
                            progressText.color = new Color(1, 1, 1, 0);
                            yield return new WaitForSecondsRealtime(0.1f);
                            progressText.color = new Color(1, 1, 1, 1);
                            yield return new WaitForSecondsRealtime(0.1f);
                        }
                        yield return new WaitForSecondsRealtime(2);
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }

    }
}
