using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed = 1f;

    void OnEnable()
    {   
        Invoke("ProjectileDestroy", 2.0f);
    }

    void Update()
    {
        ProjectileMove();
    }

    private void ProjectileMove()
    {
        transform.position = transform.position + transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void ProjectileDestroy()
    {
        gameObject.SetActive(false);
    }

}
