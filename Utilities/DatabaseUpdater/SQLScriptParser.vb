Imports System.Collections.Generic
Public Class SQLScriptParser
	'parameter values
	Dim scriptContents() As Char
	Dim AddWithEncryption As Boolean

	'initial values
	Dim WithEncryptionAdded As Boolean = False
	Dim bracketCount As Integer = 0
	Dim pos As Integer = -1

	Protected Sub New(ByVal script As String, ByVal withEncryption As Boolean)
		Me.scriptContents = script.ToCharArray
		Me.AddWithEncryption = withEncryption
		inSQLCode()
	End Sub



	Dim sections As New list(Of String)
	Dim currentSection As New Text.StringBuilder
	Dim c As Char
	Dim IsCreateSection As Boolean

	Shared Function Parse(ByVal script As String, ByVal withEncryption As Boolean) As String()
		Dim parser As New SQLScriptParser(script, withEncryption)
		Return parser.sections.ToArray()
	End Function

#Region "parser loops"

	Dim WithEncryptionInsertToken As String = "as"
	Sub inSQLCode()
		Do

			c = NextChar()
			If IsEndOfScript() Then Exit Do
			If AddWithEncryption AndAlso Not IsCreateSection AndAlso TestForToken("create", True) Then IsCreateSection = True
			If IsCreateSection AndAlso Not WithEncryptionAdded AndAlso bracketCount = 0 AndAlso WithEncryptionInsertToken = "as" Then
				If TestForToken("TRIGGER", True) Then
					WithEncryptionInsertToken = "for"
				End If
			End If
			If IsCreateSection AndAlso Not WithEncryptionAdded AndAlso bracketCount = 0 Then
				If TestForToken(WithEncryptionInsertToken, True) Then
					currentSection.Append(" WITH ENCRYPTION ")
					WithEncryptionAdded = True
					WithEncryptionInsertToken = "as"
				End If
			End If
			If c = "(" Then bracketCount += 1
			If c = ")" Then bracketCount -= 1

			If EnteringComment() Then
				InComment()
			ElseIf EnteringCommentBlock() Then
				InCommentBlock()
			ElseIf c = "'"c Then
				InTextRegion("'"c, "'"c)
			ElseIf c = """"c Then
				InTextRegion(""""c, """"c)
			ElseIf c = "["c Then
				InTextRegion("["c, "]"c)
			ElseIf TestForToken("go", True) Then
				pos += 1
				If currentSection.ToString.Trim.Length > 0 Then sections.Add(currentSection.ToString)
				bracketCount = 0
				WithEncryptionAdded = False
				IsCreateSection = False
				currentSection = New Text.StringBuilder
			Else
				currentSection.Append(c)
			End If
		Loop
		sections.Add(currentSection.ToString)
	End Sub
	Private Sub InTextRegion(ByVal startChar As Char, ByVal endChar As Char)
		currentSection.Append(c)
		Do
			c = NextChar()
			If IsEndOfScript() Then Exit Do
			If isDoubleExitCharacter(endChar) Then
				currentSection.Append(endChar).Append(endChar)
				c = NextChar()
			ElseIf c = endChar Then
				currentSection.Append(c)
				Exit Do
			Else
				currentSection.Append(c)
			End If
		Loop
	End Sub
	Private Sub InComment()
		currentSection.Append(c)
		Do
			c = NextChar()
			If IsEndOfScript() Then Exit Do
			currentSection.Append(c)
			If ExitingComment() Then
				Exit Do
			End If
		Loop
	End Sub
	Private Sub InCommentBlock()
		currentSection.Append(c)
		Do
			c = NextChar()
			If IsEndOfScript() Then Exit Do
			currentSection.Append(c)
			If exitingCommentBlock() Then
				currentSection.Append(NextChar)
				Exit Do
			End If
		Loop
	End Sub
#End Region
#Region "parser functions"
	Function EnteringCommentBlock() As Boolean
		Return c = "/" AndAlso peek(1) = "*"
	End Function
	Function EnteringComment() As Boolean
		Return c = "-" AndAlso peek(1) = "-"
	End Function
	Function ExitingComment() As Boolean
		Return c = vbCr OrElse c = vbLf
	End Function
	Function exitingCommentBlock() As Boolean
		Return c = "*" AndAlso peek(1) = "/"
	End Function
	Function isDoubleExitCharacter(ByVal endChar As Char) As Boolean
		Return c = endChar AndAlso peek(1) = endChar
	End Function

	Private Function TestForToken(ByVal text As String, ByVal hasWhiteSpaceEitherSide As Boolean) As Boolean
		Dim array() As Char = text.ToLower.ToCharArray
		If hasWhiteSpaceEitherSide Then
			If Not Char.IsWhiteSpace(peek(-1)) AndAlso Not (peek(-2) = "*" And peek(-1) = "/") Then Return False
			If Not Char.IsWhiteSpace(peek(array.Length)) AndAlso Not (peek(array.Length) = "/" And peek(array.Length + 1) = "*") Then Return False
		End If
		'check letters are the same
		For i As Integer = 0 To array.Length - 1
			If Char.ToLower(peek(i)) <> array(i) Then Return False
		Next
		Return True
	End Function
	Private Function NextChar() As Char
		pos += 1
		If pos >= scriptContents.Length Then Return " "c
		Return scriptContents(pos)
	End Function
	Private Function peek(ByVal amount As Integer) As Char
		If (pos + amount) >= scriptContents.Length _
		Or pos + amount < 0 Then _
		 Return " "c
		Return scriptContents(pos + amount)
	End Function
	Private Function IsEndOfScript() As Boolean
		Return pos >= Me.scriptContents.Length
	End Function
#End Region


End Class
