/*
 * Hugo
 * 
 * Object pooling class
 * 
 * It creates various pools with different tags for efficency
 * 
 * Dominique (Changes) 04/04/2020
 * Made object activate after having values changed (or wrong sound plays)
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pooler_HR : MonoBehaviour
{
    private FadeToBlack fade;
    #region PoolClass
   
    [System.Serializable]
    public class Pool
    {

        public Tags tag;
        public GameObject prefab;
        public int size;

    }
    #endregion

    #region Singleton
    public static Pooler_HR instance;

    private void Awake()
    {
        fade = GetComponent<FadeToBlack>();
        DontDestroyOnLoad(gameObject);
        instance = this;
        //WHEN TESTING THIS IS COMMENTED OUT TO AVOID THE ERROR - IT IS NECESSARY FOR THE START TO GO INTO THE MAIN MENU IN THE FINAL BUILD
        StartCoroutine(ToNextScene());
    }

    #endregion 
    public enum Tags
    {
        SFX
    }
    public List<Pool> pools;
    public Dictionary<Tags, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        //Populates the pool dictionary
        poolDictionary = new Dictionary<Tags, Queue<GameObject>>();

        //Looks for the amount of pools introduced in the inspector and queues the amount of instanciated objects set in the inspector
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab,transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    //SFX manager will call this function.
    //It will look at the next available object from the pool and give it to the manager to play
    public GameObject SpawnFromPool(Tags tag, Vector3 position, AudioClip clip)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.GetComponent<AudioSource>().clip = clip;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    public IEnumerator ReturnToPool(Tags tag, GameObject spawnedObject, float afterSeconds)
    {
        yield return new WaitForSeconds(afterSeconds);
        poolDictionary[tag].Enqueue(spawnedObject);
        spawnedObject.SetActive(false);
    }

    private IEnumerator ToNextScene()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(fade.Fade());
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene((int)UIManager_AR.Scenes.MENU);
    }
}
