using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tanks.Behaviours.Controlers
{
    public class EnemyController : MonoBehaviour
    {
        private Rigidbody EnemyTankRigidBody { get; set; }//RB od enemy tenka kako bi ga mogli kretati;
        
        public float Speed { get; set; }//Brzina kretanja potrebna za Move metodu dole;
        //Public jer zelimo da iz TankData scriptable objekta mozemo preuzeti brzinu tenka;

        //Posto nam se tenk krece po x i y koordinatama, zgodno je da koristimo Vector2 tip podatka;
        private List<Vector2> DirectionsSet { get; set; }//Lista mogucih direkcija za tenk;
        
        private Vector2 Direction { get; set; }//Trenutna direkcija  kretanja tenka;
        
        
        private void Start()
        {
            EnemyTankRigidBody = this.GetComponent<Rigidbody>();//Kako bi dobavili RB gameobject-a koji bude povezan sa ovom skriptom; Aka nas enemy tenk;
            
            DirectionsSet = new List<Vector2>()
            {
                //Dodamo hardkodirane moguce varijacije kretanja:
                new Vector2(0,0),
                new Vector2(-1,0),
                new Vector2(0,1),
                new Vector2(1,0),
                new Vector2(0,-1)
            };//Ne inicijaliziramo svakog frame vec jednom na pocetku, stoga je u Start a ne u Update;

            //Otpocnemo izvrsavanje nove coroutine, to ne znaci da je izvrsavanje u novom thread, vec je uporedo u starom  threadu pokrenuta i ova rutina;
            StartCoroutine(ChooseDirectionAfterTime(2));//Pozovemo nasu metodu sa time parametrom od 2 sekunde;
        }

        private void Update()
        {
            //Svakom iteracijom yield-a iz korutine, dobavlja se novi Direction iz liste mogucih i poziva se Move za njega;
            Move(Direction.x,Direction.y);//Posaljemo svaki frame nove dvije lokacije u metodu Move;
        }

        private IEnumerator ChooseDirectionAfterTime(float time)
        {
            while (true) //Beskonacna petlja jer zelimo da se konstantno kretanje vrsi;
            { 
               yield return new WaitForSeconds(time);//Svaki put kada okinemo yield (iterator), metoda se pauzira sa izvrsenjem za proslijedjeno vrijeme pa onda izvrsi kod ispod;
               Direction = DirectionsSet[Random.Range(0, 5)];//Idemo od 0 do 5 jer se 5 iskljucuje iz mogucih vrijednosti i onda imamo moguce vrijednosti od 0 do 4 i to je pet elemenata nase liste;
            }
        }
        private void Move(float horizontal, float vertical)
        {
            var vector = new Vector3(0,0,0); //Kreiramo defaultni trodimenzionalni vektor;
            if (horizontal != 0) //Moguce vrijednosti su od 1 do -1, ako nije nula korisnik je kliknuo A ili D ili desno ili lijevo strelice (po x osi);
            {
                vector.x = horizontal;
                vector = vector * (Speed * Time.deltaTime); //Delta time je prosjecno vrijeme izmedju dva frame-a;Uzima se kako bi imali normalan movement;
                //Pomnozimo sa Speed jer ono odredi kojom brzinom cemo se kretati;
                EnemyTankRigidBody.MovePosition(vector + this.transform.position); //position prop. klase Transform nam kaze gdje se objekat trenutno nalazi;

                //Rotation:
                EnemyTankRigidBody.MoveRotation(Quaternion.Euler(Vector3.up * (90 * horizontal))); //Rotiramo za 90 ili -90 jer sekrecemo lijevo ili desno;
                //Koristimo .up jer po toj osi rotiramo (po osi y se uvijek vrsi rotacija objekta);
                Debug.Log("<color=yellow>A or D is being pressed</color>");
            }
            else if (vertical != 0)
            {
                //y osa je za depth odnosno gore dole (gravitacija);
                //Ako se udje u ovaj else prica se o osi z koja kontrolise gore dole W S ili gornja donja strelica;
                vector.z = vertical;
                vector = vector * (Speed * Time.deltaTime); //Kada mnozimo vektor skalarom 3 operacije izvrsavamo tako da je more efficient kada speed i time prvo pomnozimo pa onda to sa vektorom;
                EnemyTankRigidBody.MovePosition(vector + this.transform.position);
                //Rotation:
                EnemyTankRigidBody.MoveRotation(vertical > 0 //Znaci da ostajemo u istom  pravcu ako je vertical pozitivan (>0)
                        ? Quaternion.Euler(Vector3.zero) //Stoga ne rotiramo tenk, jer se krecemo gore dole
                        : Quaternion.Euler(Vector3.up * (180 * vertical))); //Inace rotiramo ga za 180 (negativno), promjenili smo pravac;

                Debug.Log("<color=red>W or S is being pressed</color>");
            }
            else
            {
                Debug.Log("User didn't press any key!");
            }
        }
    }
}
