Public Class Compiler

    Public Const Version As Integer = 1.0

    Public Function Compile(project As String, language As Language)
        Throw New NotImplementedException("Non-implémenté")
    End Function

End Class

Public Enum Language
    VisualBasic = 0
    CSharp = 1
End Enum
