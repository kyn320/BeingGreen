using UnityEngine;

public class ViewModel<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new object();

	public static T Model
	{
		get
		{
			if (applicationIsQuitting)
			{
				return null;
			}

			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = (T)FindObjectOfType(typeof(T));
                    
					if (FindObjectsOfType(typeof(T)).Length > 1)
					{
						return _instance;
					}

					if (_instance == null)
					{
						Context context = Context.Instance;
                        
						_instance = context.gameObject.AddComponent<T>();

					} 

				}
                
				return _instance;
			}
		}
	}

	private static bool applicationIsQuitting = false;

	public void OnDestroy()
	{
		applicationIsQuitting = true;
	}
}