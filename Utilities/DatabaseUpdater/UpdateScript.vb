Public Class UpdateScript
    Inherits Script
    Implements IComparable
	Friend Const ExtendedPropertyForVersionInfoName As String = "DatabaseVersion|ModifiedDate|UpdateTimestamp|MostRecentScriptName"
	Private _versionNumber As Integer
	Sub New(ByVal path As String)
		MyBase.New(path)
		Dim versionNumberString As String = Me.GetFileName.Substring(0, Me.GetFileName.IndexOf("-")).Trim
		Me._versionNumber = Integer.Parse(versionNumberString)
		Me.objectType = ScriptType.UpdateScript
	End Sub


	Public Overrides Function ShouldBeApplied() As Boolean
		Try
			Try
				Dim valueOfExtendedPropertyForVersionInfoName As String = SQLExtendedPropertyMethods.Get(UpdateScript.ExtendedPropertyForVersionInfoName)
				Dim databaseVersionNumber As Integer = Convert.ToInt32(valueOfExtendedPropertyForVersionInfoName.Split("|"c)(0))
				Dim modifiedDateOfMostRecentScript As DateTime = DateTime.Parse(valueOfExtendedPropertyForVersionInfoName.Split("|"c)(1))
				If Me.VersionNumber < databaseVersionNumber Then
					Return False
				ElseIf Me.VersionNumber = databaseVersionNumber Then
					Return Me.modified.CompareTo(modifiedDateOfMostRecentScript) > 0
				Else
					Return True
				End If
			Catch ex As NoDatabaseObjectWithThatNameException
				Return True
			End Try
		Catch ex As Exception
			Throw ex
		End Try
	End Function
	Friend ReadOnly Property VersionNumber() As Integer
		Get
			Return Me._versionNumber
		End Get
	End Property


	Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
		Return Me._versionNumber.CompareTo(CType(obj, UpdateScript).VersionNumber)
	End Function
	Public Overrides Sub LogUpdateInDatabase()
		SQLExtendedPropertyMethods.Update(UpdateScript.ExtendedPropertyForVersionInfoName, Me.VersionNumber & "|" & Me.modified.ToString() & "|" & DateTime.Now & "|" & Me.GetFileName)
	End Sub
	
End Class
