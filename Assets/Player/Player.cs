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

        //プレイヤーの移動
        if (dx != 0 || dy != 0)
        {
            PlayerMove(dx, dy);
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
        MainCamera_Foward = MainCamera_Foward.normalized;

        if (dx != 0)//横方向移動
        {
            Vector3 MainCamera_Right = new Vector3(MainCamera_Foward.z, 0, -MainCamera_Foward.x);
            this.transform.Translate(MainCamera_Right * dx * player_speed);
        }

        if (dy != 0)//前後方向移動
        {
            this.transform.Translate(MainCamera_Foward * dy * player_speed);
        }

        Player_Anim.SetBool("Run",true);
    }
}
