using System.Collections.Generic;
using UnityEngine;

namespace Tanks.Core
{
    public class Node //Normal C# class, nema nasljedjivanja MonoBehaviour
    {
        public Vector3 NodePosition { get; set; }//Pozicija cvora;
        
        public Node Parent { get; set; }//Roditelj svakog cvora;
        public List<Node> Adjacency { get; set; }//Lista susjeda;
        public bool IsVisited  { get; set; }//Posjecen  da/ne;
        public bool IsWalkable { get; set; }//Da li se radi o kocki po kojoj se moze hodati ili ne;
        public GameObject Block { get; set; }//Samo zbog vizualizacije;

        public Node(Vector3 nodePosition,bool isWalkable)//User def ctor
        {
            IsVisited = false;//Pri samom kreiranju svaki cvor je ne posjecen;
            IsWalkable = isWalkable;
            NodePosition = nodePosition;
            Adjacency= new List<Node>();//Inicijaliziranje memorije;
        }
    }
}
