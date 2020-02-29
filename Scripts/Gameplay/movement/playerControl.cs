using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {
    
    public action leftClick;
    public action qSpell;
    public action jump;

    public Camera currentCamera;
    public Transform model;

    public unitInterface thisUnit = null;

    // Use this for initialization
    void Start () {
        if (thisUnit == null) thisUnit = GetComponent<unitInterface>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 direction = new Vector2();
		if (Input.GetKey(KeyCode.W)) direction.y = 1;
		else if (Input.GetKey(KeyCode.S)) direction.y = -1;
		if (Input.GetKey(KeyCode.D)) direction.x = 1;
		else if (Input.GetKey(KeyCode.A)) direction.x = -1;

        Vector2 cameraOffset = currentCamera.transform.position - model.transform.position;
        Vector2 mousePos = currentCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, 
                        Input.mousePosition.y, 
                        cameraOffset.magnitude));
        thisUnit.focusPoint = mousePos;

        if (Input.GetMouseButton(0)) thisUnit.use(leftClick);
        if (Input.GetKey(KeyCode.Q)) thisUnit.use(qSpell);

        if (Input.GetKey(KeyCode.Space)) thisUnit.use(jump);
        thisUnit.movementScript.move(direction);
	}
}
