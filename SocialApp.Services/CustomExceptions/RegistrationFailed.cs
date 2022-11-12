using System;

namespace Services.CustomExceptions;

public class RegistrationFailed : Exception
{
    public RegistrationFailed()
    {
        
    }

    public RegistrationFailed(string message) : base(message)
    {
        
    }
}