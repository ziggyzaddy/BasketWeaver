#if BT_PYTHON

public class BTPython
{
// Note, need to reference Microsoft.CSharp for compiler required members
    public static void PythonTest()
    {
    
    Runtime.PythonDLL = "python312.dll";
try
{
    PythonTest();
}
catch (Exception e)
{
    Console.Log($"INIT: Python Test Failed");
    config = new Settings();
    Console.LogException(e);
}

        PythonEngine.Initialize();
        using (Py.GIL())
        {
            dynamic x = 1.0;
            dynamic y = 2.0;
            dynamic z = 0;
            z = x + y;
            Console.Log((double)z);

        }

    }
}

#endif
