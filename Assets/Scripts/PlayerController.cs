using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _aim;
    [SerializeField] Camera _mainCamera;
    [SerializeField] float _cameraFollowSpeed;
    Vector2 _direction = Vector2.zero;
    Vector3 _cameraOffset;
    Vector2 _mousePosition;
    float _speed = 20f;
    [SerializeField] float _rotationSpeed = 2f;
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        _direction = new Vector2(x,y).normalized;
       
        Move();
        transform.position += new Vector3(_direction.x, 0 ,_direction.y) * _speed * Time.deltaTime;        
        transform.LookAt(new Vector3(transform.position.x + _direction.x,0,transform.position.z + _direction.y));
    }

    private void Move()
    {
        if(Input.GetAxis("Horizontal") >= .001 || Input.GetAxis("Vertical") >= .001)
        {
            
        }
    }

    private void LateUpdate()
    {
        _mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z - 12);
    }
}