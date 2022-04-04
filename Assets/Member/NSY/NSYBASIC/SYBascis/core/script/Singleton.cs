using System;  

public class Singleton<T> where T : class, new()  
{  
	public static T Instance  
	{  
		get;  
		private set;  
	}  
	
	static Singleton()  
	{  
		if (Instance == null)  
		{
            Instance = new T();  
		}  
	}  
	
	public virtual void Clear()  
	{
        Instance = null;
        Instance = new T();  
	}    
}  
