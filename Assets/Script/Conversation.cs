using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Conversation", menuName = "CreateConversation")]
public class Conversation : ScriptableObject
{
    //　会話内容
    /*
     * 最大で3行まで。
     * それ以上記入する場合は<>を入れると分割できる
     * 
     * 記入例
     * こんにちは僕は村人だよ
     * 今はゲームを作っているんだ
     * 君は何をしているの<>え！？この街に来たばっかりなの！？
     * この街はたくさんお店があって楽しい場所だよ！
     * 色々見てみるといいよ！
     * 
     * 
     * ↓表示結果
     * こんにちは僕は村人だよ
     * 今はゲームを作っているんだ
     * 君は何をしているの
     * 
     * え！？この街に来たばっかりなの！？
     * この街はたくさんお店があって楽しい場所だよ！
     * 色々見てみるといいよ！
     */
    [SerializeField]
    [Multiline(100)]
    private string message = null;

    //　会話内容を返す
    public string GetConversationMessage()
    {
        return message;
    }
}