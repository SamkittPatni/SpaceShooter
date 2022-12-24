using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isSingle = true;
    private Player player;
    private Player player2;
    [SerializeField]
    private GameObject pause_menu;
    private Animator anim;
    [SerializeField]
    private Button resume_button;
    [SerializeField]
    private Button main_menu_button;
    [SerializeField]
    private Button exit_button;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Pause_Background").GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        resume_button.gameObject.SetActive(false);
        main_menu_button.gameObject.SetActive(false);
        exit_button.gameObject.SetActive(false);
        if (anim == null)
        {
            Debug.Log("Anim is NULL.");
        }
        pause_menu.SetActive(false);
        if (isSingle)
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
        //restart level if user presses R
        if (isSingle)
        {
            int lives = player._lives;
            if (Input.GetKeyDown(KeyCode.R) && lives == 0)
            {
                SceneManager.LoadScene(1); //Game Scene
            }
            if (Input.GetKeyDown(KeyCode.M) && lives == 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            int lives = player._lives + player2._lives;
            if (Input.GetKeyDown(KeyCode.R) && lives == 0)
            {
                SceneManager.LoadScene(2); //Game Scene
            }
            if (Input.GetKeyDown(KeyCode.M) && lives == 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause_menu.SetActive(true);
            PauseGame();
        }
    }

    public int Playerlives()
    {
        int lives = player._lives + player2._lives;
        return lives;
    }

    public void Main_menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        anim.SetBool("isPaused", true);
        StartCoroutine(Delay());
    }

    public void ResumeGame()
    {
        resume_button.gameObject.SetActive(false);
        main_menu_button.gameObject.SetActive(false);
        exit_button.gameObject.SetActive(false);
        pause_menu.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        resume_button.gameObject.SetActive(true);
        main_menu_button.gameObject.SetActive(true);
        exit_button.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
