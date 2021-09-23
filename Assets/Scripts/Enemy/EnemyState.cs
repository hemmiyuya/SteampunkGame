using UnityEngine;
using System;
using UniRx;


namespace EnemyState
{
    /// <summary>
    /// ステートの実行を管理するクラス
    /// </summary>
    public class StateManager
    {
        //ステート本体
        public ReactiveProperty<EnemyState> State { get; set; } = new ReactiveProperty<EnemyState>();

        //実行ブリッジ
        public void Execute() => State.Value.Execute();
    }

    /// <summary>
    /// ステートのクラス
    /// </summary>
    public abstract class EnemyState
    {
        //デリゲート
        public Action ExecAction { get; set; }

        //実行処理
        public virtual void Execute()
        {
            if (ExecAction != null) ExecAction();
        }

        //ステート名を取得するメソッド
        public abstract string GetStateName();
    }

    //=================================================================================
    //以下状態クラス
    //=================================================================================

    /// <summary>
    /// 何もしていない状態
    /// </summary>
    public class EnemyStateIdle : EnemyState
    {
        public override string GetStateName()
        {
            return "State:Idle";
        }
    }

    /// <summary>
    /// 歩いている状態
    /// </summary>
    public class EnemyStateWalk :EnemyState
    {
        public override string GetStateName()
        {
            return "State:Walk";
        }
    }

    /// <summary>
    /// 走っている状態
    /// </summary>
    public class EnemyStateRun : EnemyState
    {
        public override string GetStateName()
        {
            return "State:Run";
        }
    }

    /// <summary>
    /// 横歩きしている状態
    /// </summary>
    public class EnemyStateSideWalk : EnemyState
    {
        public override string GetStateName()
        {
            return "State:SideWalk";
        }
    }

}