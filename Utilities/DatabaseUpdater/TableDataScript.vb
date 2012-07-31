Public Class TableDataScript
	Inherits Script
	Sub New(ByVal path As String)
		MyBase.New(path)
	End Sub

	Public Overrides Sub LogUpdateInDatabase()
		Return
	End Sub

	Public Overrides Function ShouldBeApplied() As Boolean
		Return True
	End Function
End Class
