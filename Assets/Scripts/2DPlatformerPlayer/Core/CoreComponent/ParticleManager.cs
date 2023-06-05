using UnityEngine;

/// <summary>
/// 粒子特效控制类
/// </summary>
public class ParticleManager : CoreComponent
{
    private Transform particleContainer;

    protected override void Awake()
    {
        base.Awake();

        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }

    public GameObject StartParticles(GameObject particlePrefab, Vector2 posistion, Quaternion rotation)
    {
        return Instantiate(particlePrefab, posistion, rotation, particleContainer);
    }

    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }

    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }
}
