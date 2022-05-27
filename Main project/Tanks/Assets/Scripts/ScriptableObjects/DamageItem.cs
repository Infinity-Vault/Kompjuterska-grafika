using Tanks.Behaviours.Core;
using UnityEngine;

namespace Tanks.ScriptableObjects
{
    //Kreiramo putanju za meni i ime fajla
    [CreateAssetMenu(menuName = "Tanks/Items/Damaging Item",fileName = "Damaging Item")]
    public class DamageItem : ScriptableObject //Naslijedimo klasu
    {
        [field: SerializeField]
        public float Damage { get; set; }//Napravimo jedinicu mjere za iznos stete koju ce odredjeno oruzje praviti;
        
        [field: SerializeField]
        private  GameObject Prefab { get; set;}//Objekat koji zakacimo (izgled oruzja);

        public void ApplyDamage(Entity entity)
        {
            entity.Health += Damage;//Oduzmemo zdravlje napadnutom objektu;
            if (entity.Health <= 0)//Ako je zdravlje 0 ili negativno
                entity.HandleDestruction();//Pozovemo metodu za unistenje objekta;
        }
    }
}
