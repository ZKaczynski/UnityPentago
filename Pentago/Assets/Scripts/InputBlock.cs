using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class InputBlock : MonoBehaviour {

    static bool isInputBlocked;
    static bool shouldRefresh;

    static InputBlock() {
        isInputBlocked = false;
        shouldRefresh = false;
    }

    public void Block() {
        isInputBlocked = true;
    }

    public void Unblock() {
        isInputBlocked = false;
        shouldRefresh = true;
    }
    public bool IsBlocked() {
        return isInputBlocked;
    }
    public bool ShouldRefresh() {
        return shouldRefresh;
    }

    public void AlreadyRefreshed() {
        shouldRefresh = false;
    }
    

}
