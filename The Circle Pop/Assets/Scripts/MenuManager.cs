using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject soundOffImage;

    //public static MenuManager instance;

    public static bool musicIsOff;

    private void Start() {
        AudioManager.instance.Play("Theme");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Play Scene");
    }

    public void SetMusic()
    {
        if (!soundOffImage.activeSelf)
        {
            soundOffImage.SetActive(true);
            Destroy(AudioManager.instance.gameObject);
            musicIsOff = true;
        }
        else
        {
            soundOffImage.SetActive(false);
            musicIsOff = false;
        }
    }
}
