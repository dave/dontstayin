Imports Microsoft.VisualStudio.TestTools.UnitTesting
<TestClass()> _
Public Class MainTests

    <TestMethod()> _
    Public Sub IsAFolder_NotAFolder_False()
        Assert.IsFalse(Main.IsAFolder("/\/asasdasd"))
    End Sub
    <TestMethod()> _
    Public Sub IsAFolder_NotAFolder_True()
        Assert.IsTrue(Main.IsAFolder("."))
    End Sub

End Class
