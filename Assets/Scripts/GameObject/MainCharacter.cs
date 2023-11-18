using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UnityEngine.Rendering.Universal;

public class MainCharacter : MonoBehaviour {

    [SerializeField] float        m_speed = 4.0f;
    [SerializeField] float        m_jumpForce = 7.5f;
    [SerializeField] float        m_rollForce = 6.0f;
    [SerializeField] GameObject   m_slideDust;
    [SerializeField] Light2D      m_globalLight;

    public List<Weapon>           weapons = new List<Weapon>();

    private Animator              m_animator;
    private Rigidbody2D           m_body2d;
    private BoxCollider2D         m_collider2d;
    private bool                  m_grounded = false;
    private bool                  m_rolling = false;
    private bool                  runIdleIsPlayying = false;
    private int                   m_currentAttack = 0;
    private float                 m_timeSinceAttack = 0.0f;
    private float                 m_rollDuration = 8.0f / 14.0f;
    private float                 m_rollCurrentTime;
    private float                 m_fireRate = 0.25f;
    private bool                  m_invincible = false;
    private ReactiveProperty<int> m_maxHP = new ReactiveProperty<int>(5);
    private ReactiveProperty<int> m_curHP = new ReactiveProperty<int>(5);
    
    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_collider2d = GetComponent<BoxCollider2D>();

        if (weapons.Count <= 0)
        {
            weapons = GetComponentsInChildren<Weapon>().ToList();
        }
    }

    public void Init()
    {
        weapons.ForEach(x => x.Init());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!GameManager.Instance.IsGameStarted() || GameManager.Instance.IsGameOvered() || Time.timeScale <= 0) return;

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        //if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle Animations --
        //Death
        //if (Input.GetKeyDown("e") && !m_rolling)
        //{
        //    //m_animator.SetBool("noBlood", m_noBlood);
        //    m_animator.SetTrigger("Death");
        //}
            
        //Hurt
        //else if (Input.GetKeyDown("q") && !m_rolling)
        //    m_animator.SetTrigger("Hurt");

        //Attack
        if(Input.GetMouseButton(0) && m_timeSinceAttack > m_fireRate && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack >= weapons.Count)
                m_currentAttack = 0;

            weapons[m_currentAttack].Fire();
            // Reset Attack combo if time since last attack is too large
            //if (m_timeSinceAttack > 1.0f)
            //    m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            //m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;

        }


        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            weapons[0].SetPositionX(weapons[0].IsUltimate() ? 0.5f : 0.38f);
            weapons[1].SetPositionX(weapons[1].IsUltimate() ? -0.4f : -0.28f);
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            weapons[0].SetPositionX(weapons[0].IsUltimate() ? 0.4f : 0.28f);
            weapons[1].SetPositionX(weapons[1].IsUltimate() ? -0.5f : -0.38f);
        }

        if (Input.GetMouseButton(0))
        {
            // slow down
            inputX *= 0.4f;
            inputY *= 0.4f;

            // Get the mouse position on the screen
            //Vector3 mouseScreenPos = Input.mousePosition;

            //// Convert the mouse position to world position
            //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            //// Calculate the direction from the object to the mouse position
            //Vector3 direction = mouseWorldPos - transform.position;

            //// Calculate the angle between the object's forward vector and the direction
            //var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //if (Mathf.Abs(angle) > 90f)
            //{
            //    GetComponent<SpriteRenderer>().flipX = true;
            //}
            //else if (Mathf.Abs(angle) <= 90f)
            //{
            //    GetComponent<SpriteRenderer>().flipX = false;
            //}
        }

        // Move
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);


        //Run
        //m_animator.SetFloat("Speed", (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputY) > Mathf.Epsilon) ? 1 : 0);
        m_animator.SetFloat("Speed", Mathf.Abs(Mathf.Max(Mathf.Abs(inputX), Mathf.Abs(inputY))));

        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("RunIdleTrans"))
        {
            runIdleIsPlayying = true;
            if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                runIdleIsPlayying = false;
        }
        m_animator.SetBool("RunIdlePlayying", runIdleIsPlayying);
    }

    public void SetFireRate(float fireRate)
    {
        m_fireRate = fireRate;
    }

    public void RestoreHP(int hp)
    {
        m_curHP.Value += hp;
    }

    public void SetCurHP(int hp)
    {
        m_curHP.Value = hp;
    }

    public ReactiveProperty<int> SubscribeMaxHP()
    {
        return m_maxHP;
    }
    public ReactiveProperty<int> SubscribeCurrentHP()
    {
        return m_curHP;
    }

    public void ReceiveDamage(int dmg)
    {
        if (m_invincible) return;

        m_curHP.Value -= dmg;

        if (m_curHP.Value <= 0)
        {
            m_curHP.Value = 0;
            StartCoroutine(PerformDead());
        }
        else
        {
            StartCoroutine(PerformHurt());
        }
    }

    IEnumerator PerformDead()
    {
        m_invincible = true;

        m_animator.SetTrigger("Death");

        yield return new WaitForSeconds(1.5f);

        GameManager.Instance.GameOver();
    }

    IEnumerator PerformHurt()
    {
        m_invincible = true;

        m_animator.SetTrigger("Hurt");
        m_collider2d.excludeLayers = LayerMask.GetMask("Enemy");

        yield return new WaitForSeconds(0.6f);

        m_animator.SetTrigger("HurtEnded");

        yield return new WaitForSeconds(0.2f);

        m_collider2d.excludeLayers = LayerMask.GetMask();

        m_invincible = false;
    }

    public void EnterBossPhase()
    {
        m_globalLight.color = Color.red;
    }

    public void Reset()
    {
        m_invincible = false;
        m_globalLight.color = new Color32(45, 158, 255, 255);
        m_animator.SetTrigger("DeathEnded");
        RestoreHP((int)GameManager.Instance.GetMaxHP());
    }
}
