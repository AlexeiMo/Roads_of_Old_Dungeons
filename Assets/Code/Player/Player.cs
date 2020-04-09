using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animatorController;
    private bool isFacingRight = true;
    private bool doubleJump = false;
    private bool isBlockMove = false;
    private bool iAmAttacking = false;
    private bool iDealDamage = false;

    [Header("Передвижения игрока")]
    public float walkSpeed = 3;
    public float jumpForce = 10;
    public float speedUp;
    public float hOfPlayer;

    

    [Header("Маски")]
    public LayerMask enemy;
    public LayerMask ground;
    public string groundTag;

    [Header("Характеристики атаки")]
    public float damage;
    public float attackDistance = 1;



    [SerializeField] public ItemScenePresenter itemScene;
    [SerializeField] Text text;  
   // [SerializeField] EquipmentManager equipmentManager;

    public bool IsBlockMove { set => isBlockMove = value; }


    public event Action DealingDamage;

    public MyEvents TakeEX;


    public void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _rigidbody.velocity = new Vector2(move * walkSpeed, _rigidbody.velocity.y);
        _animatorController.SetFloat("Speed", Mathf.Abs(move));     
        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();
    }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        if (TakeEX == null)
        {
            TakeEX = new MyEvents();
        }
    }
    private void Start()
    {
    
    }

    private void Update()
    {
        if (iAmAttacking)
            Damage();           
    }

    private void Flip()
    {
        
        isFacingRight = !isFacingRight;
   
        Vector3 theScale = _transform.localScale;
    
        theScale.x *= -1;
        
        _transform.localScale = theScale;
        
    }

    public bool isGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 2f), Vector2.down, hOfPlayer, ground + enemy);
        if (raycastHit2D)
            return true;
        else return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y-2f), new Vector2(0,-1*hOfPlayer));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x,transform.position.y-1), new Vector2(transform.localScale.x * attackDistance, 0));
    }

    public void Jump()
    {
        if (isGround())
        {
            _rigidbody.AddForce(_transform.up * jumpForce, ForceMode2D.Impulse);
            doubleJump = false;
        }
        else if(!doubleJump)
        {
            _rigidbody.AddForce(_transform.up * jumpForce, ForceMode2D.Impulse);
            doubleJump = true;
        }                 
    }

    public void ExitStraits()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public void Attack()
    {
      _animatorController.SetBool("Attack", true);
       
    }

    public void AttackStart()
    {
        iAmAttacking = true;
    }

    public void noAttack()
    {
        _animatorController.SetBool("Attack", false);
        iAmAttacking = false;
        iDealDamage = false;
    }

    
    public void Damage()
    {
       RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-1), new Vector2(transform.localScale.x*attackDistance,0), attackDistance, enemy);
       Debug.DrawRay(new Vector2(transform.position.x, transform.position.y-1), new Vector2(transform.localScale.x*attackDistance,0));
       if(raycastHit2D&&!iDealDamage)
       {
           DealingDamage?.Invoke();
           TakeEX.Invoke(raycastHit2D.collider.GetComponent<IAlive>().TakeDamage(damage));
           iDealDamage = true;
       }

    }



    public void HealthLevelZero()
    {
         _animatorController.SetBool("Death", true);  
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void CastOn()
    {
        _animatorController.SetBool("Cast", false);
    }


    public void FallDown()
    {
        _rigidbody.AddForce(_transform.up * (-2), ForceMode2D.Impulse);
        if (isFacingRight)
            _rigidbody.velocity = new Vector2(-1*walkSpeed,  _rigidbody.velocity.y);
        else _rigidbody.velocity = new Vector2(1* walkSpeed, _rigidbody.velocity.y);

    }
}


public class MyEvents: UnityEvent<int>
{

}