using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RedColor : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        float change = GameObject.Find("hero").GetComponent<PlayerHealth>().anger;

        GetComponent<Slider>().value = change * 0.01f;
	
	}
}
