using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


    /// <summary>
    /// Destructible object on scene. Smth that can have hitpoints
    /// </summary>
    public class Destructible : Entity
    {
        [SerializeField] private ParticleSystem deathEffectPrefab;
        #region Properties
        /// <summary>
        /// object ignores damage
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool m_IsAlwaysIndestrutible;
        public bool IsIndestructible => m_Indestructible;




    /// <summary>
    /// Start amount of hitpoints
    /// </summary>
    [SerializeField] private int m_HitPoints;
    public int MaxHitPoints => m_HitPoints;

    /// <summary>
    /// Current amount of hitpoints
    /// </summary>
    private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;
        #endregion

        #region Unity Event
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;

            transform.SetParent(null);
        }
        
        
        private void Update()
        {
            if (timer < indestructibleTime)
            {
                timer += Time.deltaTime;
            }
            if (timer >= indestructibleTime && !m_IsAlwaysIndestrutible)
            {
            m_Indestructible = false;
            }
        }

    #endregion

        #region Public API
    /// <summary>
    /// Apply damage to object
    /// </summary>
    /// <param name="damage"> Damage that will be apply to object</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

    private float timer;
    private float indestructibleTime;

    public void SetIndestructible(float time)
    {
        m_Indestructible = true;
        indestructibleTime = time;
        timer = 0;
    }
        #endregion

        /// <summary>
        /// virtual method of destroying of objects when hitpoint <= 0
        /// </summary>
        protected virtual void OnDeath()
        {
            if(deathEffectPrefab!=null)
            Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            m_EventOnDeath?.Invoke();
            Destroy(gameObject);
            
            
        }

    private static HashSet<Destructible> m_AllDestructibles;

    public static IReadOnlyCollection<Destructible> AllDestrucltibles => m_AllDestructibles;

    protected void OnEnable()
    {
        if (m_AllDestructibles == null)
        {
            m_AllDestructibles = new HashSet<Destructible>();
        }

        m_AllDestructibles.Add(this);
    }

    protected virtual void OnDestroy()
    {
        m_AllDestructibles.Remove(this);
    }

    public const int teamIdNeutral = 0;

    [SerializeField] private int m_TeamId;

    public int TeamId
    {
        get { return m_TeamId; }
        set { m_TeamId = value; }
    }

    //public int TeamId => m_TeamId;


    [SerializeField] private UnityEvent m_EventOnDeath;

    public UnityEvent Eventondeath => m_EventOnDeath;

    [SerializeField] private int m_ScoreValue;
    public int ScoreValue => m_ScoreValue;
    }