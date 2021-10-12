using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //X軸の角度を制限するための変数
    float angleUp = 60f;
    float angleDown = -60f;

    [Header("キャラクター")]
    [SerializeField] GameObject player;
    [Header("空のオブジェクト（子関係）")]
    [SerializeField] GameObject target;
    [Header("カメラ（子関係）")]
    [SerializeField] Camera cam;

    [Header("スフィアキャスト")]
    [Header("当たったオブジェクト")]
    private RaycastHit Hit;
    [Header("無視するオブジェクトレイヤー")]
    [SerializeField] private int Mask = default;
    [Header("キャラカメラ間距離")]
    private float cameradistance;
    [Header("半径")]
    [SerializeField] float radius = 0.3f;

    [Header("カメラ")]
    [Header("感度")]
    [SerializeField] float rotate_speed = 3;
    [Header("補完スピード")]
    [SerializeField] float interpolation_speed = 5;
    [Header("補完距離")]
    [SerializeField] float interpolation_distance = 5;
    [Header("ズーム感度")]
    [SerializeField] float zoom_speed = 1;
    [Header("原点位置を指定する変数")]
    [SerializeField] Vector3 axisPos;
    [Header("スクロール範囲")]
    [SerializeField] float scroll = 0;
    [SerializeField] float _maxrange = 0;
    [SerializeField] float _minrange = 0;
    //マウスホイールの値を保存
    [SerializeField] float scrollLog;
    //https://tech.pjin.jp/blog/2016/11/04/unity_skill_5/
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //CameraのAxisに相対的な位置をlocalPositionで指定
        target.transform.localPosition = new Vector3(0, 0, 3);

        cameradistance = Vector3.Distance(player.transform.position, transform.position);

        scroll = _minrange;
    }

    void Update()
    {
        //Axisの位置をキャラの位置＋axisPosで決める
        transform.position = player.transform.position + axisPos;
        cameradistance = Vector3.Distance(transform.position, target.transform.position);
        //球体を飛ばしてめり込み防止
        if (Physics.SphereCast(player.transform.position + Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * axisPos, radius, target.transform.position - player.transform.position - axisPos, out Hit, cameradistance, ~(1 << Mask)))
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Hit.point + -transform.forward * interpolation_distance, interpolation_speed) ;
        }
        else
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0, scrollLog), interpolation_speed);
        }
        //スクロールのスピードを計算して値を入れる
        float wheelspeed = Input.GetAxis("Mouse ScrollWheel") * -1 * zoom_speed;

        scroll = wheelspeed;
        //scrollAdd += Input.GetAxis("Mouse ScrollWheel") * -10;
        if (scrollLog+wheelspeed > _maxrange)
        {
            scrollLog = _maxrange;
            return;
        }
        if (scrollLog + wheelspeed < _minrange)
        {
            scrollLog = _minrange;
            return;
        }
        //マウススクロールの値は動かさないと0になるのでここで保存する
        scrollLog += wheelspeed;

        //Cameraの位置、Z軸にスクロール分を加える
        target.transform.localPosition
            = new Vector3(target.transform.localPosition.x,
            target.transform.localPosition.y,
            target.transform.localPosition.z + scroll);

        //Cameraの角度にマウスからとった値を入れる
        transform.eulerAngles += new Vector3(
            Input.GetAxis("Mouse Y") * rotate_speed,
            Input.GetAxis("Mouse X") * rotate_speed
            , 0);

        //X軸の角度
        float angleX = transform.eulerAngles.x;
        //X軸の値を180度超えたら360引くことで制限しやすくする
        if (angleX >= 180)
        {
            angleX = angleX - 360;
        }
        //Mathf.Clamp(値、最小値、最大値）でX軸の値を制限する
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angleX, angleDown, angleUp),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );
    }
    void OnDrawGizmos()
    {
        if (Physics.SphereCast(player.transform.position + Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * axisPos, radius, target.transform.position - player.transform.position - axisPos, out Hit, cameradistance, Mask))
        {
            Gizmos.DrawWireSphere(Hit.point,radius);
        }
        else
        {
            Gizmos.DrawWireSphere(target.transform.position, radius);
        }
        Gizmos.DrawRay(player.transform.position + Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * axisPos, target.transform.position-player.transform.position - axisPos);

    }
}
