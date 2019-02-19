using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] Vector3 defaultDistance = new Vector3(0f, 2.5f, -10f);
    [SerializeField] float distanceDamp = 10f;

    Transform myT;

    public Vector3 velocity = Vector3.one; //(1.0f, 1.0f, 1.0f)

    void Awake()
    {
        myT = transform;
    }

    void FixedUpdate() {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        //position finale de la camera
        Vector3 toPos = target.position + (target.rotation * defaultDistance);
        //déplacement de la camera de la position originale à la position finale avec une vitesse velocity et une distance max entre la camera et l'objet de distanceDamp
        Vector3 curPos = Vector3.SmoothDamp(myT.position, toPos, ref velocity, distanceDamp);
        //modification de la position de la camera
        myT.position = curPos;
        // la camera focus l'objet
        myT.LookAt(target);
    }


}
