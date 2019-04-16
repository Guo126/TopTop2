using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfActionThr : MonoBehaviour {

    private GameObject camera1, mainCamera;
    public GameObject storyThing;
    public AudioClip ac1,ac2;
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
            MusicManager.Instance.PlayMusic(ac2);
            Invoke("Action2", 3f);
            storyThing.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("isDown",true);
            storyThing.transform.GetChild(1).gameObject.SetActive(true);
            isAction = false;

        }

    }
    public void Action()
    {
        GameObject lockk = transform.GetChild(0).gameObject;
        if (lockk.activeSelf)
        {
            MusicManager.Instance.PlayMusic(ac1);
        }
        else
        {
            camera1.gameObject.SetActive(true);
            camera1.GetComponent<Camera>().enabled = true;
            mainCamera.GetComponent<Camera>().enabled = false;
            
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
