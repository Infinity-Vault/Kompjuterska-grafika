using System.Collections.Generic;
using UnityEngine;

namespace Tanks.Behaviours.Controlers
{
    public class AIEnemyControler : MonoBehaviour
    {
        [field: SerializeField] 
        private float Speed { get; set; } //Brzina za kretanje neprijatelja;

        private Vector3 TargetPosition { get; set; } //Kako bi znali narednu zeljenu  poziciju;

        [field: SerializeField]
        private List<Vector3> WayPoints { get; set; } //Lista narednih pozicija koje neprijatelj treba pratiti;

        private int WayPointIndex { get; set; } = 0; //Index sa kojim se krecemo po listi narednih pozicija, postavljen na 0;

        private Rigidbody TankRigidBody { get; set; } //Radi rada sa fizikom tenka (neprijatelja);
        
        //Metoda koju pozovemo iz level generatora da bi preuzeli pozicije po kojima se neprijatelj treba kretati;
        public  void  SetWayPoints(List<Vector3> wayPoints)
        {
            this.WayPoints = wayPoints; //Inicijaliziramo listu pozicija;
            TargetPosition = WayPoints[WayPointIndex]; //Postavimo narednu zeljenu poziciju na prvu poziciju liste;
        }

        private void Start()
        {
            this.TankRigidBody =
                this.GetComponent<Rigidbody>(); //Preuzmemo RB od objekta na koju je zakacena ova skripta a to je neprijatelj;
        }

        //Koristimo FixedUpdate jer je neovisna metoda od framerate-a,
        //odnosno ne zavisi joj brzina poziva od brzine frame-a
        private void FixedUpdate()
        {
            MoveThroughWayPoints();
        }

        private void MoveThroughWayPoints()
        {
            while (Vector3.Distance(this.transform.position, TargetPosition) >= 0.02f)
                //Sve dok je udaljenost trenutne pozicije i naredne zeljene >= od 0.02f
                //Nastavi kretanje :
            {
                this.TankRigidBody.MovePosition(Vector3.MoveTowards(this.transform.position, TargetPosition
                    , Speed * Time.deltaTime)); //Pomjerimo neprijatelja na narednu zeljenu poziciju;

                var LookAtNextPosition = Vector3.RotateTowards(
                    this.transform.position, TargetPosition,
                    Speed * Time.deltaTime ,0.0f);//Spremimo poziciju prema gdje neprijatelj treba da gleda dok se krece;
                //MaxRadians nam kaze za koliko smijemo max rotirati vektor (mi rotiramo za Speed*Time.DeltaTime),
                //Dok MaxMagnitude nam kaze koliko maksimalno se moze vektor povecati pri ovoj rotaciji;
                
                this.TankRigidBody.MoveRotation(Quaternion
                    .LookRotation(LookAtNextPosition));//Zarotiramo trenutnog neprijatelja prema gore izracunatoj rotaciji;

                return;//Kada pomjerimo neprijatelja i zarotiramo ga, izadjemo iz metode;
            }

            WayPointIndex++;//Uvecamo index jer je za jednu poziciju iz liste pozicija pomjeren neprijatelj;

            if (WayPointIndex == WayPoints.Count - 1)//Ukoliko smo dosli do zadnje pozicije u listi
            //-1 jer index pocinje sa brojanjem od 0, dakle
            //ako imamo WayPointIndex 4 a max velicinu liste 5,
            //5-1=4,4==4 dosli smo do kraja jer brojimo 0,1,2,3,4 (5 elemenata)
            {
                WayPointIndex = 0;//Resetuj index na nula;
                WayPoints.Reverse();//Obrni redosljed pozicija u listi
                //kako bi krenuo unatrag da se krece neprijatelj;
                //Ovime dobijemo efekat da neprijatelj  patrolira svojom rutom;
            }

            this.TargetPosition = WayPoints[WayPointIndex];//Postavimo narednu zeljenu poziciju;
        }
    }
}
