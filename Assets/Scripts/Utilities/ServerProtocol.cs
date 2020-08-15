using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerCommand {
    GET_ID,
    GET_NICKNAME,
    INVALID
}

public static class ServerProtocol {

    public static ServerCommand processMessage(string msg) {

        if (wantId(msg)) {
            return ServerCommand.GET_ID;
        }

        if (wantNick(msg)) {
            return ServerCommand.GET_NICKNAME;
        }

        if (getError(msg) != null) {
            throw new ServerException(getError(msg));
        }

        return ServerCommand.INVALID;
    }

    public static string writeCount() {
        return "count";
    }

    public static string writeNick(string nickname) {
        return "nick:" + nickname.Trim();
    }

    public static string writeId(string id) {
        return "id:" + id.Trim();
    }

    private static string getError(string msg) {

        if (msg.StartsWith("error:")) {

            return msg.Substring(6);
        }
        return null;
    }

    private static bool wantId(string msg) {
        return msg.Equals("want:id");
    }

    private static bool wantNick(string msg) {
        return msg.Equals("want:nickname");
    }

}
