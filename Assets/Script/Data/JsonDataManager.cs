using System;
using System.IO;
using UnityEngine;

namespace Data {
    public static class JsonDataManager
    {
        private static string getFilePath() { return Application.dataPath + "/UserData" + ".json"; }

        /// <summary>
        /// シリアライズするデータ
        /// </summary>
        /// <param name="playerDataWrapper"></param>
        public static void Save(PlayerDataWrapper playerDataWrapper) 
        {
            //シリアライズ
            string jsonSerializedData = JsonUtility.ToJson(playerDataWrapper);
            Debug.Log(jsonSerializedData);

           /* //ファイルの書き出し
            StreamWriter writer = new StreamWriter(Application.dataPath + "/savedata.json",false);
            writer.Write(jsonSerializedData);
            writer.Flush();
            writer.Close();*/

            //ファイルの読み込み
            using (var sw = new StreamWriter(getFilePath(), false)) 
            {
                try
                {
                    sw.Write(jsonSerializedData);
                }
                catch (Exception e) 
                {
                    Debug.Log(e);
                }
            }
        
        }

        /// <summary>
        /// 読み込み機能
        /// </summary>
        /// <returns></returns>
        public static PlayerDataWrapper Load() 
        {
            PlayerDataWrapper jsonDeserializedData = new PlayerDataWrapper();

            try
            {
                //ファイルの読みこみ
                using (FileStream fs = new FileStream(getFilePath(), FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string result = sr.ReadToEnd();
                    Debug.Log(result);

                    jsonDeserializedData = JsonUtility.FromJson<PlayerDataWrapper>(result);
                }
            }
            catch (Exception e) 
            {
                Debug.Log(e);
            }

            return jsonDeserializedData;
        
        }

    }
}