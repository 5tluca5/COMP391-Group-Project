using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UniRx;

public class MainCharacter : MonoBehaviour {

    [SerializeField] float        m_speed = 4.0f;
    [SerializeField] float        m_jumpForce = 7.5f;
    [SerializeField] float        m_rollForce = 6.0f;
    [SerializeField] GameObject   m_slideDust;

    public List<Weapon>           weapons = new List<Weapon>();

    private Animator              m_animator;
    private Rigidbody2D           m_body2d;
    private bool                  m_grounded = false;
    private bool                  m_rolling = false;
    private bool                  runIdleIsPlayying = false;
    private int                   m_currentAttack = 0;
    private float                 m_timeSinceAttack = 0.0f;
    private float                 m_rollDuration = 8.0f / 14.0f;
    private float                 m_rollCurrentTime;
    private float                 m_fireRate = 0.25f;
    private ReactiveProperty<int> m_maxHP = new ReactiveProperty<int>(5);
    private ReactiveProperty<int> m_curHP = new ReactiveProperty<int>(3);
    
    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        if(weapons.Count <= 0)
        {
            weapons = GetComponentsInChildren<Weapon>().ToList();
        }
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        if (!GameManager.Instance.IsGameStarted()) return;

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

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        
        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            weapons[0].SetPositionX(0.38f);
            weapons[1].SetPositionX(-0.28f);
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            weapons[0].SetPositionX(0.28f);
            weapons[1].SetPositionX(-0.38f);
        }

        // Move
        if (!m_rolling )
            m_body2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            //m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }
            
        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt");

        //Attack
        else if(Input.GetMouseButton(0) && m_timeSinceAttack > m_fireRate && !m_rolling)
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

        //Run
        m_animator.SetFloat("Speed", (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputY) > Mathf.Epsilon) ? 1 : 0);

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

    //public void InitHP(float hp)
    //{
    //    m_maxHP = hp;
    //    m_curHP = hp;
    //}

    //public void SetCurrentHP(float hp)
    //{
    //    m_curHP = hp;
    //}
    //public void SetMaxHP(float hp)
    //{
    //    m_maxHP = hp;
    //}
    public ReactiveProperty<int> SubscribeMaxHP()
    {
        return m_maxHP;
    }
    public ReactiveProperty<int> SubscribeCurrentHP()
    {
        return m_curHP;
    }
}
