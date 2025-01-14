using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (mousePos - transform.position);

        var angle = Mathf.Atan2(direction.y, direction.x) * 180f / Mathf.PI + offset;
        Vector3 rotation = new Vector3(0,0, angle);
        if (rotation.z > _maxRotation.z) rotation = _maxRotation;
        else if (rotation.z < _minRotation.z) rotation = _minRotation;
        transform.rotation = Quaternion.Euler(rotation);

        if (Input.GetMouseButton(0))
        {
            ProjectileSpeed += Time.deltaTime * 4;
            if (ProjectileSpeed > MaxSpeed) ProjectileSpeed = MaxSpeed;
        }
        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity);
            if (ProjectileSpeed < MinSpeed) ProjectileSpeed = MinSpeed;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * ProjectileSpeed; 
            ProjectileSpeed = 0f;
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}
