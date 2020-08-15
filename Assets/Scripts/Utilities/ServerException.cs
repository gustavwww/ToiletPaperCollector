using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerException : Exception {

    public ServerException(string message) : base(message) { }

}
