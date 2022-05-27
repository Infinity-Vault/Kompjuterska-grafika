using NaughtyAttributes;//Importujemo ekstenziju za vizualni prikaz healthbar-a
using UnityEngine;

namespace Tanks.Behaviours.Core
{
    //Kreiramo abstraktnu klasu jer zelimo da svaka druga klasa koja naslijedi ovu,
    //implementira metodu HandleDestruction() i da ima Health property;
    public abstract class Entity : MonoBehaviour
    {
        [ProgressBar("Health", 150, EColor.Green)]
        public float Health;//Kreiramo field Health za zdravlje i progressbar koji ce to prikazivati u Unity;
        
        public bool IsRunning { get; set; }//Kako bi znali da li je igrac ziv ili mrtav;

        public abstract void HandleDestruction();//Metoda koju svaka klasa koja naslijedi ovu mora implementirati na svoj nacin;
    }
}
