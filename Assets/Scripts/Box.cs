using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    public GameObject box;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        // 销毁当前游戏物体
        if (collision.collider.tag == "tank1" || collision.collider.tag == "tank2")
            Destroy(this.gameObject);
    }
}
