using PaintIn3D;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.CanvasScaler;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehavior : MonoBehaviour
{
    // Visuals
    [SerializeField] private MeshRenderer m_Mesh = null;
    [SerializeField] private Material m_StandardMaterial = null;

    // Animation
    //[SerializeField] private Animator m_Animator = null;

    // Rigidbody
    private Rigidbody m_Rigidbody = null;

    // NavMeshAgent
    public NavMeshAgent m_NavMeshAgent = null;
    private float m_NormalSpeed = 0;

    // Group
    public GroupLeader m_Leader = null;

    // -------------------------
    // START & UPDATE
    // -------------------------

    private void Start()
    {
        // Change material instance
        //m_Mesh.material = m_StandardMaterial;

        // Set Rigidbody reference
        m_Rigidbody = GetComponent<Rigidbody>();

        // Set NavMeshAgent reference
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_NormalSpeed = m_NavMeshAgent.speed;
    }

    private void FixedUpdate()
    {
        // Set the animator velocity
        //m_Animator.SetFloat("Velocity", m_NavMeshAgent.velocity.magnitude);
    }


    // -------------------------
    // GETTERS & SETTERS
    // -------------------------

    // Set a new target
    public void SetTarget(Vector3 target)
    {
        //m_NavMeshAgent.speed = adjustSpeed;
        m_NavMeshAgent.SetDestination(target);
    }

    // Get the units speed
    public float GetNormalSpeed()
    {
        return m_NormalSpeed;
    }

    // If our speed is lower than given speed, set new speed
    public void SetMinimumSpeed(float speed)
    {
        if (m_NavMeshAgent.speed < speed)
        {
            m_NavMeshAgent.speed = speed;
        }
    }

    // Get current leader
    public GroupLeader GetLeader()
    {
        return m_Leader;
    }

    // Set new leader
    public void SetLeader(GroupLeader leader)
    {
        // If we remove leader, set speed back to normal
        if (leader == null)
        {
            m_NavMeshAgent.speed = m_NormalSpeed;
        }

        m_Leader = leader;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //other.GetComponent<UnitBehavior>().m_Leader.units.TrimExcess();
            //other.gameObject.SetActive(false);
            //gameObject.SetActive(false);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    // Change mesh material
    //public void SetMaterial(Material material = null)
    //{
    //    if (material == null)
    //    {
    //        m_Mesh.material = m_StandardMaterial;
    //    }
    //    else
    //    {
    //        m_Mesh.material = material;
    //    }
    //}
}
