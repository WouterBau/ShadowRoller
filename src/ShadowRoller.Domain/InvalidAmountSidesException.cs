using System.Runtime.Serialization;

namespace ShadowRoller.Domain;
[Serializable]
public class InvalidAmountSidesException : ArgumentException
{
    public InvalidAmountSidesException()
    {
        
    }

    protected InvalidAmountSidesException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}