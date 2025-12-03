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
    private GameObject _playerCharacter;
    private GameObject _theProjectile;
    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private GameObject _playerCharacterModel;
    private Animator _playerCharacterAnimator;
    
    private void Awake()
    {
        _ammoPool = new GameObject[_maxAmmo];
        Debug.Log(_ammoPool.Length);
    }
    private void Start()
    {
        // _playerControls = new PlayerControls();  
        // _playerControls.Enable();
        // _playerControls.Player.Fire.performed += Shoot;
    }

    private void Update()
    {

        if(_playerCharacter == null)
        {
            return;
        }
        
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");
        // Debug.Log($"{_horizontalInput},{_verticalInput}");

        // Calculate the input direction vector
        Vector3 _lookDirection = new Vector3(_horizontalInput, 0f, _verticalInput);

        // Determine the intensity of the joystick push (magnitude between 0 and 1)
        float _inputMagnitude = _lookDirection.magnitude;

        // Normalize the look direction for consistent direction calculation
        _lookDirection = _lookDirection.normalized;

        // Use the input magnitude to scale the actual speed
        float _speedToApply = _movementSpeed * _inputMagnitude;

        Debug.Log(_inputMagnitude);

        if(_inputMagnitude > 0.01f) // Use a small threshold to prevent jitter when joystick is centered
        {
            float angle = Mathf.Atan2(_lookDirection.x, _lookDirection.z) * Mathf.Rad2Deg;
            _playerCharacter.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            _playerCharacterAnimator.Play("MoveForward"); // You might need a blend tree for variable speed animation

            // Apply the movement using the scaled speed
            _playerCharacter.transform.position += _lookDirection * _speedToApply * Time.deltaTime;
        }



        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     Reload();
        // }

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     Shoot();
        // }

    }

    public void GenerateCharacter()
    {
        _playerCharacter = new GameObject("PlayerCharacter");
        _playerCharacterModel = Instantiate(_playerCharacterModel);
        _playerCharacterAnimator = _playerCharacterModel.GetComponent<Animator>();
        _playerCharacterModel.transform.SetParent(_playerCharacter.transform);
        _playerCharacter.transform.position = new Vector3(0,0,0);
        _theCamera.transform.SetParent(_playerCharacter.transform);
        _createCharacterButton.gameObject.SetActive(false);
        // GenerateAmmo();
    }
 
    private void GenerateAmmo()
    {
        for(int i = 0; i < _ammoPool.Length; i++)
        {
            _theProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _ammoPool[_currentAmmoCount] = _theProjectile;
            _ammoPool[_currentAmmoCount].transform.position = _playerCharacter.transform.position;
            _ammoPool[_currentAmmoCount].transform.rotation = _playerCharacter.transform.rotation;
            _ammoPool[_currentAmmoCount].AddComponent<ProjectileController>().projectileSpeed = _projectileSpeed;
            _ammoPool[i].SetActive(false);
            _currentAmmoCount++;
            
        }
        Debug.Log($"{_currentAmmoCount}");
    }

    private void Shoot()
    {
        Debug.Log("Shoot Active");
       for(int i = 0; i < _ammoPool.Length; i++)
            {
                if(!_ammoPool[i].activeInHierarchy && _currentAmmoCount > 0)
                {
                    _ammoPool[i].transform.position = _playerCharacter.transform.position;
                    _ammoPool[i].transform.rotation = _playerCharacter.transform.rotation;
                    _ammoPool[i].SetActive(true);
                    _currentAmmoCount--;
                    Debug.Log($"{_currentAmmoCount}");
                    break;  
                }
                if(_currentAmmoCount <= 1)
                {
                    Reload();
                    Debug.Log($"Reloading to {_currentAmmoCount}!"); 
                }    
        }
    }
    private void Reload()
    {
        _currentAmmoCount = _ammoPool.Length + 1;
        Debug.Log($"Reloading to {_currentAmmoCount}!");
    }    
}
