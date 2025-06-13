using System;

namespace MySteam.Exceptions;

public class UserExistsException(string message) : Exception(message);