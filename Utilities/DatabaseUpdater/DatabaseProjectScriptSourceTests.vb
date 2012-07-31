Imports Microsoft.VisualStudio.TestTools.UnitTesting
<TestClass()> _
Public Class DatabaseProjectScriptSourceTests
    <TestMethod()> _
    Sub GoUpALevel_DummyData_Test()
        Dim dp As New DatabaseProjectScriptSource("asds")
        dp.currentPath = ".\hello\"
        dp.GoUpALevel()
        Assert.AreEqual(".\", dp.currentPath)
    End Sub


End Class
