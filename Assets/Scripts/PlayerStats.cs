using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
	// TODO add setter and getter for Money
	// TODO add some visual or audial effects 
    public static int Money;
    public int startMoney = 400;

	// TODO add setter and getter for Lives
	// TODO add some visual or audial effects
    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }

}
