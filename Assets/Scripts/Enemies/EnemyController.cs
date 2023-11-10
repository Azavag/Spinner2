using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyType
{ 
   Regular,
   Miniboss,
   Boss
}

public class EnemyController : MonoBehaviour, IDamagable, IKnockbackable
{
    [Header ("������ ����")]
    [SerializeField] protected Transform playerTransform;
    EnemyCanvasController canvasController;
    protected NavMeshAgent agent;
    protected Rigidbody rb;
    protected Animator animator;
    [SerializeField] protected AnimationClip deathClip;
    [SerializeField] protected GameObject attackControllerZone;
    EnemyAttackController attackController;
    [Header("��������������")]
    [SerializeField] EnemyType enemyType;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float startAgentSpeed;
    [SerializeField] protected float damage;
    [Header("���������")]
    protected bool isFollowPlayer;
    protected bool underGround;
    protected bool isAlive;
    protected bool isDeathAnimationEnd;
    protected Vector3 agentTarget;
    float spawnSpeed = 2f;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        canvasController = GetComponentInChildren<EnemyCanvasController>();
        attackController = attackControllerZone.GetComponent<EnemyAttackController>();

        EventManager.PlayerDied += OnPlayerDied;
        EventManager.GameReseted += OnGameReseted;
    }
    protected virtual void Start()
    {        
        rb.isKinematic = true;
        agent.speed = startAgentSpeed;
        isAlive = true;        
        NavmeshAgentState(false);
        attackControllerZone.SetActive(false);
    }  
    void Update()
    {
        if(underGround)
            CheckGround();
        else animator.SetFloat("speed", agent.velocity.sqrMagnitude);
       
    }  
    private void FixedUpdate()
    {
        //�������� � ������ �� NavMesh �� �����������
        if (isFollowPlayer)
        {
            SetTarget(playerTransform.position);
            agent.SetDestination(agentTarget);
        }
        //�������� � ������ ��� �����
        if (underGround)
            RiseToGround();

        if (isDeathAnimationEnd)
            MoveUnderground();
           
    }
    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }
    public virtual void RecieveDamage(float damageValue)
    {
        currentHealth -= damageValue;
        if (StaticSettings.CanShowDamageNumber())
            canvasController.ShowDamageValueText(damageValue);
        AnimateDamage();
        if (currentHealth <= 0 && isAlive)
        {            
            StartCoroutine(DeathProccess());
            return;
        }
        
    }
    public virtual IEnumerator DeathProccess()
    {
        DisableComponents();
               
        animator.SetTrigger("death");
        yield return new WaitForSeconds(deathClip.length);
        isDeathAnimationEnd = true;
        yield return null;

    }
    public void AnimateDamage()
    {
        animator.SetTrigger("damaged");
    }
    protected void DisableComponents()
    {
        isAlive = false;
        agent.speed = 0;
        agent.enabled = false;
        NavmeshAgentState(false);
        GetComponent<Collider>().enabled = false;
        EventManager.InvokeEnemyTypeDied(enemyType);
        EventManager.InvokeEnemyDied();
        Destroy(gameObject, 2 * deathClip.length);
    }
    protected void RiseToGround()
    {
        Vector3 movementVector = Vector3.up;
        rb.MovePosition(transform.position + movementVector * spawnSpeed * Time.deltaTime);
    }
    protected void CheckGround()
    {
        if (transform.position.y >= 0)
        {
            UnderGround(false);
            NavmeshAgentState(true);
            animator.SetTrigger("onGround");
            if(enemyType == EnemyType.Regular)
                attackControllerZone.SetActive(true);
        }
    }
    public void UnderGround(bool state)
    {
        underGround = state;
    }
    protected void MoveUnderground()
    {
        Vector3 movementVector = Vector3.down;
        rb.MovePosition(transform.position + movementVector * 0.5f * spawnSpeed * Time.deltaTime);
    }
    public void SetTargetTransform(Transform player)
    {
        playerTransform = player;
    }
    public float GetDamage()
    {
        return damage;
    }
    protected void SetTarget(Vector3 taregtVector)
    {
        agentTarget = taregtVector;
    }
    public void NavmeshAgentState(bool state)
    {
        //agent.enabled = state;
        if (state)
            agent.speed = startAgentSpeed;
        else agent.speed = 0;
        isFollowPlayer = state;
    }
    //� �������� EnemyAttackAnimation
    public void AttackPlayer()
    {
        attackController.TryAttackPlayer(damage);
    }
    //� �������� EnemyAttackAnimation
    public void CheckAttackEnd()
    {
        attackController.StopAttackAnimation();
    }
    void OnPlayerDied()
    {
        NavmeshAgentState(false);
        attackController.SetPlayerInZone(false);
    }
    protected virtual void OnGameReseted()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        EventManager.PlayerDied -= OnPlayerDied;
        EventManager.GameReseted -= OnGameReseted;
    }
  



    void StartKnockback()
    {
        //isFollowPlayer = false;
        //agent.speed = 0f;
        ////rb.isKinematic = false;
        //Vector3 knockbackDirection = (transform.position - playerTransform.position);
        //knockbackDirection.y = 0;
        //knockbackDirection.Normalize();
        ////rb.AddForce(knockbackDirection * knockbackPower, ForceMode.Impulse);
        //isBeingHit = true;
    }

    void EndKnockback()
    {
        ////rb.isKinematic = true;
        //agent.speed = startAgentSpeed;
        //isFollowPlayer = true;
        //isBeingHit = false;
    }  

    public void RecieveKnockback(float knockbackValue)
    {
        //knockbackPower = knockbackValue;
        //if (!isBeingHit)
        //{
        //    if (canKnockback)
        //    {
        //        StartKnockback();
        //    }
        //}
    }
}
