using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Benefit : MonoBehaviour
{

    public float fatigue;
    public float food;


    protected StateMachine<Fsm_Benefit> fsm;

    public StateMachine<Fsm_Benefit> FSM => fsm;

    private void Start()
    {
        fsm = new StateMachine<Fsm_Benefit>(this, new StateIdle2());
        fsm.AddStateList(new StateSleep());
        fsm.AddStateList(new StateFood());
    }

    private void Update()
    {
        fsm.Update(Time.deltaTime);
        Debug.Log(fsm.getNowState);
    }
}
