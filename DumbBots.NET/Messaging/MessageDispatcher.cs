using System;
using System.Collections.Generic;
using System.Linq;

namespace Messaging
{
    /// <summary>
    /// Sends and manages the message queue
    /// </summary>
    internal static class MessageDispatcher
    {
        private static List<Message> _messageQueue;

        static MessageDispatcher()
        {
            _messageQueue = new List<Message>();
        }

        internal static void AddMessageToQueue(Message message)
        {
            if (_messageQueue.Count != 0)
            {
                bool added = false;
                bool exists = false;

                //Find if the message already exists
                for (int i = 0; i < _messageQueue.Count; i++)
                {
                    if ((_messageQueue[i].MessageString == message.MessageString) && (_messageQueue[i].ReceiverID == message.ReceiverID))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    for (int i = 0; i < _messageQueue.Count; i++)
                    {
                        if (_messageQueue[i].DispatchTime >= message.DispatchTime)
                        {
                            _messageQueue.Insert(i, message);
                            added = true;
                            break;
                        }
                    }
                    if (!added) //Add at end
                    {
                        _messageQueue.Insert(_messageQueue.Count, message);
                    }
                }
            }
            else
            {
                _messageQueue.Add(message); //Make first message
            }
        }

        internal static Message TryFetchMessage(int entityId, uint time)
        {
            Message msg = _messageQueue.FirstOrDefault(m => m.ReceiverID == entityId && m.DispatchTime <= time);
            if (msg != null)
                _messageQueue.Remove(msg);
            return msg;
        }

        internal static void ClearQueue()
        {
            _messageQueue.Clear();
        }
    }
}