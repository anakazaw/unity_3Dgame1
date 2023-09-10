using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //ゲームオブジェクト
    public GameObject CameraLook;


    public float camera_length = 2f;//プレイヤーからのカメラの引き具合
    [SerializeField]float camera_ang = 270f;//プレイヤーからの角度
    [SerializeField]float camera_height = 2.2f;//プレイヤーの頭上からの高さ

    [SerializeField]float cameraY_max = 1f;//CameraのY軸の最大値
    [SerializeField]float cameraY_min = -1f;//CameraのY軸の最小値

    public float cameraX_speed_rate = 1f;//カメラX軸の速度割合
    public float cameraY_speed_rate = 1f;//カメラY軸の速度割合

<<<<<<< Updated upstream
    [SerializeField,Tooltip("二乗曲線")]bool multiple_flag = true;
=======
    [SerializeField,Tooltip("なめらかに動かす")]bool smooth_flag = true;
>>>>>>> Stashed changes
    [SerializeField, Tooltip("球状に移動する")] bool sphere_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        if (CameraLook == null) Debug.Log("CameraLookがない");

        //初期化
<<<<<<< Updated upstream
        camera_ang = 270;
        this.transform.localPosition = - Vector3.forward * camera_length;
=======
        this.transform.position = CameraLook.transform.position - Vector3.forward * camera_length;
>>>>>>> Stashed changes

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //マウスのx軸、y軸取得
        float Mouse_X = Input.GetAxis("Mouse X");
        float Mouse_Y = Input.GetAxis("Mouse Y");

        //カメラ左右視点移動
        CameraMove(Mouse_X,Mouse_Y);


        this.transform.LookAt(CameraLook.transform.position);
    }

    void CameraMove(float mx ,float my)
    {
<<<<<<< Updated upstream
        Vector3 Camera_Pos = this.transform.localPosition;

        //Camera横移動
        if (multiple_flag)
        {
            //二乗曲線
=======
        Vector3 CameraLook_Pos = CameraLook.transform.position;
        Vector3 Camera_Pos = Vector3.zero;

        //Camera横移動
        if (smooth_flag)
        {
            //smooth (smooth damp)
>>>>>>> Stashed changes
            camera_ang -= cameraX_speed_rate * multiply(mx);
        }
        else
        {
            //リニア
            camera_ang -= mx * cameraX_speed_rate;
        }
        

        //Cameraの上下移動
<<<<<<< Updated upstream
        if (multiple_flag)
        {
            //二乗曲線
            Camera_Pos.y -= cameraY_speed_rate * multiply(my);
=======
        if (smooth_flag)
        {
            //smooth (smooth damp)
            camera_height -= cameraY_speed_rate * multiply(my);
>>>>>>> Stashed changes
        }
        else
        {
            //リニア
<<<<<<< Updated upstream
            Camera_Pos.y -= my * cameraY_speed_rate;
        }


        Camera_Pos.y = Mathf.Clamp(Camera_Pos.y, cameraY_min, cameraY_max);
=======
            camera_height -= my * cameraY_speed_rate;
        }

        camera_height = Mathf.Clamp(camera_height, cameraY_min, cameraY_max);//高さを制限

>>>>>>> Stashed changes

        float LookAt_to_Camera = camera_length;

        if (sphere_flag)
        {
            //球状に移動
<<<<<<< Updated upstream
            LookAt_to_Camera = CameraLook.transform.localPosition.y - Camera_Pos.y;
            LookAt_to_Camera  = Mathf.Cos(Mathf.Asin(LookAt_to_Camera / camera_length)) * camera_length;
        
        }
 
        Camera_Pos.x = Mathf.Cos(camera_ang) * LookAt_to_Camera;
        Camera_Pos.z = Mathf.Sin(camera_ang) * LookAt_to_Camera;

        this.transform.localPosition = Camera_Pos;

        
    }

    float multiply(float x)
    {
        return x*Mathf.Abs(x);
    }

=======
            LookAt_to_Camera = camera_height;
            LookAt_to_Camera  = Mathf.Cos(Mathf.Asin(LookAt_to_Camera / camera_length)) * camera_length;
        
        }

        Camera_Pos.x = Mathf.Cos(camera_ang);
        Camera_Pos.z = Mathf.Sin(camera_ang);
        Camera_Pos *= LookAt_to_Camera;

        Camera_Pos.y = camera_height;

        this.transform.position = Camera_Pos + CameraLook_Pos;
       
    }

    float multiply(float x)
    {
        return x*Mathf.Abs(x);
    }

>>>>>>> Stashed changes
}
