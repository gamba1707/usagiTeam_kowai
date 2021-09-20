using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Player : MonoBehaviour
{
    public float speed = 5F;       //歩行速度
    public float jumpSpeed = 8.0F;   //ジャンプ力
    public float gravity = 20.0F;    //重力の大きさ
    private CharacterController controller;//キャラクターコントローラー
    Animator anim;
    private Vector3 moveDirection, gravityDirection, cameraForward;
    private GameObject Player_t;
    private float x, z;//入力値
    float doubletap, tapcount;
    bool dashbool;
    [SerializeField] CinemachineVirtualCamera vcamera;
    public static float gamesec;//ゲーム経過時間
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Player_t = transform.Find("2021_Leg_Samplev0").gameObject;
        Cursor.lockState = CursorLockMode.Locked;//カーソルロック
        Cursor.visible = false;//カーソル非表示
        vcamera.m_Lens.FieldOfView = 70;
        Debug.Log(DateTime.Now.ToString("yyyy年MM月dd日") + "(" + ("日月火水木金土").Substring(int.Parse(DateTime.Now.DayOfWeek.ToString("d")), 1) + ")");
    }

    // Update is called once per frame
    void Update()
    {
        timer();
        gamesec += Time.deltaTime;
        x = Input.GetAxis("Horizontal");    //左右矢印キーの値(-1.0~1.0)
        z = Input.GetAxis("Vertical");//上下矢印キーの値(-1.0~1.0)
        if (!ButtonManager.pause)
        {
            dash();
            run();
        }
        pause();
    }

    void run()
    {
        //Debug.Log(moveDirection);
        if (x != 0 || z != 0) anim.SetBool("walk", true);
        else anim.SetBool("walk", false);
        if (controller.isGrounded)//地面に接しているとき
        {
            moveDirection = new Vector3(x, 0, z);
            cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;// カメラの方向から、X-Z平面の単位ベクトルを取得
            moveDirection = cameraForward * z + Camera.main.transform.right * x;// 方向キーの入力値とカメラの向きから、移動方向を決定
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump")) moveDirection.y = jumpSpeed;
        }
        else
        {
            Vector3 Direction = ((Camera.main.transform.right * x * speed) + (Camera.main.transform.forward * z * speed));
            //gravityDirection.y += Physics.gravity.y * Time.deltaTime * 0.03f;
            moveDirection = new Vector3(Direction.x, moveDirection.y, Direction.z);
            moveDirection = transform.TransformDirection(moveDirection);
        }
        Player_t.transform.localRotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y + 90f, 0);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    void dash()
    {
        //Debug.Log(tapcount + "," + doubletap);
        //Debug.Log("wcount:"+dashbool);
        if (doubletap == 1 && tapcount > 0.3f)
        {
            tapcount = 0;
            doubletap = 0;
        }
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            doubletap++;
        }
        else
        {
            speed = 5;
            dashbool = false;

        }
        if (doubletap == 1) tapcount += Time.deltaTime;
        if (tapcount <= 0.3f && doubletap == 2)
        {
            Debug.Log(tapcount + "," + doubletap);
            if (Input.GetKey("w") || Input.GetKey("up"))
            {
                dashbool = true;
                speed = 10;
                vcamera.m_Lens.FieldOfView = 75;
                Debug.Log("yeah");
            }
            else
            {
                tapcount = 0;
                doubletap = 0;
                speed = 5;
                vcamera.m_Lens.FieldOfView = 70;
                dashbool = false;
            }
            if (doubletap < 2)
            {
                tapcount = 0;
                doubletap = 0;
                speed = 5;
                vcamera.m_Lens.FieldOfView = 70;
                dashbool = false;
            }
        }

    }

    void pause()
    {
        if (ButtonManager.pause == true && Input.GetButtonDown("tab"))//pause解除
        {
            ButtonManager.pause = false;
        }
        else if (Input.GetButtonDown("tab"))//ポーズ画面
        {
            ButtonManager.pause = true;
        }

        //カメラ設定
        if (smartphone.camApp) vcamera.enabled = true;
        else if (ButtonManager.pause) vcamera.enabled = false;
        else vcamera.enabled = true;
    }

    void timer()
    {

    }
}
