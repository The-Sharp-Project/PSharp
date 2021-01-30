Imports System.Drawing
Public Class Label

    Public Property Text As String

    Public Property Name As String

    Public Property Location As Point

    Public Property Size As Size

    Public Sub New(Name As String, Optional Text As String = "")
        Me.Name = Name
        Me.Text = Text
    End Sub

End Class
