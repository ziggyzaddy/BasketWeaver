
#if SIMD
    private static void RunSIMD(string directory)
    {
        DirectoryInfo d = new DirectoryInfo(directory.Replace("BasketWeaver", ""));

        var stopwatch = new System.Diagnostics.Stopwatch();
        foreach (string file in System.IO.Directory.GetFiles(d.FullName, "starsystemdef*.json", SearchOption.AllDirectories))
        {

            byte[] bytes = File.ReadAllBytes(file);
            unsafe
            {
                fixed (byte* ptr = bytes)
                {
                    stopwatch.Start();
                    var parse = SimdJsonN.ParseJson(bytes);
                    stopwatch.Stop();
                    if (!parse.IsValid())
                    {
                        Console.Log($"Error Parsing: {file}: {parse.GetErrorCode()}");
                    }
                    else
                    {
                        using (var iterator = new ParsedJsonIteratorN(parse))
                        {

                            while (iterator.MoveForward())
                            {
                                if (iterator.IsString() && iterator.GetUtf16String() == "Id")
                                {
                                    iterator.MoveForward();
                                    Console.Log("Id: " + iterator.GetUtf16String());
                                }
                                if (iterator.IsString() && iterator.GetUtf16String() == "Name")
                                {
                                    iterator.MoveForward();
                                    Console.Log("Name: " + iterator.GetUtf16String());
                                }
                                if (iterator.IsString() && iterator.GetUtf16String() == "Details")
                                {
                                    iterator.MoveForward();
                                    Console.Log("Details: " + iterator.GetUtf16String());
                                }
                            }
                        }

                    }
                }
            }
        }
        Console.Log($"INIT: SIMD JSON TEST Took {stopwatch.Elapsed.TotalMilliseconds} ms");
    }


    private static void RunNewtonsoft(string directory)
    {
        DirectoryInfo d = new DirectoryInfo(directory.Replace("BasketWeaver", ""));

        var stopwatch = new System.Diagnostics.Stopwatch();
        foreach (string fileStr in System.IO.Directory.GetFiles(d.FullName, "starsystemdef*.json", SearchOption.AllDirectories))
        {

            using (StreamReader file = File.OpenText(fileStr))
            {
                string json = file.ReadToEnd();

                stopwatch.Start();
                JToken jobject = JToken.Parse(json);
                stopwatch.Stop();
            }
        }
        Console.Log($"INIT: NEWTONSOFT TEST Took {stopwatch.Elapsed.TotalMilliseconds} ms");
    }

#endif
