using System;
using UnityEngine;

namespace Tanks.Behaviours.Managment
{
    public class InputManager : MonoBehaviour
    {
        public event Action<float, float> OnInput;//Event jer smo kreirali dogadjaj, niz pokazivaca na funkciju istih parametara i povratnog tipa;
        public event Action OnFire;//Ovaj action nam ne treba primati nikakve parametre;
        //Povratni tip Action-a je void;

        private void FixedUpdate() //Event funkcija koja se izvrsava neovisno o framerate-u;
        {
            //Prvo provjerimo da li je  delegat OnInput neprazan, jer postoji mogucnost da bude
            //nuliran (nijedan event mu nije dodijeljen), pa onda preko Input klase koristimo metodu 
            //GetAxisRaw (Raw kako bi input bio kompatibilan i sa kontrolerima a i sa tastaturama),
            //te kupimo horizontalnu i vertikalnu osu;
            OnInput?.Invoke(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));//Za kretanje tenka;
            
        }

        private void Update()//Logiku pucanja prebacimo u Update jer se on izvrsava svaki frame a ne svaki drugi kao u FixedUpdate;
        {
            if(Input.GetKeyDown(KeyCode.Space))//Ukoliko korisnik pritisne space, pozovi sve metode sa potpisom OnFire;
                OnFire?.Invoke();//Provjerimo da li je niz null, radi null reference exceptiona;
        }
    }
}
