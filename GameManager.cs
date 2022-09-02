using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gameInstance;

    void Awake()
    {
        //singleton pattern to ensure only one instance
        if(gameInstance == null)
        {
            gameInstance = this;
        }
        else if(gameInstance != this){
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
