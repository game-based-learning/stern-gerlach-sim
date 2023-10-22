using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SternGerlach
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] public Material correct, wrongAngle, wrongNode;
        // add more states
        public enum GameState { DEFAULT, FROZEN }
        private GameState state = GameState.DEFAULT;
        // singleton
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        public void ToggleFreeze() {
            if (state != GameState.FROZEN) { 
                state = GameState.FROZEN;
                return;
            }
            state = GameState.DEFAULT;
        }
        public GameState GetGameState() { return state; }
    }
}
