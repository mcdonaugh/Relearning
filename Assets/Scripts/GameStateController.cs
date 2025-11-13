using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = .05f;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _projectileSpeed = 1f;
    private Rigidbody _theGuy;
    private Rigidbody _theProjectile;
    private int _projectilesOut = 0;
    private Vector3 _playerCoordinates;
    private Vector3 _projectileCoordinates;
    
    private void GenerateCube()
    {
        Debug.Log("Player Is Generated");
        GameObject _newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _theGuy = _newCube.AddComponent<Rigidbody>();
        _playerCoordinates = new Vector3(0,0.5f,0);
        _theGuy.transform.position = _playerCoordinates;
        Debug.Log(_playerCoordinates);
    }

    private void GenerateProjectile()
    {
        if(_projectilesOut == 0)
        {
            _projectilesOut = _projectilesOut += 1;
            GameObject _newProjectile = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _theProjectile = _newProjectile.AddComponent<Rigidbody>();
            _theProjectile.transform.position = _playerCoordinates;
        }
    }

    private void ProjectileMove()
    {
        _projectileCoordinates = _theProjectile.position + transform.forward * _movementSpeed;
        _theProjectile.transform.position = _projectileCoordinates;
    }

    private void CubeShoot()
    {

        GenerateProjectile();
        ProjectileMove();
        Debug.Log("PewPew");
    }

    private void LogMovement()
    {
        Debug.Log($"Moved to {_playerCoordinates}");
    }

    private void CubeMoveForward()
    {
        _playerCoordinates = _theGuy.position + transform.forward * _movementSpeed;
        _theGuy.transform.position = _playerCoordinates;
        LogMovement();
    }

    private void CubeMoveBackward()
    {
        _playerCoordinates = _theGuy.position - transform.forward * _movementSpeed;
        _theGuy.transform.position = _playerCoordinates;
        LogMovement();
    }

    void Start()
    {
       GenerateCube(); 
    }
    
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CubeShoot();
        }

        if (Input.GetKey(KeyCode.W))
        {
            CubeMoveForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            CubeMoveBackward();
        }

    }

    
}
