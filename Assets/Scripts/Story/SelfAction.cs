using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAction : MonoBehaviour {

    private GameObject camera1, mainCamera;
    public GameObject storyThing,magic02;
    public AudioClip ac;
    public IUnityEvent reward;
    private bool isAction;
    private bool isGot=false;

    private void Start()
    {
        camera1 = GameObject.Find("/Cameras/Camera1");
        mainCamera = Camera.main.gameObject;
        camera1.SetActive(false);
    }
    private void Update()
    {
        if (isAction)
        {
            
            storyThing.transform.position = Vector3.Lerp(storyThing.transform.position, storyThing.transform.position+ new Vector3(0, 1, 0), Time.deltaTime);
            if (storyThing.transform.localPosition.y >= 10.8)
            {
                Invoke("Action2", 3.5f);              
                isAction = false;
            }
        }
        
        
       
    }
    public void Action()
    {
        magic02.transform.GetChild(0).gameObject.SetActive(false);
            camera1.gameObject.SetActive(true);
            camera1.GetComponent<Camera>().enabled = true;
            mainCamera.GetComponent<Camera>().enabled = false;
            isAction = true;
            MusicManager.Instance.PlayMusic(ac);
        
    }

    private void Action2()
    {
        mainCamera.GetComponent<Camera>().enabled = true;
        mainCamera.gameObject.SetActive(true);
        camera1.gameObject.SetActive(false);
        camera1.GetComponent<Camera>().enabled = false;
        reward.Invoke(1);
        if (!isGot)
        {
            PlayerMes.getInstance().Snum++;
            isGot = true;
        }
        
    }
}
