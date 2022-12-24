using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score_text;
    private Player player;
    private Player player2;
    [SerializeField]
    private Image lives_img;
    [SerializeField]
    private Sprite[] lives_display;
    [SerializeField]
    private TextMeshProUGUI gameOver_text;
    [SerializeField]
    private TextMeshProUGUI restart_text;
    [SerializeField]
    private TextMeshProUGUI mainMenu_text;
    [SerializeField]
    private TextMeshProUGUI Ammo_text;
    [SerializeField]
    private TextMeshProUGUI Countdown_text;
    private bool loop = true;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Countdown_text.gameObject.SetActive(true);
        StartCoroutine(Countdown());
        mainMenu_text.gameObject.SetActive(false);
        restart_text.gameObject.SetActive(false);
        gameOver_text.gameObject.SetActive(false);
        lives_img.GetComponent<Image>().sprite = lives_display[3];
        if (_gameManager.isSingle == true)
        {
            player = GameObject.FindWithTag("Player").transform.GetComponent<Player>();
        }
        else
        {
            player = GameObject.Find("Player_1").transform.GetComponent<Player>();
            player2 = GameObject.Find("Player_2").transform.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.isSingle == true)
        {
            int lives = player._lives;
            lives_img.GetComponent<Image>().sprite = lives_display[lives];
            if (lives == 0)
            {
                if (loop == true)
                {
                    gameOver_text.gameObject.SetActive(true);
                    restart_text.gameObject.SetActive(true);
                    mainMenu_text.gameObject.SetActive(true);
                    StartCoroutine(GameOverDisplay());
                }
                loop = false;
            }
            score_text.text = "Score: " + player._score;
            Ammo_text.text = "Ammo: " + player._ammo;
        }
        else
        {
            int lives = player._lives + player2._lives;
            lives_img.gameObject.SetActive(false);
            if(lives == 0)
            {
                if (loop == true)
                {
                    gameOver_text.gameObject.SetActive(true);
                    restart_text.gameObject.SetActive(true);
                    mainMenu_text.gameObject.SetActive(true);
                    StartCoroutine(GameOverDisplay());
                }
                loop = false;
            }
            score_text.text = "";
            Ammo_text.text = "";
        }
        
    }
    //flicker sequence for GameOver text
    IEnumerator GameOverDisplay()
    {
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
            gameOver_text.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            gameOver_text.gameObject.SetActive(true);
        }
    }

    IEnumerator Countdown()
    {
        Countdown_text.text = "3";
        yield return new WaitForSeconds(1f);
        Countdown_text.text = "2";
        yield return new WaitForSeconds(1f);
        Countdown_text.text = "1";
        yield return new WaitForSeconds(1f);
        Countdown_text.text = "START!";
        yield return new WaitForSeconds(1f);
        Countdown_text.gameObject.SetActive(false);
    }
}
