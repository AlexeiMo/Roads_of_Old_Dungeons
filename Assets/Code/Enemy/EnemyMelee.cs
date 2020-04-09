using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMelee : MonoBehaviour, IAlive
{

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator animatorController;
    private ItemDataBase dataBase;


    private float move = 1;
    
    private bool isDealDamage = false;
    private bool isStateAttack = false;
    private bool isICanAttack = false;
    private bool isFoundPlayer = false;
    private bool isCanRangeAttack = true;


    private float defaultSpeed;
    private int prefMove;


    [Header("Скорость")]
    public float walkSpeed;
    [Header("Радиус атак")]
    public float attackRangeMelee;
    public float attackRangeStartRangeAttack;
    public float attackRangeEndRangeAttack;
    
    [Header("Радиус поиска игрока")]
    public float searchRange;
    public LayerMask enemyLayer;
    [Header("Bars")]
    public float healthPoint;
    public HealthBar healthBar;
    public ItemScenePresenter itemScenePresenter;
    [Header("Урон")]
    public float damageOfEnemy;
    [Header("Задержка между атаками (сек)")]
    public float secDelayAttack;
    public float secDelayRangeAttack;


    [Header("Работа с землёй")]
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Снаряд")]
    public Bullet bullet;
    public float speedBullet;
    public float damageBullet;
    public string enemyTag;


    public int EX=100;
    private void Start()
    {
        healthBar.MaxHealthPoint = healthPoint;
        healthBar.currentValue = healthPoint;
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();

        defaultSpeed = walkSpeed;
        move = 1;
        prefMove = 1;
    }

    private void Update()
    {
        dataBase = GameObject.Find("ItemDataBase").GetComponent<ItemDataBase>();
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 2f, groundMask);
        if (!groundInfo)
            Flip();
        SearchPlayer();
        if (isFoundPlayer)
        {
            MeleeAttack();
            RangeAttack();
        }
        Health();
    }
   private void Flip()

    {
        Vector3 theScale = _transform.localScale;
        theScale.x *= -1;
        move *= -1;
        prefMove *= -1;
        _transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            Flip();
    }

    private void FixedUpdate()
    {
        animatorController.SetFloat("Speed", Mathf.Abs(move));
        _rigidbody.velocity = new Vector2(move * walkSpeed, _rigidbody.velocity.y);
    }

    private void SearchPlayer()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, new Vector2(move * searchRange, 0), searchRange, enemyLayer);
        Debug.DrawRay(transform.position, new Vector2(move * searchRange, 0),Color.green);
        if (raycastHit2D)
        {
            if (!isFoundPlayer)
            {
                isFoundPlayer = true;
                defaultSpeed = walkSpeed;
                walkSpeed *= 3;
                animatorController.SetBool("Run", true);
            }
        }
        else
        {
            if (isFoundPlayer)
            {
                walkSpeed = defaultSpeed;
                isFoundPlayer = false; 
                animatorController.SetBool("Run", false);
            }
           
        }
    }
  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRangeMelee);


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, searchRange);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, new Vector2(move, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, new Vector2(move * searchRange, 0));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x + move * attackRangeStartRangeAttack, transform.position.y),
            new Vector2(move* attackRangeEndRangeAttack-attackRangeStartRangeAttack, 0));
    }



    private void DealingDamage()
    {
       RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, new Vector2(move*attackRangeMelee,0), attackRangeMelee, enemyLayer);
       //Debug.DrawRay(transform.position, new Vector2(move*attackRangeMelee, 0),Color.green);
       

       if (raycastHit2D&&!isDealDamage)
       {
          isDealDamage = true;   
          raycastHit2D.collider.GetComponent<IAlive>().TakeDamage(damageOfEnemy);
       }
 
    }

    IEnumerator SleepRangeAttack()
    {
        yield return new WaitForSeconds(secDelayRangeAttack);

        isCanRangeAttack = true;
    }

   IEnumerator SleepMeleeAttack()
   {
       yield return new WaitForSeconds(secDelayAttack);

       isICanAttack = true;
       isStateAttack = false;
   }

    private void  MeleeAttack()
    {
        if (!isStateAttack)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, new Vector2(move * attackRangeMelee, 0), attackRangeMelee, enemyLayer);
            //Debug.DrawRay(transform.position, new Vector2(move * attackRange, 0));
            if (raycastHit2D)
            {
                animatorController.SetBool("isAttack",true);
                isStateAttack = true;
                prefMove =(int) move;
                move = 0;
            }
        }
        if (!isDealDamage && isICanAttack && isStateAttack)
            DealingDamage();
    }

    private void RangeAttack()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2( transform.position.x + move * attackRangeStartRangeAttack, transform.position.y), 
            new Vector2(move * attackRangeEndRangeAttack - attackRangeStartRangeAttack, 0), attackRangeStartRangeAttack - attackRangeEndRangeAttack);
        Debug.DrawRay(new Vector2(transform.position.x + move * attackRangeStartRangeAttack, transform.position.y),
            new Vector2(move * attackRangeEndRangeAttack - attackRangeStartRangeAttack, 0));
        if (raycastHit2D)
        {
            if (isCanRangeAttack && raycastHit2D.collider.IsTouchingLayers(enemyLayer))
            {
                isCanRangeAttack = false;
                Bullet createdBullet = Instantiate(bullet, transform.position, new Quaternion());
                createdBullet.Speed = speedBullet;
                createdBullet.Enemy = enemyTag;
                createdBullet.Damage = damageBullet;
                createdBullet.Creator = gameObject.tag;
                if (move == 0)
                    createdBullet.Move = prefMove;
                else
                    createdBullet.Move = move;
                StartCoroutine(SleepRangeAttack());
            }
        }
    }


    private void AttackStart()
    {
        isICanAttack = true;
    }
    private void EndAttack()
    {
        isDealDamage = false;
            move = prefMove;
        animatorController.SetBool("isAttack", false);
        StartCoroutine(SleepMeleeAttack());
    }

    private bool Health()
    {
        if (healthBar.currentValue == 0)
        {
            animatorController.SetBool("isDead", true);
            move = 0;
            return false;
        }
        return true;
    }


    public int TakeDamage(float damage)
    {
        healthBar.MinusCurrentValue(damage);
        if (checkDamage())
        {
            return EX;
        }
        else
            return 0;
    }

    IEnumerator SleepFlip()
    {
        yield return new WaitForSeconds(1f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-move, 0), 4, enemyLayer);
        if (hit)
        {
            Flip();
        }

    }
    private bool checkDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-move, 0), 4, enemyLayer);
        if (hit)
        {
            StartCoroutine(SleepFlip());
        }
        return healthBar.currentValue < 0; 
    }


    private void Dead()
    {
         List<Item> items = dataBase.items;
         int i = Random.Range(0, dataBase.items.Count);
         ItemScenePresenter item =Instantiate(itemScenePresenter, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),new Quaternion());
         item.CreateItemOnScene(dataBase.items[i]);
         Destroy(gameObject);
    }
   
}

