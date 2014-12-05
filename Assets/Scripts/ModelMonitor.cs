using UnityEngine;
using System.Collections;

public class ModelMonitor : MonoBehaviour {
    private GameObject _model = null;
    private GameObject _spinZoneGo = null;
    private Camera _UICamera = null;
    public Transform topLeft = null;
    public Transform bottomRight = null;
    public bool freezeXAxis = true;
    public bool freezeYAxis = false;
    public float speed = 1f;

	// Use this for initialization
	void Start () {
        _model = GameObject.Find("Main Camera/Orc");
        topLeft = transform.Find("TopLeft");
        bottomRight = transform.Find("BottomRight");

        InitCamera();
        InitSpinZoneGo();
	}

    void InitCamera()
    {
        _UICamera = GameObject.Find("UI Root/Camera").GetComponent<Camera>();
    }

    void InitSpinZoneGo()
    {
        _spinZoneGo = new GameObject();
        _spinZoneGo.layer = _UICamera.gameObject.layer;
        _spinZoneGo.name = "SpinZone";
        _spinZoneGo.transform.parent = transform;
        _spinZoneGo.transform.localEulerAngles = new Vector3(0, 0, 0);
        _spinZoneGo.transform.localScale = new Vector3(1, 1, 1);
        _spinZoneGo.AddComponent<UIWidget>();

        UIEventListener.Get(_spinZoneGo).onDrag = OnModelDrag;
        UIEventListener.Get(_spinZoneGo).onClick = OnModelClick;

        BoxCollider bc = _spinZoneGo.AddComponent<BoxCollider>();
        bc.isTrigger = true;
        bc.center = new Vector3(0, 0, 0);
        float boxWidth = Mathf.Abs(bottomRight.localPosition.x - topLeft.localPosition.x);
        float boxHeight = Mathf.Abs(bottomRight.localPosition.y - topLeft.localPosition.y);
        bc.size = new Vector3(boxWidth, boxHeight, 0);
    }

    void OnModelDrag(GameObject go, Vector2 delta)
    {
        // 互换x，y，因为当鼠标左右移动的时候，要求模型绕y移动
        Vector2 delta0 = new Vector2(delta.y, delta.x);
        Vector2 res = new Vector2(freezeXAxis ? 0 : delta0.x * speed,
            freezeYAxis ? 0 : -delta0.y * speed);
        _model.transform.localRotation = Quaternion.Euler(res.x, res.y, 0f) * _model.transform.localRotation;
    }

    void OnModelClick(GameObject go)
    {
 
    }
	
	// Update is called once per frame
	void Update () {
        //_model.transform.Rotate(0, 180*Time.deltaTime, 0);
	}
}
