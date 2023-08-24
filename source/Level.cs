using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Algorand.Unity;

public class Level : MonoBehaviour
{
    public static Level instance;


    public uint numDestructables = 0;
    public bool startNextLevel = false;
    float nextLevelTimer = 3;

    string[] levels = { "Level1", "Level2", "Level3" };
    int currentLevel = 1;

    int score = 0;
    Text scoreText;

    private Account account;
    private string txnStatus;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;
                if (currentLevel <= levels.Length)
                {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadSceneAsync(sceneName);
                }
                else
                {
                    startNextLevel = false;
                    Debug.Log("GAME OVER!!!");

                    Algo.instance.WrappedSendAsset(); // Send asset to user
                    StartCoroutine("ResetGame");
                }
                nextLevelTimer = 3;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }

        // Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                // GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                // GameObject.Find("PauseMenu").GetComponent<Canvas>().enabled = false;
            }
        }
    }

    public void ResetLevel()
    {
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>())
        {
            Destroy(b.gameObject);
        }
        numDestructables = 0;
        score = 0;
        AddScore(score);
        string sceneName = levels[currentLevel - 1];
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ResetGame()
    {
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>())
        {
            Destroy(b.gameObject);
        }
        numDestructables = 0;
        score = 0;
        AddScore(score);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }


    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        scoreText.text = score.ToString();
    }

    public void AddDestructable()
    {
        numDestructables++;
    }

    public void RemoveDestructable()
    {
        numDestructables--;

        if (numDestructables == 0)
        {
            startNextLevel = true;
        }

    }

}
