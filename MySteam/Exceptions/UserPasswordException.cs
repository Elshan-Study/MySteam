using System;

namespace MySteam.Exceptions;

public class UserPasswordException(string message) : Exception(message);
