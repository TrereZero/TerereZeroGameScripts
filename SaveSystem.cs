using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveLevel(LevelData data){

            if(!Directory.Exists(Application.persistentDataPath + "/saves/")){
                createSaveDataDirectory();
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saves/levelData.txt";
            
            FileStream stream = new FileStream(path, FileMode.Create);

            binaryFormatter.Serialize(stream, data);
            stream.Close();
    }

    public static LevelData LoadLevel(){
        string path = Application.persistentDataPath + "/saves/levelData.txt";
        if(File.Exists(path)){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            LevelData data = (LevelData) binaryFormatter.Deserialize(stream);
            stream.Close();

            return data;
        }else{
            LevelData newData = new LevelData();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            
            FileStream stream = new FileStream(path, FileMode.Create);

            binaryFormatter.Serialize(stream, newData);
            stream.Close();
            return newData;
        }
    }


    public static void SavePlayerStatus(PlayerScript player){

        if(!Directory.Exists(Application.persistentDataPath + "/saves/")){
                createSaveDataDirectory();
            }
            
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saves/playerData.txt";
            
            FileStream stream = new FileStream(path, FileMode.Create);
            PlayerData data = new PlayerData(player);

            binaryFormatter.Serialize(stream, data);
            stream.Close();


    }

    public static PlayerData LoadPlayerStatus(){

        string path = Application.persistentDataPath + "/saves/playerData.txt";
        if(File.Exists(path)){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            PlayerData data = (PlayerData)binaryFormatter.Deserialize(stream);
            stream.Close();

            return data;
        }else{
            Debug.Log("Arquivo de PlayerData não existe");
            return null;
        }


        
    }

    public static void SaveEndGame(){

            LevelData data = LoadLevel();
            data.gameComplete = true;
            data.playerPosition[0] = 1.8267f;
            data.playerPosition[1] = 0.436f;
            SaveLevel(data);
    }


    public static void deleteAllSaveData(){
        string path = Application.persistentDataPath + "/saves/";
        DirectoryInfo saveDirectory = new DirectoryInfo(path);
        Directory.Delete(path,true);
        Directory.CreateDirectory(path);
    }

    public static void createSaveDataDirectory(){
        string path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
    }
}
