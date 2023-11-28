using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Transform _attackEffectPoint;
    
    //Components
    public Animator _animator;
    private EnemyAttack _enemyAttack;
    
    //Managements
    private GameManager _gameManager;

    //Controllers
    private EffectController _effectController;

    void Awake()    
    {
        _enemyAttack = GetComponentInParent<EnemyAttack>();
        _animator = GetComponent<Animator>();
        _enemyAttack.ChangeAnimationAction += ChangeAnimation;
        _enemyAttack.ChangeAnimationGameOverAction += ChangeAnimationGameOver;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _effectController = _gameManager.effectController;
    }

    private void ChangeAnimation(string currentAnimation, bool parameter)
    {
        int hashCode = Animator.StringToHash(currentAnimation);
        _animator.SetBool(hashCode, parameter);
        if (currentAnimation.Equals("Attack"))
        {
            _enemyAttack.isAttacking = parameter;
        }
    }
    
    public void ChangeStateToTrueAnimation(string currentAnimation)
    {
        _animator.SetBool(currentAnimation, true);
        if (currentAnimation.Equals("Attack"))
        {
            _enemyAttack.isAttacking = true;
        }
    }
    
    public void ChangeStateToFalseAnimation(string currentAnimation)
    {
        _animator.SetBool(currentAnimation, false);
        if (currentAnimation.Equals("Attack"))
        {
            _enemyAttack.isAttacking = false;
        }
    }

    private void ChangeAnimationGameOver()
    {
        _animator.SetTrigger("GameOver");
    }

    public void CreateAttackEffect(float direction)
    {
    //     int attackIndex = 0;
    //     switch (_playerAttack.currentAttackMode)
    //     {
    //         case Attack.JAP :
    //             attackIndex = 0;
    //             break;
    //         case Attack.HOOK :
    //             attackIndex = 1;
    //             break;
    //         case Attack.UPPERCUT :
    //             attackIndex = 2;
    //             break;
    //     }
    //     if (direction > 0)
    //     {
    //          GameObject obj = _effectController.CreateEffect("PlayerAttackEffect", _attackEffectPointRight[attackIndex].position,
    //             _attackEffectPointRight[attackIndex].rotation, 1);
    //          obj.GetComponent<VisualEffect>().Play();
    //     }
    //     else if(direction < 0)
    //     {
    //         GameObject obj = _effectController.CreateEffect("PlayerAttackEffect", _attackEffectPointLeft[attackIndex].position,
    //             _attackEffectPointLeft[attackIndex].rotation, 1);
    //         obj.GetComponent<VisualEffect>().Play();
    //     }
    }
}
