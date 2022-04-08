using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Tanks.Behaviours.Managment
{
    public class PlayerControler : MonoBehaviour
    {
        [field: SerializeField]//Pomocu ovoga imamo privatno polje koje je accessable samo unity developerima ravno iz Engine-a;
        private InputManager InputManager { get; set; }//Moramo kreirati neki GameObject koji ce se referirati na ovaj InputManager kako bi kontrole radile;
        [field: SerializeField] 
        private float Speed { get; set; }  //Isti razlog;    
        //Kreiramo prop brzine jer cemo je koristiti regularno;
        
        [field: SerializeField]
        private GameObject Projectile { get; set; }//Projektil koji ce tenk koristiti za pucanje;
        
        //RigidBody klasu i objekte koristimo kada god zelimo neku silu da izvrsavamo
        //nad nekim GameObject;
        private Rigidbody TankRigidbody { get; set; }
        
        

        private void Start()//Event funkcija koja se okine na samom pocetku;
        {
            InputManager.OnInput += OnInputRecieved;//Dodana metoda u niz za kretanje;
            InputManager.OnFire += OnFirePressed;//Dodana metoda u niz za pucanje;

            TankRigidbody = this.GetComponent<Rigidbody>();//GetComponent metoda vrati komponentu tipa <T> iz odredjenog GameObject;
            //Kod RigidBody u Unity-u moramo staviti Y osu na fixed radi gravitacije (kako ne bi propadao u beskraj);
        }

        private void OnFirePressed()
        {
            //Logika pucanja:
           var projectileToShoot= Instantiate(Projectile,this.transform.position+
                                                         this.transform.forward+new Vector3(0,0.70f,0.10f), Quaternion.identity);//Dodatni novi vektor radi specificnije pozicije kugle;
           //Instanciraj projektil uvijek ISPRED player controllera (zavisi prema gdje je usmjeren tenk);
           //Nakon toga, nadji RB od Projectile objekta, te vektor brzine kretanja  mu pomjeri za 10 metara unaprijed;
           projectileToShoot.GetComponent<Rigidbody>().velocity = this.transform.forward * 10f; 

        }

        //Metoda koja ce se dodati u niz event-a OnInput;
        private void OnInputRecieved(float horizontal, float vertical)
        {
            var vector = new Vector3(0,0,0);//Kreiramo defaultni trodimenzionalni vektor;
            if (horizontal != 0)//Moguce vrijednosti su od 1 do -1, ako nije nula korisnik je kliknuo A ili D ili desno ili lijevo strelice (po x osi);
            {
                vector.x = horizontal;
                vector = vector * (Speed * Time.deltaTime);//Delta time je prosjecno vrijeme izmedju dva frame-a;Uzima se kako bi imali normalan movement;
                //Pomnozimo sa Speed jer ono odredi kojom brzinom cemo se kretati;
                TankRigidbody.MovePosition(vector+this.transform.position);//position prop. klase Transform nam kaze gdje se objekat trenutno nalazi;
                
                //Rotation:
                TankRigidbody.MoveRotation(Quaternion.Euler(Vector3.up * (90 * horizontal))); //Rotiramo za 90 ili -90 jer sekrecemo lijevo ili desno;
                //Koristimo .up jer po toj osi rotiramo (po osi y se uvijek vrsi rotacija objekta);
               Debug.Log("<color=yellow>A or D is being pressed</color>");
            }
            else if(vertical!=0)
            {
                //y osa je za depth odnosno gore dole (gravitacija);
                //Ako se udje u ovaj else prica se o osi z koja kontrolise gore dole W S ili gornja donja strelica;
                vector.z = vertical;
                vector = vector * (Speed * Time.deltaTime);//Kada mnozimo vektor skalarom 3 operacije izvrsavamo tako da je more efficient kada speed i time prvo pomnozimo pa onda to sa vektorom;
                TankRigidbody.MovePosition(vector+this.transform.position);
                //Rotation:
                TankRigidbody.MoveRotation(vertical>0 //Znaci da ostajemo u istom  pravcu ako je vertical pozitivan (>0)
                ? Quaternion.Euler(Vector3.zero) //Stoga ne rotiramo tenk, jer se krecemo gore dole
                :Quaternion.Euler(Vector3.up * (180 * vertical)));//Inace rotiramo ga za 180 (negativno), promjenili smo pravac;
                
                Debug.Log("<color=red>W or S is being pressed</color>");
            }
            else
            {
                Debug.Log("User didn't press any key!");
            }
        }
    }
}
