using AmazingAssets.CurvedWorld.Example;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IDamagable
{
    [Header("������")]
    [SerializeField] Image healthSlider;
    [SerializeField] GameController gameController;
    [SerializeField] SoundController soundController;
    [SerializeField] PlayerCanvasController canvasController;
    Animator animator;
    PlayerMovement playerMovement;
    [SerializeField] WeaponMovementController weaponMovementController;
    [SerializeField] PlayerWeaponController weaponController;
    [SerializeField] Vector3 startCoordinates;
    [Header("��������������")]
    [SerializeField] float enemyDamage = 10;                     //����� - 10
    [SerializeField] float moveSpeed = 70;                       //����� - 70
    [SerializeField] float rotaionSpeed = 200;                    //����� - 200
    [SerializeField] float maxHealth;       
    [SerializeField] float currentHealth;
    float weaponDamage = 1;
    [SerializeField] float immortalityInterval = 1.5f;             //����� �����������
    public bool isImmortal;
    float immortalityTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        canvasController = GetComponentInChildren<PlayerCanvasController>();
    }
    void Start()
    {
        enemyDamage = Progress.Instance.playerInfo.playerDamage;
        rotaionSpeed = Progress.Instance.playerInfo.playerRotation;
        moveSpeed = Progress.Instance.playerInfo.playerSpeed;
        weaponMovementController.SetRotationSpeed(rotaionSpeed);       
        weaponController.SetEnemyDamage(enemyDamage);
        weaponController.SetWeaponDamage(weaponDamage);
        playerMovement.enabled = false;
        ResetHealth();
    }
    private void Update()
    {
        //�������� �� ������������
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
        canvasController.ShowDamageValueText(damageValue);
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
        //playerMovement.SwitchInput(false);
        playerMovement.enabled = false;
        animator.SetFloat("speed", 0);
        animator.SetBool("isDeath", true);
        GetComponent<Collider>().enabled = false;
        EventManager.InvokePlayerDied();
        weaponController.ShowWeaponModel(false);
        yield return new WaitForSeconds(2.5f);
        gameController.EndLevel(false);
        yield return null;
    }
    //�������������� ���������
    public void ResetPlayer()
    {       
        transform.position = Vector3.zero;
        animator.SetBool("isDeath", false);
        animator.SetFloat("speed", 0);
        ResetHealth();
        GetComponent<BoxCollider>().enabled = true;
        weaponController.ShowWeaponModel(true);
        isImmortal = false;
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

    //�������� �������� ������
    public void ChangeRotationSpeed(float rotationSpeedDiff)
    {
        rotaionSpeed += rotationSpeedDiff;
        Progress.Instance.playerInfo.playerRotation = rotaionSpeed;
        YandexSDK.Save();
        weaponMovementController.SetRotationSpeed(rotaionSpeed);
    }
    //�������� �����������
    public float GetMovementSpeed()
    {
        return moveSpeed;
    }
    public void ChangeMovementSpeed(float moveSpeedDiff)
    {
        moveSpeed += moveSpeedDiff;
        Progress.Instance.playerInfo.playerSpeed = moveSpeed;
        YandexSDK.Save();
        playerMovement.SetMovementSpeed(moveSpeed);
    }
    //���� �� ����������
    public float GetEnemyDamage()
    {
        return enemyDamage;
    }
    public void ChangeEnemyDamage(float damageDiff)
    {
        enemyDamage += damageDiff;
        Progress.Instance.playerInfo.playerDamage = enemyDamage;
        YandexSDK.Save();
        weaponController.SetEnemyDamage(enemyDamage);
    }
    //���� �� ������
    public float GetWeaponDamage()
    {
        return weaponDamage;
    }

}
