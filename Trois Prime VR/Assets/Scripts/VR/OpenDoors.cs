using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenDoors : MonoBehaviour
{
    public GameObject _leftDoor;
    public GameObject _rightDoor;
    public GameObject _logo;
 
    // Update is called once per frame
    public void OpenDoor()
    {
        _logo.SetActive(true);
        _leftDoor.GetComponent<Animation>().Play();
        _rightDoor.GetComponent<Animation>().Play();
        StartCoroutine(Wait());
        
     }

     IEnumerator Wait()
    {  
        yield return new WaitForSeconds(1f);
         
		 if(this.gameObject.name=="Right_Door4" || this.gameObject.name=="Left_Door4")
		 {
          SceneManager.LoadScene("VR-4");
         }

         if(this.gameObject.name=="Right_Door5" || this.gameObject.name=="Left_Door5")
		 {
          SceneManager.LoadScene("VR-5");
         }

    }
}
