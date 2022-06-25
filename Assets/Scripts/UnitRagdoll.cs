using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] Transform ragdollRootBone = null;

    public void Setup(Transform originalRootBone)
    {
        MatchAllChildTransforms(originalRootBone, ragdollRootBone);

        ApplyExplosionToRagdoll(ragdollRootBone, 300f, transform.position, 10f);
    }

    private void MatchAllChildTransforms(Transform root, Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);

            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;
            }

            MatchAllChildTransforms(child, cloneChild);
        }
    }

    private void ApplyExplosionToRagdoll(Transform root, float explosionForce, Vector3 position, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, position, explosionRange);
            }

            ApplyExplosionToRagdoll(child, explosionForce, position, explosionRange);
        }
    }
}