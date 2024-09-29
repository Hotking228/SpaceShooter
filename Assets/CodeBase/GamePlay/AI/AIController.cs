using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpaceShip))]
public class AIController : MonoBehaviour
{
   
    public enum AIBehaviour
    {
        Null,
        Patrol
    }

    public enum PatrolBehaviour
    {
        Path,
        Random
    }

    public enum FollowTargetMode
    {
        Follow,
        PreEmption
    }

    [SerializeField] private PatrolBehaviour m_PatrolBehaviour;
    [SerializeField] private Transform[] m_PatrolPath;

    [SerializeField] private FollowTargetMode m_FollowTargetMode;


    [SerializeField] private AIBehaviour m_AIBehaviour;


    [SerializeField] private AIPointerPatrol m_AIPatrolPoint;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_NavigationLinear;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_NavigationAngular;

    [SerializeField] private float m_RandomSelectMovePointTime;

    [SerializeField] private float m_FindNewTargetTime;

    [SerializeField] private float ShootDelay;

    [SerializeField] private float m_EvadeRayLength;

    [SerializeField] private float fireDistance;

    private SpaceShip m_SpaceShip;

    private Vector3 m_MovePosition;

    private Destructible m_SelectedTarget;

    private Timer m_RandomizeDirectionTimer;
    private Timer m_FindNewTargetTimer;
    private Timer m_FireTimer;
    private void Start()
    {
        if (fireDistance == 0)
            fireDistance = 10.0f;
        m_AIPatrolPoint = FindAnyObjectByType<AIPointerPatrol>();
        currentPointId = 0;
        if (m_PatrolPath == null || m_PatrolPath.Length ==0)
        {
            m_PatrolBehaviour = PatrolBehaviour.Random;
        }
        m_SpaceShip = GetComponent<SpaceShip>();
        InitTimers();
    }

    private void Update()
    {
        UpdateTimers();

        UpdateAI();
    }

    private void UpdateAI()
    {
       

        if (m_AIBehaviour == AIBehaviour.Patrol)
        {
            UpdateBehaviourPatrol();
        }
    }

    public void UpdateBehaviourPatrol()
    {
        ActionFindNewMovePosition();
        ActionControlShip();
        ActionFindNewAttackTarget();
        ActionFire();
        ActionEvadeCollision();
    }

    private SpaceShip targetShip;


    private int currentPointId;
    private float pathPatrolPointradius = 0.5f;
    private void ActionFindNewMovePosition()
    {
        
        if (m_AIBehaviour == AIBehaviour.Patrol)
        {
            if (m_SelectedTarget != null)
            {
                if (m_FollowTargetMode == FollowTargetMode.PreEmption)
                {
                    m_MovePosition = (Vector2)FindPreEmptionPoint(Time.deltaTime * targetShip.transform.up * targetShip.Thrust * targetShip.ThrustControl, transform.up * Time.deltaTime * m_SpaceShip.Thrust * m_SpaceShip.ThrustControl, m_SelectedTarget.transform.position, transform.position);
              
                }
                if (m_FollowTargetMode == FollowTargetMode.Follow)
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
            }
            else
            {
                if(m_PatrolBehaviour == PatrolBehaviour.Path)
                {
                    m_MovePosition = m_PatrolPath[currentPointId].position;
                  
                    if ((transform.position - m_PatrolPath[currentPointId].position).magnitude <= pathPatrolPointradius)
                    {
                        currentPointId++;
                        if (currentPointId >= m_PatrolPath.Length)
                        {
                            currentPointId = 0;
                        }
                    }
                }
                if (m_PatrolBehaviour == PatrolBehaviour.Random)
                {
                    if (m_AIPatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_AIPatrolPoint.transform.position - transform.position).sqrMagnitude < m_AIPatrolPoint.Radius * m_AIPatrolPoint.Radius;
                        if (isInsidePatrolZone)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_AIPatrolPoint.Radius + m_AIPatrolPoint.transform.position;
                                
                                m_MovePosition = newPoint;
                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_AIPatrolPoint.transform.position;
                        }
                    }
                }
            }
        }
        
    }

    private Vector2 FindPreEmptionPoint(Vector2 targetVelocity, Vector2 shipVelocity, Vector2 target, Vector2 ship)
    {
        Vector2 relativeVelocity =  shipVelocity - targetVelocity;

        float pathTime = (target - ship).magnitude / relativeVelocity.magnitude;


        Vector2 targetPoint = (pathTime * shipVelocity.magnitude * targetVelocity.normalized) + target;


        return targetPoint;
    }
    private void ActionEvadeCollision()
    {
        if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength))
        {
            m_MovePosition = transform.position + transform.right * 100.0f;
        }
    }

    private void ActionControlShip()
    {
        m_SpaceShip.ThrustControl = m_NavigationLinear;

        m_SpaceShip.TorqueControl = ComputeAlignTorqueNormalized(m_MovePosition, m_SpaceShip.transform ) * m_NavigationAngular;
    }


    private const float MAX_ANGLE = 45.0f;
    private static float angle;
    private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship)
    {
        Vector2 localTargetPostion = ship.InverseTransformPoint(targetPosition);

        angle = Vector3.SignedAngle(localTargetPostion, Vector3.up, Vector3.forward);

    

        angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

        return -angle;
    }

    private void ActionFindNewAttackTarget()
    {
        if (m_FindNewTargetTimer.IsFinished)
        {
            m_SelectedTarget = FindNearestDestructibleTarget();

            m_FindNewTargetTimer.Start(m_FindNewTargetTime);
        }
    }
    

    private void ActionFire()
    {
        if (m_SelectedTarget != null)
        {
            if (m_FireTimer.IsFinished)
            {
                if (angle < 45 && angle > -45 && (m_SelectedTarget.transform.position - transform.position).magnitude <= fireDistance)
                {

                    m_SpaceShip.Fire(TurretMode.Primary);

                    m_FireTimer.Start(ShootDelay);
                }
            }
        }
    }

    private Destructible FindNearestDestructibleTarget()
    {
        float minDist = float.MaxValue;

        Destructible potentialTarget = null;

        foreach (var v in Destructible.AllDestrucltibles)
        {
            if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

            if (v.TeamId == Destructible.teamIdNeutral) continue;

            if (v.TeamId == m_SpaceShip.TeamId) continue;

            float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                potentialTarget = v;
            }
        }
        if (potentialTarget != null)
            targetShip = potentialTarget.GetComponent<SpaceShip>();
        return potentialTarget;
    }


    #region Timers

    private void InitTimers()
    {
        m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
        m_FireTimer = new Timer(ShootDelay);
        m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
    }

    private void UpdateTimers()
    {
        m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
        m_FireTimer.RemoveTime(Time.deltaTime);
        m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
    }

    #endregion

    public void SetPatrolBehaviour(AIPointerPatrol point)
    {
        m_AIBehaviour = AIBehaviour.Patrol;
        m_AIPatrolPoint = point;
    }

}
