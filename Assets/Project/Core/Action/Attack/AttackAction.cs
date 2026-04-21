using UnityEngine;

public class AttackAction
{
    private StateTransitionSystem _stateTransitionSystem;
    private AnimationPlayer _animPlayer;
    private bool _canQueueNextAttack = false;
    private bool _queuedNextAttack = false;
    private int _comboIndex = 0;
    
    public AttackAction(StateTransitionSystem stateTransitionSystem, AnimationPlayer animPlayer)
    {
        _stateTransitionSystem = stateTransitionSystem;
        _animPlayer = animPlayer;
    }
    public void HandleLightAttack()
    {
        Debug.Log("Light Attack");
        if (_canQueueNextAttack)
        {
            _queuedNextAttack = true;
            return;
        }
        StartAttack();
    }
    private void StartAttack()
    {
        //_transition.EnterAttack();
        _comboIndex = Mathf.Clamp(_comboIndex, 0, 2);
        //_animPlayer.PlayLightAttack(_comboIndex);
        _canQueueNextAttack = false;
        _queuedNextAttack = false;

    }
    public void OpenComboWindow()
    {
        _canQueueNextAttack = true;

    }
    public void CloseComboWindow()
    {
        _canQueueNextAttack = false;
    }
    private void OnAttackEnd()
    {
        if (_queuedNextAttack)
        {
            StartAttack();
            _comboIndex++;
        }
        else
        {
            _comboIndex = 0;
           //_transition.ExitAttack();
        }
    }
}
