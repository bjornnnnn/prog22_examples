using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

string messageString = @"lösenord-hemligt";
Console.WriteLine(messageString);
//Convert the string into an array of bytes.
byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);

//Create the hash value from the array of bytes.
byte[] hashValue = SHA256.HashData(messageBytes);

//Display the hash value to the console.
Console.WriteLine(Convert.ToHexString(hashValue));