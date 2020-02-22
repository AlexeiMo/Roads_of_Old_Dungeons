using UnityEngine;
using System.Threading;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float WalkSpeed = 3;
    public float JumpForce = 10;
   // private bool isGrounded = true;
    private bool isFacingRight = true;
    [SerializeField] public ItemScene itemScene;
    public Transform groundCheck;
    [SerializeField] Text text;
    [SerializeField] EquipmentManager equipmentManager;
    
    //private bool damage = true;
    private float groundRadius = 0.3f;
    public LayerMask enemy;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animatorController;
    [SerializeField] private Transform sword;

    [SerializeField] private HealthBar healthBar;

    public void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        _animatorController.SetFloat("Speed", Mathf.Abs(move));
       _rigidbody.velocity = new Vector2(move * WalkSpeed, _rigidbody.velocity.y);
        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();
    }
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
    }
    private void Flip()
    {
        
        isFacingRight = !isFacingRight;
   
        Vector3 theScale = _transform.localScale;
    
        theScale.x *= -1;
        
        _transform.localScale = theScale;
        
    }



    private bool isGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position,groundRadius);
        int j = 0;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Ground"))
                j++;
        }     
        return j > 0;
    }

    public void Jump()
    { 
        if(isGround())
            _rigidbody.AddForce(_transform.up * JumpForce, ForceMode2D.Impulse);      
    }


    public void Attack()
    {
      _animatorController.SetBool("Attack", true);     
    }

    public void noAttack()
    {
        _animatorController.SetBool("Attack", false);
    }

    public void Hit(float damage)
    {
        CustomItemAndGo item = equipmentManager.getItem(GearMainType.Armor);
        if (item == null)
            healthBar.AdjustCurrentValue(damage);
        else
        {
            healthBar.AdjustCurrentValue(damage/item.TheItem.mainStat.TheValue);
        }

    }
    public void Damage()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(sword.position, 0.3f, enemy);

        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].CompareTag("Enemy"))
            {
                CustomItemAndGo item = equipmentManager.getItem(GearMainType.Weapon);
                Enemy enemy = collider[i].GetComponent<Enemy>();
                if (item == null)
                   enemy.damageTake(-0.04f);               
                else                              
                   enemy.damageTake(-0.04f*item.TheItem.mainStat.TheValue);            
            }

        }

    }

    public void Health()
    {
        if (healthBar.currentValue == 0)
        {
            _animatorController.SetBool("Death", true);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

 /*   private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (Input.GetAxis("Pick") == 1)
            {
                collision.GetComponent<ItemScenePresenter>().PickUp();
            }

        }
    }*/

    private void Update()
    {
        //string gold = ShopManager.TestScenePlayerGold.ToString();
        //text.text = "Золото " + gold;
    }

    private void CastOn()
    {
        _animatorController.SetBool("Cast", false);
    }

}