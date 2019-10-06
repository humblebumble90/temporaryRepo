using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameController gc;
    public bool gameOver;
    public bool restart;
    [Header("Enemies")]
    public int numberOfEnemy1;
    public int numberOfEnemy2;
    public GameObject enemy1;
    public GameObject enemy2;

    private float time = 0f;
    public List<GameObject> enemy1s;
    public List<GameObject> enemy2s;

    [Header("ScoreBoard")]
    [SerializeField]
    private int _lives;
    [SerializeField]
    private int _score;
    public Text livesLabel;
    public Text scoreLabel;
    public Text gameOverLabel;
    public Text restartLabel;

    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("UI Control")]
    public GameObject StartLabel;
    public GameObject StartButton;
    public int Lives
    {

        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            livesLabel.text = "Lives: " + _lives.ToString();
        }
    }
    public int Score
    {
        get
        {
            return _score;

        }
        set
        {
            _score = value;
            scoreLabel.text = "Score : " + _score.ToString();
        }
    }

    public
    // Start is called before the first frame update
    void Start()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Start":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                break;
            case "main":
                StartLabel.SetActive(false);
                StartButton.SetActive(false);
                break;
            case "End":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                StartLabel.SetActive(false);
                StartButton.SetActive(false);
                break;
        }

        Lives = 5;
        Score = 0;
        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeSoundSource = audioSources[(int)activeSoundClip];
            activeSoundSource.playOnAwake = true;
            activeSoundSource.loop = true;
            activeSoundSource.volume = 0.5f;
            activeSoundSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Setting delay of spawning enemy. time % nf means n 
        time += Time.deltaTime;
        if (time >= 3f && gameOver != true)
        {
            time = time % 1f;
            Spawn();
            Debug.Log("Enemies spawned");
        }
        if(gameOver != true && time >= 3f)
        {
            Spawn();
        }

        if (restart == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    void Spawn()
    {
        enemy1s = new List<GameObject>();
        enemy2s = new List<GameObject>();

        for (int enemy1Num = 0;enemy1Num < numberOfEnemy1; enemy1Num++)
        {
            enemy1s.Add(Instantiate(enemy1));
        }

        for (int enemy2Num = 0; enemy2Num < numberOfEnemy2; enemy2Num++)
        {
            enemy2s.Add(Instantiate(enemy2));
        }
    }
    public void GameOver()
    {
        gameOver = true;
        restart = true;
        gameOverLabel.enabled = true;
        restartLabel.enabled = true;
        audioSources[(int)SoundClip.GAME_THEME].Stop();
        audioSources[(int)SoundClip.GAME_OVER].volume = 0.5f;
        audioSources[(int)SoundClip.GAME_OVER].Play();

    }
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
