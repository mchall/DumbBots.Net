using System;

namespace Messaging
{
    /// <summary>
    /// Holds information for the creation of a Message
    /// </summary>
    internal class Message
    {
        internal int SenderID { get; set; }
        internal int ReceiverID { get; set; }
        internal string MessageString { get; set; }
        internal uint DispatchTime { get; set; }

        internal Message(int senderID, int receiverID, string message, uint dispatchTime)
        {
            SenderID = senderID;
            ReceiverID = receiverID;
            MessageString = message;
            DispatchTime = dispatchTime;
        }
    }
}