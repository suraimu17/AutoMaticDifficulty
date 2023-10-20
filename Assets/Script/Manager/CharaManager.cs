using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class CharaManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] CharaArray;
        //ƒLƒƒƒ‰‚Ì‘½‚³‚É‚æ‚Á‚Ä•Ï‚¦‚é
        public bool[] CharaChoise { get; private set; } = new bool[2];

        public static CharaManager Instance = null;

        public int setCharaNum = 0;
        public int upgradeNum = 0;
        public int strongTileChara = 0;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }



        public GameObject GetChoiceChara() 
        {
            int index = -1;
            for (int i = 0; i < CharaChoise.Length; i++) 
            {
                if (CharaChoise[i] == true) index = i;
            }
            if (index < 0) return null;

            return CharaArray[index];

        }
        public void ResetCharaChoice() 
        {
            for (int i = 0; i < CharaChoise.Length; i++) 
            {
                CharaChoise[i]=false;
            }
        }
    }
}