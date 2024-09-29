using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : Destructible
{
    /// <summary>
    /// Mass of automatic station of rigid
    /// </summary>
    [Header("Space Ship")]
    [SerializeField] private float m_Mass;

    /// <summary>
    /// Forward force
    /// </summary>
    [SerializeField] private float m_Thrust;
    public float Thrust => m_Thrust;

    /// <summary>
    /// Rotation force
    /// </summary>
    [SerializeField] private float m_Mobility;

    /// <summary>
    /// Max linear velocity
    /// </summary>
    [SerializeField] private float m_MaxLinearVelocity;


    /// <summary>
    /// Max angular velocity grad/sec
    /// </summary>
    [SerializeField] private float m_MaxAngularVelocity;

    /// <summary>
    /// link on rigid
    /// </summary>
    private Rigidbody2D m_Rigid;

    public float MaxLinearVelocity => m_MaxLinearVelocity;
    public float MaxAngularVelocity => m_MaxAngularVelocity;

    [SerializeField] private Sprite m_PreviewImage;
    public Sprite PreviewImage => m_PreviewImage;


    #region API

    /// <summary>
    /// Control of linear force from -1 to 1
    /// </summary>
    public float ThrustControl { get; set; }

    /// <summary>
    /// Control of angular force from -1 to 1
    /// </summary>
    public float TorqueControl { get; set; }


    #endregion
    #region Unity Event
    private void Awake()
    {
        transform.SetParent(null);
    }
    protected override void Start()
    {
        base.Start();
        
        m_Rigid = GetComponent<Rigidbody2D>();
        m_Rigid.mass = m_Mass;
        startThrust = m_Thrust;
        m_Rigid.inertia = 1;
        InitOffensive();
    }

    
    private void FixedUpdate()
    {
        UpdateRigidbody();

        UpdateEnergyRegen();
    }

    #endregion
    /// <summary>
    /// Add forces to ship for moving
    /// </summary>
    private void UpdateRigidbody()
    {
        m_Rigid.AddForce(transform.up * Time.fixedDeltaTime *  m_Thrust * ThrustControl, ForceMode2D.Force);

        m_Rigid.AddForce(-Time.fixedDeltaTime * m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity), ForceMode2D.Force);


        m_Rigid.AddTorque(Time.fixedDeltaTime * m_Mobility * TorqueControl, ForceMode2D.Force);

        m_Rigid.AddTorque(-Time.fixedDeltaTime  * m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity), ForceMode2D.Force);

    }
    
    private float startThrust;

    private bool isBoosted;
    private float timerBoost;
    private float boostTime;
    public void SetSpeedBoost(float newThrust, float boostTime)
    {
        m_Thrust = newThrust;
        this.boostTime = boostTime;
        timerBoost = 0;
        isBoosted = true;
    }

    private void Update()
    {
        if (isBoosted) 
        { 
            if (timerBoost < boostTime)
            {
                timerBoost += Time.deltaTime;
            }
            if (timerBoost >= boostTime)
            {
                isBoosted = false;
                m_Thrust = startThrust;

            }
        
        }
    }
    
    [SerializeField] private Turret[] m_Turrets;
    public void Fire(TurretMode mode)
    {
        for (int i = 0; i < m_Turrets.Length; i++   )
        {
            if (m_Turrets[i].Mode == mode)
            {
                m_Turrets[i].Fire();
            }
        }
    }

    [SerializeField] private int m_MaxEnergy;
    [SerializeField] private int m_MaxAmmo;
    [SerializeField] private int m_EnergyRegenPerSecond;

    private float m_PrimaryEnergy;
    private int m_SecondaryAmmo;

    public void AddEnergy(int e)
    {


        m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
    }

    public void AddAmmo(int ammo)
    {
        m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
    }

    private void InitOffensive()
    {
        m_PrimaryEnergy = m_MaxEnergy;
        m_SecondaryAmmo = m_MaxAmmo;
    }

    private void UpdateEnergyRegen()
    {
        m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime, 0, m_MaxEnergy);
    }

    public bool DrawAmmo(int count)
    {
        if (count == 0) return true;

        if (m_SecondaryAmmo >= count)
        {
            m_SecondaryAmmo -= count;
            return true;
        }

        return false;
    }

    public bool DrawEnergy(int count)
    {
        if (count == 0) return true;

        if (m_PrimaryEnergy >= count)
        {
            m_PrimaryEnergy -= count;
            return true;
        }

        return false;
    }
    public void AssignWeapon(TurretProperties props)
    {
        for (int i = 0; i < m_Turrets.Length; i++)
        {
            m_Turrets[i].AssignLoadout(props);
        }
    }
}

