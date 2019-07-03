using System.Runtime.Serialization;

namespace GotifyWinClient
{
    [DataContract]
    internal class Message
    {
        [DataMember] internal string title;
        [DataMember] internal string message;
    }
}
