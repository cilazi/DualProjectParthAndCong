using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider temp)
    {
        if(temp.GetComponent<Collider>().tag == "Player")
        {
            Application.Quit();
        }
    }
}
