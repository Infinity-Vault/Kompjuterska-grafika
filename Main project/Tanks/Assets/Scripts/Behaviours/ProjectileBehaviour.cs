using UnityEngine;

namespace Tanks.Behaviours
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        //Donja event funkcija se okine tj. pocne izvrsavati onda kada collider ili RB ovog (this) game object-a tj. projektila
        //dodirne drugi collider ili RB- rigid body;
        private void OnCollisionEnter(Collision collision)//Parametar je objekat koji je trenutno udaren;
        {
            Destroy(collision.gameObject);//Metoda Destroy dati parametar ukloni, stoga uklonimo prvo game object sa kojim se dogodio sudar;
            Destroy(this.gameObject);//Pa onda unistimo sami (this) game object; U nasem slucaju this je ustvari sami projektil;
            //Znaci prvo unistimo ono sta je projektil pogodio, pa onda i njega samog;
        }
    }
}
