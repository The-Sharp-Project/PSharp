Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module CompilerConsole

    Dim WithEvents timer As New Timers.Timer(500) With {
        .AutoReset = True,
        .Enabled = False
    }
    Dim exiting As Boolean = False


    Sub Main()
        Console.Clear()
        Console.CursorVisible = False
        WriteHeader()
        Console.WriteLine("  ")
        Console.WriteLine("  [1] Compile project to VisualBasic.NET")
        Console.WriteLine("  [2] Compile project to C#.NET")
        Console.WriteLine("  [3] Compile single file to DLL")
        Console.WriteLine("  [4] Help about the Compiler Console")
        Console.WriteLine("  [5] Check for problems with PSharp libs")
        Console.WriteLine("  [6] Exit Compiler Console")

        If exiting Then
            Console.WriteLine("  ")
            Console.WriteLine("  Press [6] again to exit, else press another key")
        End If

actions:
        Dim key = Console.ReadKey(True).Key
        If key = ConsoleKey.D1 Then
            CompileVB()
        ElseIf key = ConsoleKey.D4 Then
            ShowHelp()
        ElseIf key = ConsoleKey.D5 Then
            CheckLibs()
        ElseIf key = ConsoleKey.D6 Then
            If exiting Then
                End
            Else
                exiting = True
                Main()
            End If
        Else
            exiting = False
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

    Sub CheckLibs()
        Console.Clear()
        WriteHeader()
        Console.WriteLine("  ")
        Console.WriteLine("  [V] = OK")
        Console.WriteLine("  [!] = Outdated")
        Console.WriteLine("  [X] = Invalid or inexistent file")
        Console.WriteLine("  ")
        Console.WriteLine("  Checking PSharp Framework libs...")
        Console.WriteLine("  ")
        Dim jsonfile As JObject = JObject.Parse(FileIO.FileSystem.ReadAllText("frameworklibs.json"))
        Dim list As List(Of JToken) = jsonfile("latest")("components").Children.ToList
        For Each token As JToken In list
            Dim obj As FrameworkLibsUtils.FrameworkLib = token.ToObject(Of FrameworkLibsUtils.FrameworkLib)
            If FileIO.FileSystem.FileExists(obj.Name) Then
                Dim assembly = FileVersionInfo.GetVersionInfo(obj.Name)
                If assembly.FileVersion = obj.Version Then
                    Console.WriteLine(String.Format("  {0} V{1} [V]", obj.Name, obj.Version))
                Else
                    Console.WriteLine(String.Format("  {0} V{1} [!]", obj.Name, obj.Version))
                End If
            Else
                Console.WriteLine(String.Format("  {0} V{1} [X]", obj.Name, obj.Version))
            End If
        Next
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
        Console.WriteLine("  +========== WARNING ==========+")
        Console.WriteLine("  |     NOT YET IMPLEMENTED     |")
        Console.WriteLine("  |         COMING SOON         |")
        Console.WriteLine("  +=============================+")
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
