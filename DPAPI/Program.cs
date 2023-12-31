﻿using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;


// add data protection services
var serviceCollection = new ServiceCollection();
serviceCollection.AddDataProtection();
var services = serviceCollection.BuildServiceProvider();

// create an instance of MyClass using the service provider
var instance = ActivatorUtilities.CreateInstance<MyClass>(services);
instance.RunSample();


public class MyClass
{
    IDataProtector _protector;

    // the 'provider' parameter is provided by DI
    public MyClass(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("Contoso.MyClass.v1");
    }

    public void RunSample()
    {
        Console.Write("Skriv nått: ");
        string input = Console.ReadLine();

        // protect the payload
        string protectedPayload = _protector.Protect(input);
        Console.WriteLine($"Krypterat meddelande: {protectedPayload}");

        // unprotect the payload
        string unprotectedPayload = _protector.Unprotect(protectedPayload);
        Console.WriteLine($"Okrypterat meddelande: {unprotectedPayload}");
    }
}

