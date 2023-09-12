using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject MainCamera = null;
    Animator Player_Anim;

    [SerializeField] float player_speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (MainCamera == null) Debug.Log("MainCameraがない");
        Player_Anim = this.GetComponent<Animator>();

        //初期化
    }

    // Update is called once per frame
    void Update()
    {
        //入力情報
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        //dx += 0.1f;

        //プレイヤーの移動・向き
        if (dx != 0 || dy != 0)
        {
            PlayerMove(dx, dy);

            Player_Anim.SetBool("Run", true);
        }
        else
        {
            Player_Anim.SetBool("Run", false);
        }

    }

    void PlayerMove(float dx,float dy)
    {
        Vector3 MainCamera_Foward = MainCamera.transform.forward;
        MainCamera_Foward.y = 0;

        Vector3 Move_Vec = Vector3.zero;

        if (dx != 0)//横方向移動
        {
            Vector3 MainCamera_Right = new Vector3(MainCamera_Foward.z, 0, -MainCamera_Foward.x);
            MainCamera_Right = MainCamera_Right * dx;

            Move_Vec += MainCamera_Right;
        }

        if (dy != 0)//前後方向移動
        {
            MainCamera_Foward = MainCamera_Foward * dy;

            Move_Vec += MainCamera_Foward;
        }

        Move_Vec = Move_Vec.normalized;
        this.transform.Translate(Move_Vec * player_speed,Space.World);

        //Quaternion Player_look = Quaternion.LookRotation(Target_Vec, Vector3.up);
        this.transform.LookAt(this.transform.position + Move_Vec, Vector3.up);

 
    }
}
