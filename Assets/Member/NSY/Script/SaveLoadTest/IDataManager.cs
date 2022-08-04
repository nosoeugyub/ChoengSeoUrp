using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataManager
{
    void LoadData(SaveData data);

    void SaveData(ref SaveData data);//참조 전달 = 값 수정가능하게 함 
    
  
}
