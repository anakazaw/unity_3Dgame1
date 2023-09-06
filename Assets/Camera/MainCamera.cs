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

    float camera_x = 0;

    float Xx = 1f;
    float Yx = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (CameraLook == null) Debug.Log("CameraLookがない");

        //初期化
        camera_ang = 270;

    }

    // Update is called once per frame
    void Update()
    {
        //マウスのx軸、y軸取得
        float Mouse_X = Input.GetAxis("Mouse X");
        float Mouse_Y = Input.GetAxis("Mouse Y");

        //カメラ左右視点移動
        CameraMoveLR(Mouse_X);

        //カメラ上下視点移動
        CameraLookUpDown(Mouse_Y);
        

        this.transform.LookAt(CameraLook.transform.position);
    }

    void CameraMoveLR(float mx)
    {

        Xx += 0.001f;
        Xx -= Mathf.Abs(mx);
        Xx = Mathf.Clamp(Xx, 0.85f, 1f);

        if (mx != 0)
        {
            camera_x = mx > 0 ? cameraX_accel_rate : -cameraX_accel_rate;
        }
        
        camera_ang -=mx * cameraX_speed_rate + camera_x * Log(Xx);

        Debug.Log(Log(Xx));
        //Debug.Log(camera_ang);

        Vector3 Camera_Pos = this.transform.localPosition;

        Camera_Pos.x = Mathf.Cos(camera_ang) * camera_length;
        Camera_Pos.z = Mathf.Sin(camera_ang) * camera_length;

        this.transform.localPosition = Camera_Pos;

        
    }

    void CameraLookUpDown(float my)
    {
        Vector3 Camera_Pos = this.transform.localPosition;

        //Cameraの上下移動
        Camera_Pos.y += my * cameraY_speed_rate;
        Camera_Pos.y = Mathf.Clamp(Camera_Pos.y, cameraY_min, cameraY_max);

        this.transform.localPosition = Camera_Pos;

    }

    float Sigmoid(float x)
    {
        return 1/(1+Mathf.Exp(2*x))-0.5f;
    }

    float Log(float x)
    {
        return -Mathf.Log(x);
    }
}
