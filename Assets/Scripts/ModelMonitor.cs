using UnityEngine;
using System.Collections;

public class ModelMonitor : MonoBehaviour {
    private GameObject _model = null;

	// Use this for initialization
	void Start () {
        _model = GameObject.Find("Main Camera/Orc");
	
	}
	
	// Update is called once per frame
	void Update () {
        _model.transform.Rotate(0, 180*Time.deltaTime, 0);
	}
}
