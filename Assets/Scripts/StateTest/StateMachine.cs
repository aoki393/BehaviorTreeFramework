using UnityEngine;

// 状态接口
public interface IState {
    void Enter();
    void Update();
    void Exit();
}

// 具体状态
public class IdleState : IState {
    public void Enter() {
        Debug.Log("进入待机状态");
    }
    
    public void Update() {
        // 待机逻辑
    }
    
    public void Exit() {
        Debug.Log("退出待机状态");
    }
}

// 状态机
public class StateMachine {
    private IState currentState;
    
    public void ChangeState(IState newState) {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
    
    public void Update() {
        currentState?.Update();
    }
}
