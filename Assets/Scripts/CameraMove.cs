using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //X���̊p�x�𐧌����邽�߂̕ϐ�
    float angleUp = 60f;
    float angleDown = -60f;

    [Header("�L�����N�^�[")]
    [SerializeField] GameObject player;
    [Header("��̃I�u�W�F�N�g�i�q�֌W�j")]
    [SerializeField] GameObject target;
    [Header("�J�����i�q�֌W�j")]
    [SerializeField] Camera cam;

    [Header("�X�t�B�A�L���X�g")]
    [Header("���������I�u�W�F�N�g")]
    private RaycastHit Hit;
    [Header("��������I�u�W�F�N�g���C���[")]
    [SerializeField] private int Mask = default;
    [Header("�L�����J�����ԋ���")]
    private float cameradistance;
    [Header("���a")]
    [SerializeField] float radius = 0.3f;

    [Header("�J����")]
    [Header("���x")]
    [SerializeField] float rotate_speed = 3;
    [Header("�⊮�X�s�[�h")]
    [SerializeField] float interpolation_speed = 5;
    [Header("�⊮����")]
    [SerializeField] float interpolation_distance = 5;
    [Header("�Y�[�����x")]
    [SerializeField] float zoom_speed = 1;
    [Header("���_�ʒu���w�肷��ϐ�")]
    [SerializeField] Vector3 axisPos;
    [Header("�X�N���[���͈�")]
    [SerializeField] float scroll = 0;
    [SerializeField] float _maxrange = 0;
    [SerializeField] float _minrange = 0;
    //�}�E�X�z�C�[���̒l��ۑ�
    [SerializeField] float scrollLog;
    //https://tech.pjin.jp/blog/2016/11/04/unity_skill_5/
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Camera��Axis�ɑ��ΓI�Ȉʒu��localPosition�Ŏw��
        target.transform.localPosition = new Vector3(0, 0, 3);

        cameradistance = Vector3.Distance(player.transform.position, transform.position);

        scroll = _minrange;
    }

    void Update()
    {
        //Axis�̈ʒu���L�����̈ʒu�{axisPos�Ō��߂�
        transform.position = player.transform.position + axisPos;
        cameradistance = Vector3.Distance(transform.position, target.transform.position);
        //���̂��΂��Ă߂荞�ݖh�~
        if (Physics.SphereCast(player.transform.position + Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * axisPos, radius, target.transform.position - player.transform.position - axisPos, out Hit, cameradistance, ~(1 << Mask)))
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, Hit.point + -transform.forward * interpolation_distance, interpolation_speed) ;
        }
        else
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0, scrollLog), interpolation_speed);
        }
        //�X�N���[���̃X�s�[�h���v�Z���Ēl������
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
        //�}�E�X�X�N���[���̒l�͓������Ȃ���0�ɂȂ�̂ł����ŕۑ�����
        scrollLog += wheelspeed;

        //Camera�̈ʒu�AZ���ɃX�N���[������������
        target.transform.localPosition
            = new Vector3(target.transform.localPosition.x,
            target.transform.localPosition.y,
            target.transform.localPosition.z + scroll);

        //Camera�̊p�x�Ƀ}�E�X����Ƃ����l������
        transform.eulerAngles += new Vector3(
            Input.GetAxis("Mouse Y") * rotate_speed,
            Input.GetAxis("Mouse X") * rotate_speed
            , 0);

        //X���̊p�x
        float angleX = transform.eulerAngles.x;
        //X���̒l��180�x��������360�������ƂŐ������₷������
        if (angleX >= 180)
        {
            angleX = angleX - 360;
        }
        //Mathf.Clamp(�l�A�ŏ��l�A�ő�l�j��X���̒l�𐧌�����
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
