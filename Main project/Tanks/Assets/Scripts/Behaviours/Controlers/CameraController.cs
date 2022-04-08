using UnityEngine;

namespace Tanks.Behaviours.Controlers
{
    public class CameraController : MonoBehaviour
    {
        [field: SerializeField]
        private Transform Player { get; set; }//PlayerController (parent od Tank objekta);
        [field: SerializeField]
        private  Vector3 Offset { get; set; }//Vrijednost za koju moramo pomjeriti kameru prema gore (birds eye view);
        
        //Posto se svakim frame-om moze mjenjati pozicija, logicno je da movement kamere
        //pisemo u Update:
        private void Update()
        {
            //Main kameru u unity, tacnije njenu poziciju, postavimo na poziciju Player-a,
            //sa offsetom od 10 metar na y osi, jer je i kamera dignuta 10 metara (pogled odozgo);
            this.transform.position = this.Player.position + Offset;
        }
    }
}
