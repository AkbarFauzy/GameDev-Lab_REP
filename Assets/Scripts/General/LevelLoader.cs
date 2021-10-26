using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private string previousScene;
    private string nextScene;


    private LevelLoader Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }

    }

    public void LoadLevel(string nextLevel)
    {
        setPreviousLevel(SceneManager.GetActiveScene());
        SceneManager.LoadScene(nextLevel);
    }

    private void setPreviousLevel(Scene scene)
    {
        previousScene = scene.name;
    }
}
