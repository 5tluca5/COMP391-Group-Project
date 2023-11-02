using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }


    // Reference
    public MainCharacter player;
}
