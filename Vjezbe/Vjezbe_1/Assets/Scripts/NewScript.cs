using UnityEngine;

namespace Vjezbe_1 //Namespace je jako bitno koristiti radi lakse organizacije koda
{
    public class NewScript : MonoBehaviour //Moramo naslijediti klasu MonoBehaviour kako
    //bi imali komunikaciju sa Unity engine-om;
    //Svaku skriptu da bi ona bila primjenjena, moramo povezati sa nekim GameObject-om u Unity;
    {
        //Event funkcije, tj. one se izvrsavaju kada se desi/okine/trigger-uje odredjeni event;
        private void Awake() 
        {
            //Awake se izvrsi kada se skripta koju pisemo ucita;
            Debug.Log("<color=blue>This is called in the Awake method</color>");
        }

        private void Update()
        {
            //Update se izvrsi svakog novog frame-a;
            Debug.Log("<color=red>This is called from the Update method</color>");
        }
    }
}
