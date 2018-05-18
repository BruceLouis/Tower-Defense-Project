using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    [SerializeField] float upperX, lowerX;
    [SerializeField] float upperZ, lowerZ;
    [SerializeField] float speed;

    private float xThrow, yThrow, zThrow;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        //may change input to support androids in the future
        xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * speed * Time.deltaTime;
        float newX = Mathf.Clamp(transform.position.x + xOffset, lowerX, upperX);

        zThrow = Input.GetAxis("Vertical");
        float zOffset = zThrow * speed * Time.deltaTime;
        float newZ = Mathf.Clamp(transform.position.z + zOffset, lowerZ, upperZ);

        yThrow = Input.GetAxis("Mouse ScrollWheel");
        float yOffset = -yThrow * (speed * 10f) * Time.deltaTime;
        float newY = transform.position.y + yOffset;

        transform.position = new Vector3(newX, newY, newZ);
    }
}
