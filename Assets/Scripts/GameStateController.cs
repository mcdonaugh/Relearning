using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = .05f;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private Transform _theCamera;
    [SerializeField] private Button _createCharacterButton;
    [SerializeField] private float _projectileSpeed = 100f;
    [SerializeField] private int _maxAmmo = 5;
    private int _currentAmmoCount = 0;
    private GameObject[] _ammoPool;
    private GameObject _theGuy;
    private GameObject _theProjectile;
    
    private void Awake()
    {
        _ammoPool = new GameObject[_maxAmmo];
        Debug.Log(_ammoPool.Length);

    }

    public void GenerateCube()
    {
        Debug.Log("Player Is Generated");
        GameObject _newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _theGuy = _newCube;
        _theGuy.transform.position = new Vector3(0,0.5f,0);
        _theCamera.transform.SetParent(_theGuy.transform);
        _createCharacterButton.gameObject.SetActive(false);
        GenerateAmmo();
    }

    //generate full pool
    //reduce ammo on each space
    //when projectile count hits < 1, fill

    private void Shoot()
    {
       for(int i = 0; i < _ammoPool.Length; i++)
            {
                if(!_ammoPool[i].activeInHierarchy && _currentAmmoCount > 0)
                {
                    _ammoPool[i].transform.position = _theGuy.transform.position;
                    _ammoPool[i].transform.rotation = _theGuy.transform.rotation;
                    _ammoPool[i].SetActive(true);
                    _currentAmmoCount--;
                    Debug.Log($"{_currentAmmoCount}");
                    break;  
                }

                Debug.Log("You are out of ammo!");

        }
    }

    private void GenerateAmmo()
    {
        Debug.Log("This Method Was Called");
        Debug.Log($"{_ammoPool.Length}");
        Debug.Log($"{_currentAmmoCount}");

        for(int i = 0; i < _ammoPool.Length; i++)
        {
            _theProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _ammoPool[_currentAmmoCount] = _theProjectile;
            _ammoPool[_currentAmmoCount].transform.position = _theGuy.transform.position;
            _ammoPool[_currentAmmoCount].transform.rotation = _theGuy.transform.rotation;
            _ammoPool[_currentAmmoCount].AddComponent<ProjectileController>().projectileSpeed = _projectileSpeed;
            _ammoPool[i].SetActive(false);
            _currentAmmoCount++;
            
        }
        Debug.Log($"{_currentAmmoCount}");
    }

    private void Reload()
    {
        _currentAmmoCount = _ammoPool.Length;
        Debug.Log($"Reloading to {_currentAmmoCount}!");
    }

    private void CubeMoveForward()
    {
        _theGuy.transform.position = _theGuy.transform.position + _theGuy.transform.forward * _movementSpeed;
    }

    private void CubeMoveBackward()
    {
        _theGuy.transform.position = _theGuy.transform.position - _theGuy.transform.forward * _movementSpeed;
    }

    private void CubeRotateRight()
    {
        Vector3 rotation = new Vector3(0,_rotationSpeed,0);
        _theGuy.transform.rotation = _theGuy.transform.rotation * Quaternion.Euler(rotation * Time.deltaTime);
    }

    private void CubeRotateLeft()
    {
        Vector3 rotation = new Vector3(0,_rotationSpeed,0);
        _theGuy.transform.rotation = _theGuy.transform.rotation * Quaternion.Euler(-rotation * Time.deltaTime);
    }
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetKey(KeyCode.W))
        {
            CubeMoveForward();
        }

        if (Input.GetKey(KeyCode.S))
        {
            CubeMoveBackward();
        }

        if (Input.GetKey(KeyCode.D))
        {
            CubeRotateRight();
        }

        if (Input.GetKey(KeyCode.A))
        {
            CubeRotateLeft();
        }



    }

    
}
