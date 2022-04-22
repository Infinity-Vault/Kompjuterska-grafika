using System.Collections.Generic;
using Tanks.Behaviours.Controlers;
using Tanks.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tanks.Generators
{
    public class EnemyGenerator : MonoBehaviour
    {
        [field: SerializeField] private List<TankData> Tanks { get; set; } = new List<TankData>();//Lista TankData scriptable objekata odakle
        //mozemo uzimati razlicite vrste tenkova i generisati ih randomly;
        [field: SerializeField]
        private int NumberOfEnemies { get; set; }//Broj neprijatelja za generisati;
        private void Start()
        {
            for (int i = 0; i < NumberOfEnemies; i++)//Generisemo broj enemies koji je unijet 
            {
               
                if (Random.Range(0, 2) < 0.5f)//Random broj od 0 do 1 ispitamo
                    Instantiate(Tanks[0].TankModel, new Vector3(i+i*2, 0, i*3),
                        Quaternion.identity).GetComponent<EnemyController>().Speed = Tanks[0].Speed;//ako je manji od 0.3 generisemo prvi tenk nase liste i pristupimo njegovoj komponenti EnemyController koja ima property Speed i postavimo ga na Speed od prvog tank model;
                else 
                    //U suprotnom uradimo isto samo generisemo drugacijeg neprijatelja, drugi tip;
                    Instantiate(Tanks[1].TankModel, new Vector3(i+i*2, 0, i*3),
                        Quaternion.identity).GetComponent<EnemyController>().Speed = Tanks[1].Speed;
            }
        }
    }
}
