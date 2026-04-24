using UnityEngine;

public class AttackAction
{
    private HitDetectionSystem _hitDetection;
    private StateTransitionSystem _stateTransitionSystem;
    private AnimationPlayer _animPlayer;
    private bool _canQueueNextAttack = false;
    private bool _queuedNextAttack = false;
    private int _comboIndex = 0;

    public AttackAction(StateTransitionSystem stateTransitionSystem, AnimationPlayer animPlayer, HitDetectionSystem hitDetection)
    {
        _stateTransitionSystem = stateTransitionSystem;
        _animPlayer = animPlayer;
        _hitDetection = hitDetection;

    }
    public void HandleLightAttack()
    {
        //Debug.Log("Light Attack");
        if (_canQueueNextAttack)
        {
            _queuedNextAttack = true;
            return;
        }
        StartAttack();
    }
    private void StartAttack()
    {
        //_stateTransitionSystem.EnterAttack();
        _comboIndex = Mathf.Clamp(_comboIndex, 1, 3);
        _animPlayer.PlayLightAttack(_comboIndex);
        Debug.Log($"comboIndex: {_comboIndex}");

        _canQueueNextAttack = false;
        _queuedNextAttack = false;

    }
    public void OpenComboWindow()
    {
        _canQueueNextAttack = true;
        //Debug.Log("Combo Window Opened");

    }
    public void CloseComboWindow()
    {
        _canQueueNextAttack = false;
        //Debug.Log("Combo Window Closed");
    }
    public void OnAttackEnd()
    {
        //Debug.Log("Attack Ended");
        if (_queuedNextAttack)
        {
            if(_comboIndex >= 3)
            {
                _comboIndex = 0;
            }
            else
            {
                _comboIndex++;
            }
            StartAttack();
            //Debug.Log("Combo Attack");
        }
        else
        {
            _comboIndex = 0;
            _animPlayer.CombatIdle();//TODO: Update this
            _stateTransitionSystem.ExitAttack();
        }
    }
    public void OnHit()
    {
        AttackInfo context = new AttackInfo
        {
            Attacker = _animPlayer.gameObject,
            Damage = 20
        };
        Debug.Log("Detecting Hit");
        _hitDetection.DetectHit(context);
    }

}
