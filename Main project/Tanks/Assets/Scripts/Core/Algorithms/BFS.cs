using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks.Core.Algorithms
{
    public class BFS//Nema nasljedjivanja, regularna C# klasa
    {
        private Node[,] MapGraph { get; set; } //Sami graf cvorova
        private List<Node> Path { get; set; }  //Finalna putanja cvorova

        public BFS(Node[,] mapGraph)//User def ctor;
        {
            MapGraph = mapGraph;//Preuzmemo graf cvorova u local varijablu;
            Path = new List<Node>();//Inicijaliziramo listu;
        }

        //Metoda koja ce da generise put izmedju dva cvora (spoji pocetni i krajnji)
        public List<Node> GetPathToTarget(Vector2Int startNode, Vector2Int endNode)
        {
            var nodePathLinkedList = PathFind(MapGraph[startNode.x, startNode.y],//Posaljemo cvor na x i y indexu kao startNode i isto tako za endNode;
                MapGraph[endNode.x, endNode.y]);//Dobijemo pocetak nase putanje;
            GeneratePathPoints(nodePathLinkedList);//Rekurzivno punimo Path listu cvorova (odnosno nas put);
            return Path;//Vratimo putanju cvorova;
        }

        private Node PathFind(Node startNode, Node endNode)
        {
            var BfsQueue = new Queue<Node>();//Kreiramo  red cvorova;

            endNode.IsVisited = true;//Krajnji cvor oznacimo posjecenim;
            
            BfsQueue.Enqueue(endNode);//Idemo odazada pa zato ubacimo u red krajni cvor;
            while (BfsQueue.Count != 0)//Sve dok red ima elemenata (cvorova) u sebi:
            {
                var currentNode = BfsQueue.Dequeue();//Trenutni cvor postanje onaj prvi dodan;
                foreach (var node in currentNode.Adjacency)//Za svaki susjed iz liste susjedstva trenutnog cvora
                {
                    if(node.IsVisited)//Ukoliko je posjecen preskoci ga;
                        continue;
                    node.Parent = currentNode;//Postavi susjedu da je mu je roditelj trenutni cvor;
                    if (node == startNode)//Ako je susjed pocetni cvor
                        return node.Parent;//Vrati njegovog roditelja (pocetak);
                    node.IsVisited = true;//Oznaci susjeda kao posjecenog;
                    BfsQueue.Enqueue(node);//Dodaj susjeda u red;
                }
            }
            return null;//Ukoliko ne moze da se kreira put izmedju cvorova, vrati null;
        }

        private void GeneratePathPoints(Node node)
        {
            if(node==null)//Ako cvor ne postoji izadji iz rekurzije;
                return;
            if (node.Parent == null)//Ako roditelj ne postoji dosli smo do zadnjeg cvora
            {
                Path.Add(node);//Stoga ga samo dodaj i izadji iz rekurzije;
                return;
            }
            Path.Add(node);//Inace, u opstem slucaju samo dodamo cvor;
            GeneratePathPoints(node.Parent);//Pozovemo rekurzivno metodu za roditelja od trenutnog cvora;
        }
    }
}
