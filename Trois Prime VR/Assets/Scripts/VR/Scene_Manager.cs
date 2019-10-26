using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Scene_Manager : MonoBehaviour
{
    public float _spinForce;
    public GameObject _roofEffect;
    public GameObject _pos;
    public GameObject _robot;
    public GameObject _car;
    public GameObject _mainCamera;
    private int x=0;
    private int y=0;

    private int z;
    public GameObject _btn; 
    public GameObject _countText; 

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
		 if(scene.name=="VR-2")
		 {
          StartCoroutine(CountDown());
         }
    }


    IEnumerator CountDown()
    {   z=3; 
        _countText.GetComponent<Text>().text=""+z;
        yield return new WaitForSeconds(1f);
        z=z-1;
        _countText.GetComponent<Text>().text=""+z;
        yield return new WaitForSeconds(1f);
        z=z-1;
        _countText.GetComponent<Text>().text=""+z;
        yield return new WaitForSeconds(1f);
        _countText.GetComponent<Text>().text="";
        _btn.GetComponent<Button>().interactable=true;
    }
    
    void Update()
    {
    }
    
    public void ShowEffect(){
        Instantiate (_roofEffect,_pos.transform.position,_pos.transform.rotation);
         
    }

    public void HoloRoboot(){
        x=x+1;
        if(x>1)
        {
            x=0;
        }
        if(x==1)
        {
         _robot.GetComponent<HoloController>().effectOn = true;
        }
        if(x==0)
        {
         _robot.GetComponent<HoloController>().effectOn = false;
        }
         
    }

    public void HoloCar(){
        y=y+1;
        if(y>1)
        {
            y=0;
        }
        if(y==1)
        {
         _car.GetComponent<HoloController>().effectOn = true;
        }
        if(y==0)
        {
         _car.GetComponent<HoloController>().effectOn = false;
        }
         
    }

    public void CameraGlow(){
    
    StartCoroutine(Wait());
    _mainCamera.GetComponent<CameraFilterPack_Glow_Glow>().enabled=true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        _mainCamera.GetComponent<CameraFilterPack_Glow_Glow>().enabled=false;
    }


    public void CameraBlood(){
    
    StartCoroutine(Wait1());
    _mainCamera.GetComponent<CameraFilterPack_AAA_Blood_Plus>().enabled=true;
    }

    IEnumerator Wait1()
    {
        yield return new WaitForSeconds(2f);
        _mainCamera.GetComponent<CameraFilterPack_AAA_Blood_Plus>().enabled=false;
    }





    public void SceneVR1()
    {
        SceneManager.LoadScene("VR-1");
    }

    public void SceneVR2()
    {
        SceneManager.LoadScene("VR-2");
    }
    public void SceneVR3()
    {
        SceneManager.LoadScene("VR-3");
    }
}
