using UnityEngine;
using Manager;

namespace Data {
    public class UserData : MonoBehaviour
    {
        private DifficultyManager difficultManager;

        private void Start()
        {
            difficultManager = FindObjectOfType<DifficultyManager>();
            var player = load();
            //Debug.Log(player.DataList[0].amountStylePer);
        }
        public void Save(float amountPer,float strongPer,float savePer,string style,bool IsStyle) 
        {
            PlayerDataWrapper playerDataWrapper = JsonDataManager.Load();
            SaveData saveData = new SaveData();

            //  ÉfÅ[É^ÇÃí«â¡
            if (playerDataWrapper.DataList.Count==0) saveData.playerNum = 1;
            else saveData.playerNum = playerDataWrapper.DataList[playerDataWrapper.DataList.Count - 1].playerNum + 1;

            saveData.amountStylePer = amountPer;
            saveData.strongStylePer = strongPer;
            saveData.saveStylePer = savePer;
            saveData.playerStyle = style;
            saveData.IsStyle=IsStyle;
            saveData.difficluty = difficultManager.difficulty;

            playerDataWrapper.DataList.Add(saveData);


            JsonDataManager.Save(playerDataWrapper);
        }

        public PlayerDataWrapper load()
        {
            return JsonDataManager.Load();
        }

    }
}