using Tanks.Behaviours.Core;
using Tanks.ScriptableObjects;
using UnityEngine;

namespace Tanks.Behaviours.Controlers
{
    public class DamagingItemControler : MonoBehaviour
    {
        [field: SerializeField]
        private DamageItem DamageItem { get; set; }//Oruzje;

        //Donja metoda se izvrsi onda kada se igrac sudari sa oruzjem;
        private void OnTriggerEnter(Collider other)//Other predstavlja igraca;
        {
            if (other.gameObject.name != "Projectile")//Kako bi sprijecili pad programa ukoliko pogodimo oruzje projektilom tenka jer on nema Entity komponentu i odmah imamo NullReference exception;
            {
                var player = other.GetComponent<Entity>();//Uzmemo entity klasu iz igraca koji je primio stetu;
                DamageItem.ApplyDamage(player);//Posaljemo igraca nad kojim se treba steta primjeniti, smanjiti mu se health; 
            }
        }
    }
}
