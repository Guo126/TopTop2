using UnityEngine;
using UnityEngine.UI;

public class mouseStyle :MonoBehaviour
{
    public Texture mouse;
    public RawImage image1, image2;
    public GameObject music;
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("Story2") == 1)
        {
            startGame();
        }
        else
        {

            Invoke("startGame", 15f);
        }
        
    }

    

    void startGame()
    {
        if (image1 != null)
        {
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
            
            PlayerPrefs.SetInt("Story2", 1);
        }
        
    }

    private void OnApplicationQuit()
    {
        
    }

    public void Destroy()
    {
        Application.Quit();

    }

}

