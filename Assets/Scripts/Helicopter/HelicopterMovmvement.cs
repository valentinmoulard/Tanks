using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMovmvement : MonoBehaviour {

    public float speed;
    
    Transform HelicopterTransform;
    void Start()
    {
        HelicopterTransform = gameObject.GetComponent<Transform>();
    }
	// Update is called once per frame
	void Update () {
        HelicopterTransform.Translate(Vector3.forward * speed * Time.deltaTime);

        HelicopterTransform.Rotate(new Vector3(0, 1, 0), 10 * Time.deltaTime);
    }
}
