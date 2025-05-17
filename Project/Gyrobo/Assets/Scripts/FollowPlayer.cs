using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gyrobo
{
    public class FollowPlayer : MonoBehaviour
    {
        public GameObject Player;

        public GameObject GameManager;
        private GameManager _gameManagerScript;

        // Start is called before the first frame update
        void Start()
        {
            _gameManagerScript = GameManager.GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_gameManagerScript.IsGameOver)
            {
                return;
            }
            
            transform.position = Player.transform.position + new Vector3(0, 0, 10);
        }
    }
}
