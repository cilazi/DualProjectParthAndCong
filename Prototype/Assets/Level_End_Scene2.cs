using UnityEngine;
using System.Collections;

public class Level_End_Scene2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Application.LoadLevelAsync("Prototype_Scene3");
    }
}
