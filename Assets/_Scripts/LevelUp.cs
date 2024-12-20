using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public int fertilizer;
    [SerializeField] private int fertilizerForLevelUp;
    private int level;

    public void AddFertilizer()
    {
        fertilizer++;
        
        
    }
    public void Level()
    {
        if(fertilizer >= fertilizerForLevelUp)
        {
            level++;
        }
        Debug.Log(level);
    }
    
}
