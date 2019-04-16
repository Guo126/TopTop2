using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfActionSeco : MonoBehaviour {
    private GameObject camera1, mainCamera;
    public GameObject storyThing,magic03;
    public AudioClip ac;
    private bool isAction;

    private void Start()
    {
        camera1 = GameObject.Find("/Cameras/Camera1");
        mainCamera = Camera.main.gameObject;

    }
    private void Update()
    {
        if (isAction)
        {
                
                Invoke("Action2", 3f);
                isAction = false;
            
        }
        
    }
    public void Action()
    {
        GameObject lockk = transform.GetChild(0).gameObject;
        if (lockk.activeSelf)
        {
            MusicManager.Instance.PlayMusic(ac);
        }
        else
        {
            camera1.gameObject.SetActive(true);
            camera1.GetComponent<Camera>().enabled = true;
            mainCamera.GetComponent<Camera>().enabled = false;
            storyThing.GetComponent<SelfRotation>().isStop = true;
            magic03.transform.GetChild(0).gameObject.SetActive(false);
            isAction = true;
        }


        //storyThing.transform.position += new Vector3(0, 20, 0);
    }

    private void Action2()
    {
        mainCamera.GetComponent<Camera>().enabled = true;
        mainCamera.gameObject.SetActive(true);
        camera1.gameObject.SetActive(false);
        camera1.GetComponent<Camera>().enabled = false;
    }
}
