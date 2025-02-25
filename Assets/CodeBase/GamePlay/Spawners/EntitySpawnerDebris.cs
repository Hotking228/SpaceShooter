using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnerDebris : MonoBehaviour
{
    [SerializeField] private Destructible[] m_DebrisPrefabs;

    [SerializeField] private int m_NumDebris;

    [SerializeField] private CircleArea m_CircleArea;

    [SerializeField] private float m_RandomSpeed;

    private void Start()
    {
        for (int i = 0; i < m_NumDebris; i++)
        {
            SpawnDebris();
        }
    }

    private void SpawnDebris()
    {
        int index = Random.Range(0, m_DebrisPrefabs.Length);

        GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

        debris.transform.position = m_CircleArea.GetRandomInsideZone();

        debris.GetComponent<Destructible>().Eventondeath.AddListener(OnDebrisDie);

        Rigidbody2D rb = debris.GetComponent <Rigidbody2D>();

        if (rb != null && m_RandomSpeed > 0)
        {
            rb.velocity = UnityEngine.Random.insideUnitCircle * m_RandomSpeed;
        }
    }

    private void OnDebrisDie()
    {

    }

}
