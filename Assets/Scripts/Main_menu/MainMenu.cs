using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Image title_screen;
    [SerializeField]
    private Button start_button;
    [SerializeField]
    private AudioSource button_sound;
    [SerializeField]
    private Button coop_button;
    [SerializeField]
    private Button options_button;
    [SerializeField]
    private Button exit_button;
    [SerializeField]
    public Slider volume_slider;
    [SerializeField]
    private GameObject options_menu;
    [SerializeField]
    private GameObject controls_menu;
    [SerializeField]
    private GameObject instructions_menu;
    private float volume;
    // Start is called before the first frame update
    void Start()
    {
        volume_slider.value = PlayerPrefs.GetFloat("vol", 1);
        options_menu.SetActive(false);
        controls_menu.SetActive(false);
        instructions_menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        volume = volume_slider.value;
        PlayerPrefs.SetFloat("vol", volume_slider.value);
        AudioListener.volume = volume;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadSinglePlayer()
    {
        StartCoroutine(ButtonFlicker(1));
    }

    public void LoadCoOpMode()
    {
        StartCoroutine(ButtonFlickerCoOp(2));
    }

    public void OptionsMenu()
    {
        button_sound.Play(0);
        start_button.gameObject.SetActive(false);
        coop_button.gameObject.SetActive(false);
        options_button.gameObject.SetActive(false);
        exit_button.gameObject.SetActive(false);
        title_screen.gameObject.SetActive(false);
        options_menu.SetActive(true);
    }

    public void OptionsBack()
    {
        start_button.gameObject.SetActive(true);
        coop_button.gameObject.SetActive(true);
        options_button.gameObject.SetActive(true);
        exit_button.gameObject.SetActive(true);
        title_screen.gameObject.SetActive(true);
        options_menu.SetActive(false);
    }

    public void ControlsMenu()
    {
        options_menu.SetActive(false);
        controls_menu.SetActive(true);
    }

    public void ControlsBack()
    {
        options_menu.SetActive(true);
        controls_menu.SetActive(false);
    }

    public void InstructionsMenu()
    {
        options_menu.SetActive(false);
        instructions_menu.SetActive(true);
    }

    public void InstructionsBack()
    {
        options_menu.SetActive(true);
        instructions_menu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    IEnumerator ButtonFlicker(int mode)
    {
        button_sound.Play(0);
        start_button.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.03f);
        start_button.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        start_button.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.03f);
        start_button.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(mode); //Single Player scene
    }

    IEnumerator ButtonFlickerCoOp(int mode)
    {
        button_sound.Play(0);
        coop_button.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.03f);
        coop_button.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        coop_button.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.03f);
        coop_button.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(mode);
    }
}
