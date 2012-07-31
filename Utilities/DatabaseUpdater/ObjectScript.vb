Public Class ObjectScript
    Inherits Script





	Const ExtendedPropertyForLastModifiedName As String = "LastModified"

	Sub New(ByVal path As String, ByVal objectType As String)
		MyBase.New(path)

		Dim filename As String = GetFileName()

		Me._objectName = filename.Substring(0, filename.LastIndexOf("."))

		Me.objectType = objectType
	End Sub
	Public Overrides Function ShouldBeApplied() As Boolean
		Return IsDifferentToTheOneInTheDatabase()
	End Function
	Private Function IsDifferentToTheOneInTheDatabase() As Boolean
		Try
			Dim dbDate As DateTime = DateTime.Parse(SQLExtendedPropertyMethods.Get(ExtendedPropertyForLastModifiedName, Me._objectName, Me.objectType))
			Return Math.Abs(modified.Subtract(CType(dbDate, DateTime)).TotalSeconds) > 1
		Catch ex As NoDatabaseObjectWithThatNameException
			Return True
		End Try
	End Function
	Public Overrides Sub LogUpdateInDatabase()
		SQLExtendedPropertyMethods.Update(ExtendedPropertyForLastModifiedName, Me.modified.ToString, Me._objectName, Me.objectType)
	End Sub

	ReadOnly Property ObjectName() As String
		Get
			Return Me._objectName
		End Get
	End Property

End Class
