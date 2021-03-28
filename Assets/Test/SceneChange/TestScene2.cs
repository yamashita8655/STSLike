using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickButton()
    {
		Debug.Log("OnClickButton:TestScene2");
		//LocalSceneManager.Instance.LoadScene(LocalSceneManager.SceneName.TestScene1);
    }
}
