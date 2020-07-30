using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomException : Exception
{
  public CustomException(string message): base(message)
    {
    }
}
