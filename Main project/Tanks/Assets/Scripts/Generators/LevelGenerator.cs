using Unity.Mathematics;
using UnityEngine;

namespace Tanks.Generators
{
    public class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField]
        private GameObject Obsticle { get; set; } //Prepreka koju uzmemo iz Unity;
     
        private void Start()
        {
            var rand = new System.Random();//Random obj;
            for (int i = 0; i < 10; i++)
            {
                //Randomly generisemo x i z pozicije dok je y visina fiksna;
                var positionx = rand.Next(1, 5);
                var positionz = rand.Next(1, 5);
                Instantiate(Obsticle, new Vector3(positionx, 0.50f, positionz), 
                    quaternion.identity);
                //Instantiate extension metoda nam omoguci da instanciramo objekat,
                //na nekoj poziciji, i rotiran u nekom polozaju;
                //quaternion.identity predstavlja 0,0,0 rotaciju;
            }
            for (int i = 0; i < 10; i++)
            {
                //Isto kao i gore samo sa negativnim vrijednostima;
                var positionx = rand.Next(-5, -1);
                var positionz = rand.Next(-5, -1);
                Instantiate(Obsticle, new Vector3(positionx, 0.50f, positionz), 
                    quaternion.identity);
            }            
        }
    }
}

