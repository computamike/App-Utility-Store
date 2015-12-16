using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[SetUpFixture]
public class GlobalSetup
{
    [SetUp]
    public void ShowSomeTrace()
    {
        Trace.WriteLine("It works..."); // won't actually trace
    }
}