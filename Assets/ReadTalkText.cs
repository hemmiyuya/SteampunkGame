using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ReadTalkText : MonoBehaviour
{
    private bool CanRead = false;


    [SerializeField]
    private Text dataText;
    //　読む込むテキストが書き込まれている.txtファイル
    [SerializeField]
    private TextAsset textAsset;

    private string loadText;
    private string[] splitText;
    private int textNum;

    [SerializeField]
    private StartTalkIvent talkIvent;


    //　表示するメッセージ
    [SerializeField]
    [TextArea(1, 20)]
    private string allMessage = "今回はRPGでよく使われるメッセージ表示機能を作りたいと思います。\n"
            + "メッセージが表示されるスピードの調節も可能であり、改行にも対応します。\n"
            + "改善の余地がかなりありますが、               最低限の機能は備えていると思われます。\n"
            + "ぜひ活用してみてください。\n<>"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ<>"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ<>"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ"
            + "あああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああああ";
    //　使用する分割文字列
    [SerializeField]
    private string splitString = "<>";
    //　分割したメッセージ
    private string[] splitMessage;
    //　分割したメッセージの何番目か
    private int messageNum;
    //　テキストスピード
    [SerializeField]
    private float textSpeed = 0.05f;
    //　経過時間
    private float elapsedTime = 0f;
    //　今見ている文字番号
    private int nowTextNum = 0;

    //　1回分のメッセージを表示したかどうか
    private bool isOneMessage = false;
    //　メッセージをすべて表示したかどうか
    private bool isEndMessage = true;

    void Start()
    {
        //clickIcon = transform.Find("Panel/Image").GetComponent<Image>();
        dataText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        dataText.text = "";
        gameObject.SetActive(false);
    }

    void Update()
    {
        //　メッセージが終わっているか、メッセージがない場合はこれ以降何もしない
        if (isEndMessage || allMessage == null)
        {
            return;
        }

        //　1回に表示するメッセージを表示していない	
        if (!isOneMessage)
        {
            //　テキスト表示時間を経過したらメッセージを追加
            if (elapsedTime >= textSpeed)
            {
                dataText.text += splitMessage[messageNum][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;

                //　メッセージを全部表示、または行数が最大数表示された
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //　メッセージ表示中にマウスの右ボタンを押したら一括表示
            if (Input.GetMouseButtonDown(1))
            {
                //　ここまでに表示しているテキストに残りのメッセージを足す
                dataText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }
            //　1回に表示するメッセージを表示した
        }
        else
        {

            elapsedTime += Time.deltaTime;

            //　クリックアイコンを点滅する時間を超えた時、反転させる


            //　マウスクリックされたら次の文字表示処理
            if (Input.GetMouseButtonDown(1))
            {
                nowTextNum = 0;
                messageNum++;
                dataText.text = "";

                elapsedTime = 0f;
                isOneMessage = false;

                //　メッセージが全部表示されていたらゲームオブジェクト自体の削除
                if (messageNum >= splitMessage.Length)
                {
                    isEndMessage = true;
                    talkIvent.EndTalkIvent();
                }
            }
        }
    }

    void SetMessage(string message)
    {
        this.allMessage = message;
        //　分割文字列で一回に表示するメッセージを分割する
        splitMessage = Regex.Split(allMessage, splitString);
        nowTextNum = 0;
        messageNum = 0;
        dataText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }
    //　他のスクリプトから新しいメッセージを設定
    public void SetMessagePanel(string message)
    {
        SetMessage(message);
    }
}
