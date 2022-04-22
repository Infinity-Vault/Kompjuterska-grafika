using Unity.Mathematics;
using UnityEngine;

namespace Tanks.Generators
{
    public class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField] private GameObject ObsticleOne { get; set; } //Prepreka koju uzmemo iz Unity;
        [field: SerializeField] private GameObject ObsticleTwo { get; set; } //Prepreka koju uzmemo iz Unity;

        [field: SerializeField]
        private Vector2Int Dimensions { get; set; } //Kako bi mogli iz Unity generisati dimenzije

        private void Start()
        {
            //Staro generisanje:


            //var rand = new System.Random();//Random obj;
            //for (int i = 0; i < 10; i++)
            //{
            //    //Randomly generisemo x i z pozicije dok je y visina fiksna;
            //    var positionx = rand.Next(1, 5);
            //    var positionz = rand.Next(1, 5);
            //    Instantiate(ObsticleOne, new Vector3(positionx, 0.50f, positionz), 
            //        quaternion.identity);
            //    //Instantiate extension metoda nam omoguci da instanciramo objekat,
            //    //na nekoj poziciji, i rotiran u nekom polozaju;
            //    //quaternion.identity predstavlja 0,0,0 rotaciju;
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    //Isto kao i gore samo sa negativnim vrijednostima;
            //    var positionx = rand.Next(-5, -1);
            //    var positionz = rand.Next(-5, -1);
            //    Instantiate(ObsticleOne, new Vector3(positionx, 0.50f, positionz), 
            //        quaternion.identity);
            //}            


            //Novo generisanje:

           //for (int i = 0; i < Dimensions.x; i++)
           //{
           //    for (int j = 0; j < Dimensions.y; j++)
           //    {
           //        if ((i & j) > 3) // razliciti patterni promjenom |,^,& itd, takodjer broj 3 moze biti bilo koji br (razliciti patterni sa raz. brojevima)
           //            Instantiate(ObsticleOne, new Vector3(i, 0, j), Quaternion.identity);
           //    }
           //}

           //Random number generation:
           // for (int i = 0; i < Dimensions.x; i++)
           // {
           // 
           //    for (int j = 0; j < Dimensions.y; j++)
           //    {
           //        if (Random.Range(0f,1)>0.8f)//Bitno je staviti f kod nule kako bi se generisala random float vrijednost;
           //            Instantiate(ObsticleOne, new Vector3(i,0,j), Quaternion.identity);
           //        //Else if stavimo jer zelimo da dva puta random broj generisemo (time dobivamo prazne prostore)
           //        else if(Random.Range(0f,1)>0.8f)
           //            Instantiate(ObsticleTwo, new Vector3(i,0,j), Quaternion.identity);//Quaternion.identity jer ne zelimo nikakve rotacije da imamo na objektu;
           //    }
           // }

            //Perlin noise usage (Similar logic used in Mincraft):
            for (int i = 0; i < Dimensions.x; i++)
            {
                for (int j = 0; j < Dimensions.y; j++)
                {
                    //var xCoordinate= (i*0.5f) / Dimensions.x*20;
                    //var yCoordinate = (j*0.5f) / Dimensions.y*20;
                    //
                    ////Slicna logika random generisanja kao i gore:
                    //if(Mathf.PerlinNoise(xCoordinate, yCoordinate)>0.7f)
                    //    Instantiate(ObsticleOne, new Vector3(i,0,j), Quaternion.identity);
                    //else if(Mathf.PerlinNoise(xCoordinate, yCoordinate)<0.4f)
                    //    Instantiate(ObsticleTwo, new Vector3(i,0,j), Quaternion.identity);
                    
                    //Muhamedov nacin generisanja levela:
                    var xCoordinate = 0.6f * i / Dimensions.x * 10;//Kreiramo float brojeve za PerlinNoise
                    var yCoordinate = 0.3f * j / Dimensions.y * 10;
                    var positionOfBlock = Mathf.PerlinNoise(xCoordinate, yCoordinate);//Perlin nam vrati broj izmedju 0/1;
            
                    if (positionOfBlock > 0.55f)
                        Instantiate(ObsticleOne, new Vector3(i-(Dimensions.x/2), 0, j-(Dimensions.y/2)), 
                            quaternion.identity);//Dijelimo dimenzijom samo radi ljepse raspodjele kocki;
                    else if(positionOfBlock<=0.3f)
                        Instantiate(ObsticleTwo, new Vector3(i-(Dimensions.x/2), 0, j-(Dimensions.y/2)), 
                            quaternion.identity);
                }
            }
        }
    }
}

