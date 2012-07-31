Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Public Class CSharpProjectScriptSource
    Inherits ScriptSource
    Friend path As String
    Friend current As Integer
    Friend fileContent As String()
    Friend currentPath As String
    Friend depth As Integer = 0


    Sub New(ByVal path As String)
        Me.path = path
    End Sub

    Public Overrides Sub FillTables()
        currentPath = New IO.FileInfo(path).Directory.FullName + "\"
        fileContent = System.IO.File.ReadAllLines(path)
        current = 0
        While current < fileContent.Length
            If IsScriptDefinition(Line) Then
                Dim path As String = GetBitInQuotes()
                Select Case GetFirstBit(path)
                    Case "Change Scripts"
                        AddScript(ScriptType.UpdateScript, path)
                    Case "Create Scripts"
                        Select Case GetSecondBit(path)
                            Case "prc" : AddScript(ScriptType.StoredProcedure, path)
                            Case "udf" : AddScript(ScriptType.UserDefinedFunction, path)
                            Case "viw" : AddScript(ScriptType.View, path)
                            Case "trg" : AddScript(ScriptType.Trigger, path)
                        End Select
                    Case "Data Scripts"
                        AddScript(ScriptType.TableData, path)
                End Select
            End If
            MoveToNextLine()
        End While
    End Sub

    Public Shared Function GetFirstBit(ByVal path As String) As String
        Return path.Substring(0, path.IndexOf("\"))
    End Function

    Public Shared Function GetSecondBit(ByVal path As String) As String
        Dim indexOfFirstSlash As Integer = path.IndexOf("\")
        Return path.Substring(indexOfFirstSlash + 1, path.IndexOf("\", indexOfFirstSlash + 1))
    End Function


    Sub MoveToNextLine()
        current += 1
    End Sub
    ReadOnly Property Line() As String
        Get
            If current < fileContent.Length Then
                Return Me.fileContent(current).Trim
            Else
                Return ""
            End If
        End Get
    End Property
    Function GetBitInQuotes() As String
        Return Line.Substring(Line.IndexOf("""") + 1, Line.LastIndexOf("""") - Line.IndexOf("""") - 1).Replace("""""", """")
    End Function

    Shared Function IsScriptDefinition(ByVal line As String) As Boolean
        Try
            Return line.IndexOf("<None Include=""") > -1 And line.IndexOf(".sql"" />") > -1
        Catch ex As System.ArgumentOutOfRangeException
            Return False
        End Try

    End Function
End Class
