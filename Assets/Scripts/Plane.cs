using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour {
    public GameObject flyplane;
    private Vector3 fwd;
    public GameObject box;
    private float dist = 10;
    private Vector3 pos;
    private Quaternion rot;
	// Use this for initialization
	void Start () {
        box = Resources.Load("box") as GameObject;
       
        fwd = transform.TransformDirection(-flyplane.transform.forward) + transform.TransformDirection(-flyplane.transform.right);
	}
	
	// Update is called once per frame
	void Update () {
            float createBox = Random.Range(0,100);
            if (createBox == 1)
            {
                pos = flyplane.transform.position;
                rot = new Quaternion(0, 0, 0, 0);
                Instantiate(box, pos, rot);
            }
 
        flyplane.transform.position = flyplane.transform.position + 100 * fwd * Time.deltaTime;
        dist -= 1 * Time.deltaTime;
        if (dist < 0)
            Destroy(flyplane);
	}
}
