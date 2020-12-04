using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(Player)){
            Debug.Log("aaaaaaaaaaaaaaaaa");
            MoveScene();
        }
    }

    void MoveScene(){
        SceneManager.LoadScene("TitleScene");
    }
}
