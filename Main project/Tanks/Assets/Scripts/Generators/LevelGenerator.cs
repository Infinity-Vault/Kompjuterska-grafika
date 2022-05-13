using Tanks.Core;
using Tanks.Core.Algorithms;
using Unity.Mathematics;
using UnityEngine;

namespace Tanks.Generators
{
    public class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField] private GameObject Obsticle { get; set; } //Prepreka koju uzmemo iz Unity;
        [field: SerializeField] private GameObject WalkableBlock { get; set; } //Prepreka koju uzmemo iz Unity;

        [field: SerializeField]
        private Vector2Int Dimensions { get; set; } //Kako bi mogli iz Unity generisati dimenzije
        private Node [,] MapGraph { get; set; }//Graf koji ce se sastojati od cvorova koji su walkable;
        //[,] je skraceni oblik deklarisanja i indeksiranja 2D niza;
        
        
        private void Start()
        {
            MapGraph = new Node[Dimensions.x, Dimensions.y];//Instanciramo 2D niz x i y dimenzija naseg levela u Unity;
            GenerateLevel();//Generisemo level;
            var BFS = new BFS(MapGraph);//Napravimo objekt klase BFS sa nasim grafom;

           var Path = BFS.GetPathToTarget(new Vector2Int(49,38),new Vector2Int(32,9));
           //Odaberemo putanju koju zelimo (od koje do koje kocke);

           foreach (var node in Path)
               node.Block.GetComponent<Renderer>().material.color = Color.blue;//Svaku kocku u putanji obojimo u plavo;
        }

        private void GenerateLevel()
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
                    
                    var xCoordinate = 0.6f * i / Dimensions.x * 10; //Kreiramo float brojeve za PerlinNoise
                    var yCoordinate = 0.3f * j / Dimensions.y * 10;
                    var BlockPosition =
                        new Vector3(i - Dimensions.x / 2, 0,
                            j - Dimensions.y / 2); //Kreiramo poziciju na kojoj cemo instancirat kocke/prepreke;
                    //Dijelimo dimenzijom samo radi ljepse raspodjele kocki;

                    if (Mathf.PerlinNoise(xCoordinate, yCoordinate) > 0.5f) //Perlin nam vrati broj izmedju 0/1;
                    {
                        MapGraph[i, j] =
                            new Node(BlockPosition,
                                false); //Kreiramo cvor na poziciji trenutne kocke i sa vrijednoscu false jer nije walkable kocka;
                        var kocka = Instantiate(Obsticle, BlockPosition,
                            quaternion.identity); //Pokupimo GameObject kocku;
                        MapGraph[i, j].Block = kocka; //Trenutnom cvoru dodijelimo trenutnu kocku u prop Block;

                    }
                    else
                    {
                        MapGraph[i, j] =
                            new Node(BlockPosition,
                                true); //Kreiramo cvor na poziciji trenutne kocke i oznacimo da je walkable;
                        var kocka = Instantiate(WalkableBlock, BlockPosition,
                            Quaternion.identity); //Instanciramo walkable kocku;
                        kocka.name = $"Walkable [{i},{j}]";//Kako bi u debagiranju imali vidiljivo o kojoj kocki je rijec;
                        MapGraph[i, j].Block = kocka;
                    }
                }
            }

            GenerateGraph();//Generisemo graf tj. povezemo susjedne cvorove;
        }

        private void GenerateGraph()
        {
            for (int i = 0; i < Dimensions.x; i++)
            {
                for (int j = 0; j < Dimensions.y; j++)
                {
                    //Na sljedeci nacin provjeravamo lijevo,desno,gore i dole za sve moguce walkable kocke;
                    //Svrha jeste da imamo matricu  svih walkable kocki;
                    if (j - 1 >= 0)//Ako nismo otisli izvan matrice lijevo;
                    {
                        if (MapGraph[i, j-1].IsWalkable)//Ako je prethodni cvor walkable kocka
                            MapGraph[i,j].Adjacency.Add(MapGraph[i,j-1]);//Dodaj prethodni node kao susjed trenutnom
                    }

                    if (j + 1 < Dimensions.y)//Ako nismo otisli izvan matrice desno 
                    {
                        if(MapGraph[i,j+1].IsWalkable)//Ako je naredni cvor walkable kocka
                            MapGraph[i,j].Adjacency.Add(MapGraph[i,j+1]);//Dodaj naredni node kao susjed trenutnom;
                    }

                    //Ista logika za kretanje gore dole:
                    if (i - 1 >= 0)//Ako smo otisli gore izvan matrice
                    {
                        if(MapGraph[i-1,j].IsWalkable)
                            MapGraph[i,j].Adjacency.Add(MapGraph[i-1,j]);
                    }

                    if (i + 1 < Dimensions.x)//Ako smo otisli dole izvan matrice
                    {
                        if(MapGraph[i+1,j].IsWalkable)
                            MapGraph[i,j].Adjacency.Add(MapGraph[i+1,j]);
                    }
                }
            }
        }
    }
}


