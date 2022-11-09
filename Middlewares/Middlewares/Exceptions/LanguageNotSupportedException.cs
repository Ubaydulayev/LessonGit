﻿using System.Runtime.CompilerServices;

namespace Middlewares.Exceptions;

public class LanguageNotSupportedException : Exception
{
    public LanguageNotSupportedException() : base("Only 'uz', 'en' supported!")
    {

    }
}