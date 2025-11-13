using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private int _age = 0;
    [SerializeField] private float _playerCurrentHealth = 30;
    [SerializeField] private float _playerMaxHealth = 50;
    [SerializeField] private float _theDamage = 5;
    [SerializeField] private float _theHeal = 5;
    [SerializeField] private int _diceSides = 6;
    private string _name = "Brian";
    private bool _isAdult = false;


    private void CheckAgeValidity()
    {
        if (_age <= 0)
        {
            _age = 1;
        }

        if (_age >= 130)
        {
            _age = 129;
        }
    }

    private void CheckAdulthood()
    {
        if (_age >= 18)
        {
            _isAdult = true;
        }
        else
        {
            _isAdult = false;
        }
    }

    private void PrintPlayerData()
    {
        Debug.Log("My name is " + _name + ", and I am " + _age + " years old and I'm an adult: " + _isAdult + ". " + _name + "'s current health is: " + _playerCurrentHealth);
        Debug.Log($"My name is {_name}, and I am {_age} years old and it is {_isAdult} that I am an adult! My current health is {_playerCurrentHealth}!");
    }

    private void DamagePlayer()
    {
        _playerCurrentHealth = _playerCurrentHealth - _theDamage;
        Debug.Log($"The player has recieved {_theDamage} damage!");
    }

    private void HealPlayer()
    {
        if (_playerCurrentHealth > 0 && _playerCurrentHealth < _playerMaxHealth)
        {
            _playerCurrentHealth = _playerCurrentHealth + _theHeal;
            Debug.Log($"The player has bean healed by {_theHeal}!");
        }
        else if (_playerCurrentHealth >= _playerMaxHealth)
        {
            _playerCurrentHealth = _playerMaxHealth;
            Debug.Log("Your health is full!");
        }
        else
        {
            Debug.Log("You can't heal - you're ded, silly!");
        }
    }

    private int Roll(int sides)
    {
        int _theRoll = Random.Range(1, sides + 1);
        Debug.Log($"You rolled a {_theRoll}!");
        return _theRoll;
    }

    private void HurtOrHeal()
    {

        int currentRoll = Roll(_diceSides);
        if (currentRoll == 1)
        {
            DamagePlayer();
        }
        else if (currentRoll == 6)
        {
            HealPlayer();
        }
        
    }

    void Start()
    {
        _playerCurrentHealth = _playerMaxHealth;
        CheckAgeValidity();
        CheckAdulthood();
        PrintPlayerData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_playerCurrentHealth > 0)
            {
                Debug.Log("I pressed space!");
                DamagePlayer();
                PrintPlayerData();
            }
            else
            {
                Debug.Log("I am ded.");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("I pressed R to Heal!");
            HealPlayer();
            PrintPlayerData();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Roll(_diceSides);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HurtOrHeal();
        }
    }

}
