using System;

namespace MySteam.Exceptions;

public class UserSearchException(string message) : Exception(message);