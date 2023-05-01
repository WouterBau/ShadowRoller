using System.Runtime.Serialization;

namespace ShadowRoller.Domain;
[Serializable]
public class InvalidAmountSidesException : ArgumentException
{
    protected InvalidAmountSidesException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}