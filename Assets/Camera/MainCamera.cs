using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //ゲームオブジェクト
    public GameObject CameraLook;


    public float camera_length = 2f;//プレイヤーからのカメラの引き具合
    float camera_ang;//プレイヤーからの角度

    [SerializeField]float cameraY_max = 1f;//CameraのY軸の最大値
    [SerializeField]float cameraY_min = -1f;//CameraのY軸の最小値

    public float cameraX_speed_rate = 1f;//カメラX軸の速度割合
    [SerializeField]float cameraX_accel_rate = 1f;//カメラX軸の加速度割合
    public float cameraY_speed_rate = 1f;//カメラY軸の速度割合
    [SerializeField]float cameraY_accel_rate = 1f;//カメラX軸の加速度割合

    [SerializeField]bool accel_flag = true;

    float camera_x = 0;
    float camera_y = 0;

    float Xx = 1f;
    float Yx = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (CameraLook == null) Debug.Log("CameraLookがない");

        //初期化
        camera_ang = 270;
        this.transform.localPosition = - Vector3.forward * camera_length;

    }

    // Update is called once per frame
    void Update()
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
        Vector3 Camera_Pos = this.transform.localPosition;

        Xx += 0.001f;
        Xx -= Mathf.Abs(mx);
        Xx = Mathf.Clamp(Xx, 0.8f, 1f);

        if (mx != 0)
        {
            camera_x = mx > 0 ? cameraX_accel_rate : -cameraX_accel_rate;
        }

        if (accel_flag)
        {
            //加速度あり
            camera_ang -= mx * cameraX_speed_rate + camera_x * Log(Xx);
        }
        else
        {
            //加速度なし
            camera_ang -= mx * cameraX_speed_rate;
        }

        

        Yx += 0.001f;
        Yx -= Mathf.Abs(my);
        Yx = Mathf.Clamp(Yx, 0.8f, 1f);

        if (my != 0)
        {
            camera_y = my > 0 ? cameraY_accel_rate : -cameraY_accel_rate;
        }

        //Cameraの上下移動
        if (accel_flag)
        {
            //加速度あり
            Camera_Pos.y -= my * cameraY_speed_rate + camera_y * Log(Yx);
        }
        else
        {
            //加速度なし
            Camera_Pos.y -= my * cameraY_speed_rate;
        }


        Camera_Pos.y = Mathf.Clamp(Camera_Pos.y, cameraY_min, cameraY_max);

        float LookAt_to_Camera = CameraLook.transform.localPosition.y - Camera_Pos.y;
        LookAt_to_Camera  = Mathf.Cos(Mathf.Asin(LookAt_to_Camera / camera_length)) * camera_length;

        Camera_Pos.x = Mathf.Cos(camera_ang) * LookAt_to_Camera;
        Camera_Pos.z = Mathf.Sin(camera_ang) * LookAt_to_Camera;

        this.transform.localPosition = Camera_Pos;

        
    }


    float Log(float x)
    {
        return -Mathf.Log(x);
    }
}
