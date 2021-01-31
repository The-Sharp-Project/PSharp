Module CompilerConsole

    Dim WithEvents timer As New Timers.Timer(500) With {
        .AutoReset = True,
        .Enabled = False
    }

    Sub Main()
        Console.Clear()
        WriteHeader()
        Console.WriteLine("  ")
        Console.WriteLine("  [1] Compile project to VisualBasic.NET")
        Console.WriteLine("  [2] Compile project to C#.NET")
        Console.WriteLine("  [3] Compile single file to DLL")
        Console.WriteLine("  [4] Help about the Compiler Console")
        Console.WriteLine("  [5] Exit Compiler Console")
actions:
        Dim key = Console.ReadKey(True).Key
        If key = ConsoleKey.D1 Then
            CompileVB()
        ElseIf key = ConsoleKey.D4 Then
            ShowHelp()
        ElseIf key = ConsoleKey.D5 Then
            End
        Else
            GoTo actions
        End If
    End Sub

    Sub CompileVB()
        Console.Clear()
        WriteHeader()
        Console.WriteLine("  ")
        Console.Write("  Searching for project in this directory")
        timer.Start()
        For Each file As String In FileIO.FileSystem.GetFiles(".")
            If file.EndsWith(".psproj") Then
                timer.Stop()
                Console.WriteLine("  Compilig" + file + "...")
            End If
        Next
        timer.Stop()
        Console.WriteLine("  ")
        Console.WriteLine("  No PSharp Project found in this directory ! Try in another directory !")
        Console.WriteLine("  ")
        Console.WriteLine("  [1] Retry")
        Console.WriteLine("  [2] Cancel")
actions:
        Dim key = Console.ReadKey(True).Key
        If key = ConsoleKey.D1 Then
            CompileVB()
        ElseIf key = ConsoleKey.D2 Then
            Main()
        Else
            GoTo actions
        End If
    End Sub

    Sub ShowHelp()
        Console.Clear()
        WriteHeader()
        Console.WriteLine("  ")
        Console.WriteLine("  ===== GLOSSARY ======")
        Console.WriteLine("  [/command] = optionnal")
        Console.WriteLine("  {/command} = obligatory")
        Console.WriteLine("  ")
        Console.WriteLine("  {/nogui} => Do not show GUI")
        Console.WriteLine("  [/noprogress] => Do not show progress")
        Console.WriteLine("  [/showtrace] => Show the entire trace of the compiler")
        Console.WriteLine("  {/project='string'} => Location of the project file")
        Console.WriteLine("  [/language='vb/cs' => Compile in the specified language. Default is VB")
        Console.WriteLine("  ")
        Console.WriteLine("  [1] Back")
actions:
        Dim key = Console.ReadKey(True).Key
        If key = ConsoleKey.D1 Then
            Main()
        Else
            GoTo actions
        End If
    End Sub

    Private Sub timer_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles timer.Elapsed
        Console.Write(".")
    End Sub

    Function WriteHeader()
        Console.WriteLine("  +===========================================================+")
        Console.WriteLine("  |                PSHARP CONSOLE COMPILER                    |")
        Console.WriteLine("  |                        BETA 1.0                           |")
        Console.WriteLine("  |                      PSHARP V1.0                          |")
        Console.WriteLine("  +===========================================================+")
        Return True
    End Function

End Module
