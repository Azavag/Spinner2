using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IDamagable
{
    [Header("Ссылки")]
    [SerializeField] Image healthSlider;
    [SerializeField] GameController gameController;
    [SerializeField] SoundController soundController;
    Animator animator;
    Rigidbody rb;
    PlayerMovement playerMovement;
    [SerializeField] WeaponMovementController weaponMovementController;
    [SerializeField] PlayerWeaponController weaponController;
    [SerializeField] Vector3 startCoordinates;
    [Header("Характеристики")]
    [SerializeField] float enemyDamage;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotaionSpeed;                    //Старт 200
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    float weaponDamage = 1;
    [SerializeField] float immortalityInterval = 1.5f;             //Время неузвимости
    bool isImmortal;
    float immortalityTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Start()
    {       
        weaponMovementController.SetRotationSpeed(rotaionSpeed);       
        weaponController.SetEnemyDamage(enemyDamage);
        weaponController.SetWeaponDamage(weaponDamage);
        
        ResetHealth();
    }
    private void Update()
    {
        //Проверка на неуязвимость
        if(isImmortal)
        {
            immortalityTimer -= Time.deltaTime;
            if(immortalityTimer <= 0 )
                ChangeImmortalityState();
        }
    }

    public void RecieveDamage(float damageValue)
    {
        if (isImmortal)
            return;

        currentHealth -= damageValue;
        healthSlider.fillAmount = currentHealth / maxHealth;
        soundController.Play("PlayerTakeDamage");
        if (currentHealth <= 0)
        {
            StartCoroutine(DeathProccess());
            return;
        }
        ChangeImmortalityState();
    }
    IEnumerator DeathProccess()
    {
        playerMovement.SwitchInput(false);
        animator.SetBool("isDeath", true);
        GetComponent<Collider>().enabled = false;
        EventManager.InvokePlayerDied();
        weaponController.ShowWeaponModel(false);
        yield return new WaitForSeconds(3f);
        gameController.EndLevel(false);
        yield return null;
    }
    //Восстановление персонажа
    public void ResetPlayer()
    {
        weaponController.ShowWeaponModel(true);
        animator.SetBool("isDeath", false);
        ResetPosition();
        ResetHealth();
        GetComponent<BoxCollider>().enabled = true;
    }
    public void ResetPosition()
    {
        transform.position = startCoordinates;
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthSlider.fillAmount = 1;
    }
    void ChangeImmortalityState()
    {
        isImmortal = !isImmortal;
        if (isImmortal)
        {
            immortalityTimer = immortalityInterval;
        }
        animator.SetBool("isImmortal", isImmortal);

    }
    public bool GetInvulmmortality()
    {
        return isImmortal;
    }
    //Скорость вращения оружия
    public void ChangeRotationSpeed(float rotationSpeedDiff)
    {
        rotaionSpeed += rotationSpeedDiff;
        weaponMovementController.SetRotationSpeed(rotaionSpeed);
    }
    //Скорость перемещения
    public float GetMovementSpeed()
    {
        return moveSpeed;
    }
    public void ChangeMovementSpeed(float moveSpeedDiff)
    {
        moveSpeed += moveSpeedDiff;
        playerMovement.SetMovementSpeed(moveSpeed);
    }
    //Урон по противнику
    public float GetEnemyDamage()
    {
        return enemyDamage;
    }
    public void ChangeEnemyDamage(float enemyDamageDiff)
    {
        enemyDamage += enemyDamageDiff;
        weaponController.SetEnemyDamage(enemyDamage);
    }
    //урон по оружию(Скорее всего статичный = 1)
    public float GetWeaponDamage()
    {
        return weaponDamage;
    }

}
