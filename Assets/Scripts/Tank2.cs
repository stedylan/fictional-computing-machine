using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank2 : MonoBehaviour
{
    
    const int toolNum = 5;
    private int speed;
    public GameObject tank2;
    public Texture2D blood_red;//黑色贴图
    public Texture2D blood_black;//红色贴图
    public int life = 100;
    public  int attack1 = 10;
    public GameObject goBullet;
    private Vector3 bulletPos;
    private Quaternion bulletRotation;
    private int key;
    public bool[] tools;
    private int attTime;
    public Camera camera;
    private Texture2D blood;
    private float smoothSpeed = 6f;
    private float distanceH = 10f;
    private float distanceV = 6f;
    private Texture2D hp;
    private Texture2D att;
    private Texture2D attState;
    private GameObject guided;
    private GameObject guidedMi;
    private Texture2D missile;
    // Use this for initialization
    void Start()
    {
        tools = new bool[toolNum] { false, false ,false,false,false};
        blood = Resources.Load("blood") as Texture2D;
        goBullet = Resources.Load("missile_green") as GameObject;
        speed = 30;
        missile = Resources.Load("dd") as Texture2D;
        hp = Resources.Load("Hp++") as Texture2D;
        att = Resources.Load("att") as Texture2D;
        attState = Resources.Load("Att++") as Texture2D;
        guided = Resources.Load("guidedMissile") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("tool2guided"))
        {
            if (tools[4] == true)
            {
                tools[3] = true;
                guidedMi = Instantiate(guided);
                guidedMi.transform.position = tank2.transform.position;
                tools[4] = false;
            }
        }
        if(tools[3] == true)
        {
            guidedMi.transform.LookAt(GameObject.Find("Tank1").transform);
            guidedMi.transform.position = guidedMi.transform.position + 100 * guidedMi.transform.forward * Time.deltaTime;
        }
            if (Input.GetButtonDown("Fire2"))
            {
                
                bulletPos = GameObject.Find("cannon").transform.position;
                bulletRotation = new Quaternion(0, 0, 0, 0);
                Instantiate(goBullet, bulletPos, bulletRotation);

            }
            Vector3 direction = -Input.GetAxis("Vertical2") * transform.forward;
            tank2.transform.position = transform.position + speed * direction * Time.deltaTime;
            if (tank2.transform.localEulerAngles.z >= 89 && tank2.transform.localEulerAngles.z <= 91 || tank2.transform.localEulerAngles.z >= 269 && tank2.transform.localEulerAngles.z <= 271)
            {
                if (Input.GetAxis("Horizontal2") > 0)
                    tank2.transform.localEulerAngles += new Vector3(0, 0, 90);

                if (Input.GetAxis("Horizontal2") < 0)
                    tank2.transform.localEulerAngles -= new Vector3(0, 0, 90);
            }
            else if (tank2.transform.localEulerAngles.z >= 179 && tank2.transform.localEulerAngles.z <= 181 || tank2.transform.localEulerAngles.z >= 179 && tank2.transform.localEulerAngles.z <= 181)
            {
                if (Input.GetAxis("Horizontal2") > 0)
                    tank2.transform.localEulerAngles += new Vector3(0, 0, 180);

                if (Input.GetAxis("Horizontal2") < 0)
                    tank2.transform.localEulerAngles -= new Vector3(0, 0, 180);
            }
            else
            {
                if (Input.GetAxis("Horizontal2") > 0)
                    tank2.transform.Rotate(0, 2, 0);
                if (Input.GetAxis("Horizontal2") < 0)
                    tank2.transform.Rotate(0, -2, 0);
            }
            if (life <= 0)
                Destroy(tank2);
    }
    void LateUpdate()
    {
        changePosition();
    }
    void OnGUI()
    {
        float lifeOn = (float)life / 100;
        GUI.DrawTexture(new Rect(400,650,450*lifeOn,20),blood);
        Box();
        if (Input.GetButtonDown("tool2hp"))
        {
            if (tools[0] == true)
            {
                if (life <= 70)
                    life += 30;
                else
                    life = 100;
                tools[0] = false;
            }
        }
        if (Input.GetButtonDown("tool2att"))
        {
            if (tools[1] == true)
            {
                attTime = (int)Time.time;
                tools[2] = true;
                GameObject.Find("Tank1").GetComponent<Tank1>().attack2 = 30;
                tools[1] = false;
            }
        }
        if ((int)Time.time - 5 == attTime)
        {
            tools[2] = false;
            GameObject.Find("Tank1").GetComponent<Tank1>().attack2 = 10;
        }
    }
  
    void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "tank1bullet")
            life -= attack1;
        if (collision.collider.tag == "box")
        {
            key = Random.Range(1, 4);
            if (key == 1)
                tools[0] = true;
            else if (key == 2)
                tools[1] = true;
            else if (key == 3)
                tools[4] = true;
        }
    }

    // 碰撞结束
    void OnCollisionExit(Collision collision)
    {

    }

    // 碰撞持续中
    void OnCollisionStay(Collision collision)
    {

    }
    void Box()
    {
        if (tools[2] == true)
        {
            GUI.DrawTexture(new Rect(70, 380, 20, 20), attState);
        }
        if (tools[0] == true)
            GUI.DrawTexture(new Rect(10, 420, 40, 40), hp);
        if (tools[1] == true)

            GUI.DrawTexture(new Rect(10, 460, 40, 40), att);
        if (tools[4] == true)
            GUI.DrawTexture(new Rect(10, 500, 40, 40), missile);


    }
    private void changePosition()
    {
        Vector3 nextPos = this.gameObject.transform.forward * distanceH + this.gameObject.transform.up * distanceV + this.gameObject.transform.position;
        //Vector3 nextPos = new Vector3(10,6,0) + this.gameObject.transform.position;
        camera.transform.position = Vector3.Lerp(camera.transform.position, nextPos, smoothSpeed * Time.deltaTime); //平滑插值
        camera.transform.LookAt(GameObject.Find("Cube2").transform);

    }
}
