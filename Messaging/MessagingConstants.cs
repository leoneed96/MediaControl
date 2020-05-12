using System;
using System.Collections.Generic;
using System.Text;

namespace Messaging
{
    public class MessagingConstants
    {
        public static class ClientMessages
        {
            public const string CHECK_STATE = "CheckState";
            public const string PLAY_PAUSE = "PlayPause";
            public const string INC_VOLUME = "IncVolume";
            public const string DEC_VOLUME = "DecVolume";
            public const string SET_VOLUME = "SetVolume";
            public const string NEXT = "Next";
            public const string PREV = "Prev";
        }

        public static class ServerMessages
        {
            public const string STATE_RECEIVED = "StateReceived";
            public const string ACTION_COMPLETED = "ActionCompleted";
        }
    }
}
