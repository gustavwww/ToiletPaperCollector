using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ServerListener {
    void commandReceived(ServerCommand cmd);
    void exceptionOccurred(Exception e);
}
