using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string cutScene;
    public GameObject startButton;
    public GameObject resumeButton;
    public GameObject optionsScreen;
    public GameObject controlsScreen;
    public LoadingManager LoadingManager;
    public FadeController fadeController;

    public AudioSource buttonClick;
    public AudioSource mainmenuMusic;  
    public AudioSource cutsceneMusic;

    [SerializeField] private float musicFade = 1f;
    
    public void Start()
    {
        if (mainmenuMusic != null)
        {
            DontDestroyOnLoad(mainmenuMusic.gameObject);
        }

        if (cutsceneMusic != null)
        {
            cutsceneMusic.Stop();
        }

        // Handle button visibility based on PlayerPrefs
        if (PlayerPrefs.GetInt("FromPauseMenu", 0) == 1)
        {
            PlayerPrefs.SetInt("FromPauseMenu", 0); 
            startButton.SetActive(false);       
            resumeButton.SetActive(true);         
        }
        else
        {
            startButton.SetActive(true);           
            resumeButton.SetActive(false);         
        }

   
        if (PlayerPrefs.GetInt("OpenControlsPanel", 0) == 1)
        {
            PlayerPrefs.SetInt("OpenControlsPanel", 0); // Reset the flag
            OpenControlsPanel();
        }
        else
        {
            controlsScreen.SetActive(false);
        }

        controlsScreen.SetActive(false);
    }
    public void StartGame()
    {
        ActivateSound();

        if (LoadingManager != null && fadeController != null)
        {
            StartCoroutine(FadeAndLoadCutscene());
        }
        else if (LoadingManager != null)
        {
            LoadingManager.LoadScene(cutScene);
        }
    }

    public void OpenOptions()
    {
        ActivateSound();
        optionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        ActivateSound();
        optionsScreen.SetActive(false);
    }

    public void OpenControlsPanel()
    {
        ActivateSound();
        if (controlsScreen != null)
        {
            controlsScreen.SetActive(true); 
        }
    }

    public void CloseControlsPanel()
    {
        ActivateSound();
        if (controlsScreen != null)
        {
            controlsScreen.SetActive(false); 
        }
    }

    private void ActivateSound()
    {
        if (buttonClick != null)
        {
            buttonClick.Play();
        }
    }

    private IEnumerator TransitionSound()
    {
        if (mainmenuMusic != null)
        {
            float startVolume = mainmenuMusic.volume;

            // Fade out
            for (float t = 0; t < musicFade; t += Time.deltaTime)
            {
                mainmenuMusic.volume = Mathf.Lerp(startVolume, 0, t / musicFade);
                yield return null;
            }

            // Ensure music is completely muted after the fade-out
            mainmenuMusic.volume = 0;
            mainmenuMusic.Stop();
        }

        // Fade in the cutscene music
        if (cutsceneMusic != null)
        {
            cutsceneMusic.Play();
            float startVolume = 0f;
            cutsceneMusic.volume = startVolume;

            // Fade in
            for (float t = 0; t < musicFade; t += Time.deltaTime)
            {
                cutsceneMusic.volume = Mathf.Lerp(startVolume, 1, t / musicFade);
                yield return null;
            }

            cutsceneMusic.volume = 1;
        }
    }
    
    private IEnumerator FadeAndLoadCutscene()
    {
        yield return StartCoroutine(TransitionSound());

        yield return StartCoroutine(fadeController.FadeIn(1f));

        LoadingManager.LoadScene(cutScene);
    }

    public void ResumeGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        ActivateSound();
        Application.Quit();
    }
}
