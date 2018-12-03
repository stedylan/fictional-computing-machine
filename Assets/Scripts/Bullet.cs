using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    Vector3 fwd;
    public GameObject bullet2;
    private float dist = 10;
    public int kind;
	// Use this for initialization
	void Start () {
        fwd = -transform.TransformDirection(GameObject.Find("Tank2").transform.forward);
        bullet2.transform.localEulerAngles = GameObject.Find("Tank2").transform.localEulerAngles;
        bullet2.transform.localEulerAngles += new Vector3(180, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
            bullet2.transform.position = bullet2.transform.position + 100 * fwd * Time.deltaTime;
        dist -= 10 * Time.deltaTime;

        if ( dist < 0)
            Destroy(gameObject);
	}
    void OnCollisionEnter(Collision collision)
    {
        // 销毁当前游戏物体
        if (collision.collider.tag == "terrain" || collision.collider.tag == "tank1")
            Destroy(this.gameObject);
    }

    // 碰撞结束
    void OnCollisionExit(Collision collision)
    {

    }

    // 碰撞持续中
    void OnCollisionStay(Collision collision)
    {

    }
}
