using UnityEngine;

namespace Tanks.Scripts
{
    [CreateAssetMenu(fileName = "TankData",menuName ="Tanks/Tank data" )]
    //Ovo nam omoguci da kreiramo meni za assets, damo mu menuName i fileName
    public class TankData : ScriptableObject  //Naslijedimo klasu ScriptableObject
    {
        [field: SerializeField]
        public GameObject TankModel { get; set; } //Model tenka iz Unity-a;
        
        [field: SerializeField]
        public float Speed { get; set; }//Brzina za kretanje tenka;
    }
}
