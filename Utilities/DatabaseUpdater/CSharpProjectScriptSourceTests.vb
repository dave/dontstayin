Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
<TestClass()> _
Public Class CSharpProjectScriptSourceTests
    <TestMethod()> _
    Public Sub GoUpALevel_Test()
        Assert.AreEqual("Change Scripts", CSharpProjectScriptSource.GetFirstBit("Change Scripts\0001 - create tables.sql"))
    End Sub
    <TestMethod()> _
    Public Sub IsScriptDefinition_Test()
        Assert.AreEqual(True, CSharpProjectScriptSource.IsScriptDefinition("<None Include=""Change Scripts\0001 - create tables.sql"" />"))
        Assert.AreNotEqual(True, CSharpProjectScriptSource.IsScriptDefinition("<None Include=""Change Scripts\0001 - create tables.cs"" />"))

    End Sub

End Class
