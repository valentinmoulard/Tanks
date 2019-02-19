using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour {

    //serialize permet d'afficher des variables privées dans l'inspecteur ET sans qu'elle soit accessible depuis un autre script
    [SerializeField]
    List<Transform> _checkPointLists;

    private int _currentCheckPoint;
    private bool _isTraveling;

    NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start () {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _currentCheckPoint = 0;
        if (_navMeshAgent == null)
        {
            Debug.LogError("nav mesh agent not attached to " + gameObject.name);
        }
        else
        {
            DefDestination();
        }
	}
	

    public void Update()
    {
        //ATTENTION : !_navMeshAgent.pathPending est tres important car il evite de skiper des etape(checkpoints) lors de la course
        if (_navMeshAgent.remainingDistance <= 5f && !_navMeshAgent.pathPending)
        {
            if (_currentCheckPoint == 14)
            {
                _currentCheckPoint = 0;
            }
            
            DefDestination();
            _currentCheckPoint += 1;
        }
    }


	//methode qui définit la destiantion
	private void DefDestination() {
        //s'il a une destination
	    if(_checkPointLists != null)
        {
            
            Vector3 targetVector = _checkPointLists[_currentCheckPoint].transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
	}

}
