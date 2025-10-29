using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] PlayerInputHandler inputHandler;
    [SerializeField] PlayerAnimController animController;
    [SerializeField] float comboResetTime = 1f;
    [SerializeField] float inputBufferTime = 0.3f;
    float clipLength;
    float clipSpeed;
    float timePassed;
    private int _comboIndex = 0;
    private float _lastAttackTime;
    private bool _canAttack = true;
    private bool _bufferedAttack;
    private bool _reset = false;

    private void OnEnable()
    {
        inputHandler.OnLightAttack += LightAttack;
    }
    private void OnDisable()
    {
        inputHandler.OnLightAttack -= LightAttack;
    }
    private void Update()
    {
        /*if (Time.time - _lastAttackTime > comboResetTime)
        {
            //_comboIndex = 0; //TODO : When removed this line the second combo attack started playing.
            _comboIndex = 0;
            animController.ResetAttack();
        }*/
        timePassed = Time.deltaTime;//TODO
        if(!(timePassed > clipLength / clipSpeed))
        {
            animController.ResetAttack();

        }
    }
    void LightAttack()
    {
        if (!_canAttack)
        {
            StartCoroutine(BufferNextAttack());
            return;
        }

        if (_reset) 
        {
            _comboIndex = 0;
            _reset = false;
        }
        
        _comboIndex++;
        _comboIndex = Mathf.Clamp(_comboIndex, 1, 3);
        print(_comboIndex);
        animController.LightAttack("Attack" + _comboIndex);
        clipLength = animController.playerAnimator.GetCurrentAnimatorClipInfo(1)[0].clip.length;//TODO
        clipSpeed = animController.playerAnimator.GetCurrentAnimatorStateInfo(1).speed;//TODO
        _lastAttackTime = Time.deltaTime;
        _canAttack = false;
    }
    IEnumerator BufferNextAttack()
    {
        _bufferedAttack = true;
        yield return new WaitForSeconds(inputBufferTime);
        _bufferedAttack = false;
    }
    public void ResetNewAttackWindow()
    {
        _canAttack = true;
        if (_bufferedAttack) 
        {
            _bufferedAttack = false;
            LightAttack();
        }
        else if(_comboIndex >= 3)
        {
            ResetCombo();
        }
    }
    public void ResetCombo()
    {
        _comboIndex = 0;
        _canAttack = true;
    }
}
