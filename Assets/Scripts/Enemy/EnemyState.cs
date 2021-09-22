using UnityEngine;
using System;
using UniRx;


namespace EnemyState
{
    /// <summary>
    /// �X�e�[�g�̎��s���Ǘ�����N���X
    /// </summary>
    public class StateManager
    {
        //�X�e�[�g�{��
        public ReactiveProperty<EnemyState> State { get; set; } = new ReactiveProperty<EnemyState>();

        //���s�u���b�W
        public void Execute() => State.Value.Execute();
    }

    /// <summary>
    /// �X�e�[�g�̃N���X
    /// </summary>
    public abstract class EnemyState
    {
        //�f���Q�[�g
        public Action ExecAction { get; set; }

        //���s����
        public virtual void Execute()
        {
            if (ExecAction != null) ExecAction();
        }

        //�X�e�[�g�����擾���郁�\�b�h
        public abstract string GetStateName();
    }

    //=================================================================================
    //�ȉ���ԃN���X
    //=================================================================================

    /// <summary>
    /// �������Ă��Ȃ����
    /// </summary>
    public class EnemyStateIdle : EnemyState
    {
        public override string GetStateName()
        {
            return "State:Idle";
        }
    }

    /// <summary>
    /// �����Ă�����
    /// </summary>
    public class EnemyStateWalk :EnemyState
    {
        public override string GetStateName()
        {
            return "State:Walk";
        }
    }

    /// <summary>
    /// �����Ă�����
    /// </summary>
    public class EnemyStateRun : EnemyState
    {
        public override string GetStateName()
        {
            return "State:Run";
        }
    }

    /// <summary>
    /// ���������Ă�����
    /// </summary>
    public class EnemyStateSideWalk : EnemyState
    {
        public override string GetStateName()
        {
            return "State:SideWalk";
        }
    }

}