using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animatorController;
    private float move;
    private int delay;
    private bool damage = true;
    private int delay_anim = 0;
    [SerializeField] private float WalkSpeed;
    [SerializeField] private Transform attackHit;
    [SerializeField] private string tagEnd;
    [SerializeField] private string enemyTag;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;


    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        move = 1;
    }

    public void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 2f);
        if (groundInfo == false)
        {
            Debug.Log(gameObject.name + "Enter");
            this.Flip();
        }
        Health();
        if (delay_anim < 0)
            Attack();
        else
            delay_anim--;
        if (damage)
            Hit();

    }
    private void Flip()
    {
        Vector3 theScale = _transform.localScale;
        theScale.x *= -1;
        move *= -1;
        _transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")||collision.CompareTag("Enemy"))
            Flip();
    }

    private void FixedUpdate()
    {
        _animatorController.SetFloat("Speed", Mathf.Abs(move));
        _rigidbody.velocity = new Vector2(move * WalkSpeed, _rigidbody.velocity.y);
       
    }


    private void Hit()
    {
       Collider2D[] collider = Physics2D.OverlapCircleAll(attackHit.position, 0.5f, layerMask);
       for(int i = 0; i < collider.Length; i++)
       {
            if (collider[i].tag == enemyTag)
            {
                Player player = collider[i].GetComponent<Player>();
                player.Hit(-0.1f);
                damage = false;
            }
       }
 
    }


    private void  Attack()
    {
        if (!_animatorController.GetBool("isAttack"))
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector2(3*move, 0), 4, layerMask);
            if (hit.Length != 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].transform.CompareTag("Player"))
                    {
                        _animatorController.SetBool("isAttack", true);                           
                    }
                }
            }
        }
    }

    private void EndAttack()
    {
        damage = true;
        _animatorController.SetBool("isAttack", false);
        delay_anim = 60;
    }

    private bool Health()
    {
        if (healthBar.currentValue == 0)
        {
            _animatorController.SetBool("isDead", true);
            move = 0;
            return false;
        }
        return true;
    }


    public void damageTake(float damage)
    {
        healthBar.AdjustCurrentValue(damage);
        checkDamage();
    }
    private void checkDamage()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector2(move, 0), 4, layerMask);
        if (hit.Length == 0)
        {
            Flip();
        }
    }
    private void Dead()
    {
        List<Item> items = DatabaseManager.Items;
        int i = Random.Range(0, DatabaseManager.Items.Count);
        ItemScene itemScene = GameObject.Find("ItemScene").GetComponent<ItemScene>();
        ItemScene item =Instantiate(itemScene, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),new Quaternion());
        item.Present(DatabaseManager.Items[i]);
        ShopManager.TestScenePlayerGold += 1000;
        Destroy(gameObject);
    }


}

