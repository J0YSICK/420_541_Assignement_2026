using UnityEngine;

public class GameManager : MonoBehaviour
{
    int Score = 0;
    
    public static GameManager Instance { get; private set;}

    private void Awake(){
        Instance = this;
    }


}
