using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クエストに関する情報を格納した親クラス
/// クエストクリア条件は必ず実装
/// クエスト失敗条件はクエストごとに決める
/// </summary>
public abstract class QuestData : MonoBehaviour
{
    private NPCData NPCData=default;



    /// <summary>
    /// クエストの報酬
    /// </summary>
    [SerializeField]
    private int _questClereReward = 0;
    [SerializeField]
    private int _questFailedReward = 0;

    /// <summary>
    /// クエストの名前
    /// </summary>
    [SerializeField]
    private string _questName = "";

    /// <summary>
    /// クエストクリア
    /// </summary>
    public virtual void QuestClere()
    {
        //クエストクリア画面表示
        
    }
}
