Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Reflection
Imports OfficeOpenXml.Style
Imports OfficeOpenXml
Imports System.IO
Imports Microsoft.IdentityModel.Clients.ActiveDirectory
Imports Microsoft.Rest.Azure.Authentication
Imports System.Threading
Imports Microsoft.Azure.Management.DataLake.Store
Imports Microsoft.WindowsAzure.Storage
Imports Microsoft.WindowsAzure.Storage.Auth
Imports Microsoft.WindowsAzure.Storage.Blob
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome


<TestClass()> Public Class T1ADLSToDW
    <TestMethod()> Sub T1CleanupDW()
        Dim i, n
        Dim f1 As FileInfo = New FileInfo(AssemblyDirectory & "\data\Config_DW.xlsx")
        Dim f2 As FileInfo = New FileInfo(AssemblyDirectory & "\data\ADLS_DW.xlsx")
        Dim objexcel As ExcelPackage = New ExcelPackage(f1)
        Dim objexcel2 As ExcelPackage = New ExcelPackage(f2)
        Dim objsheet1 As ExcelWorksheet = objexcel.Workbook.Worksheets("query")
        Dim objsheet As ExcelWorksheet = objexcel.Workbook.Worksheets("mapping")
        Dim objsheet2 As ExcelWorksheet = objexcel2.Workbook.Worksheets("config")
        Dim objsheet3 As ExcelWorksheet = objexcel2.Workbook.Worksheets("summary")
        If objsheet.Cells(2, 9).Value = "Yes" Then
            For i = 0 To 20
                objsheet2.Cells(i + 2, 1).Value = "NA"
                objsheet2.Cells(i + 2, 2).Value = "NA"
                objsheet2.Cells(i + 2, 3).Value = "NA"
                objsheet2.Cells(i + 2, 4).Value = "NA"
                objsheet2.Cells(i + 2, 5).Value = "NA"
                objsheet2.Cells(i + 2, 6).Value = "NA"
                objsheet2.Cells(i + 2, 7).Value = "NA"
                objsheet2.Cells(i + 2, 8).Value = "NA"
                objsheet2.Cells(i + 2, 9).Value = "NA"
                objsheet2.Cells(i + 2, 10).Value = "NA"
                objsheet2.Cells(i + 2, 11).Value = "NA"
                objsheet2.Cells(i + 2, 12).Value = "NA"
            Next
            For n = 0 To 2
                objsheet3.Cells(n + 4, 6).Value = ""
                objsheet3.Cells(n + 4, 7).Value = ""
                objsheet3.Cells(n + 4, 8).Value = ""
                objsheet3.Cells(n + 4, 9).Value = ""
            Next
        End If
        objexcel.Save()
        objexcel2.Save()
        Assert.AreEqual(1, 1)

    End Sub

    <TestMethod()> Sub T2ConfigDW()

        Dim con, rs, value, value2, i, j, objfields, c, d, con2, rs2, k, l, m, n, z, t
        Dim f1 As FileInfo = New FileInfo(AssemblyDirectory & "\data\Config_DW.xlsx")
        Dim f2 As FileInfo = New FileInfo(AssemblyDirectory & "\data\ADLS_DW.xlsx")
        Dim objexcel As ExcelPackage = New ExcelPackage(f1)
        Dim objexcel2 As ExcelPackage = New ExcelPackage(f2)
        Dim objsheet1 As ExcelWorksheet = objexcel.Workbook.Worksheets("query")
        Dim objsheet As ExcelWorksheet = objexcel2.Workbook.Worksheets("config")
        Dim objsheet2 As ExcelWorksheet = objexcel.Workbook.Worksheets("config")
        Dim objsheet3 As ExcelWorksheet = objexcel.Workbook.Worksheets("mapping")
        c = 0
        d = 2
        t = 6
        For n = 0 To 4
            l = 2
            For k = 2 To 31
                objsheet2.Cells(2, 1).Value = objsheet3.Cells(k, n + 1).Value
                objsheet2.Cells(2, 2).Value = objsheet3.Cells(k, n + 2).Value
                For m = 2 To 11
                    objsheet2.Cells(m, 3).Value = objsheet3.Cells(l, n + 3).Value
                    l = l + 1
                Next
                objsheet1.Calculate()
                For j = 2 To 2
                    con = CreateObject("adodb.connection")
                    rs = CreateObject("adodb.recordset")
                    con2 = CreateObject("adodb.connection")
                    rs2 = CreateObject("adodb.recordset")
                    With con
                        .ConnectionString = "Driver={SQL Server};server=continuous.database.windows.net;database=Employee;Uid=satish;Pwd=Azure123!;"
                        .Open
                    End With
                    With con2
                        .ConnectionString = "Driver={SQL Server};server=continuous.database.windows.net;database=Employee;Uid=satish;Pwd=Azure123!;"
                        .Open
                    End With
                    If n = 0 Then
                        value2 = objsheet1.Cells(j, t).Value
                        rs.open(objsheet1.Cells(j, t + 1).Value, con)
                        objfields = rs.Fields
                        Do While Not rs.eof
                            For i = 0 To (objfields.Count - 1)
                                value = rs.fields.item(i).Value
                                objsheet.Cells(c + 2, d).Value = value
                                objsheet.Cells(c + 2, (d - 1)).Value = value2
                                c = c + 1
                            Next
                            rs.movenext
                        Loop
                    End If
                    objsheet1.Calculate()
                    objexcel.Save()
                    If n = 4 Then
                        value2 = objsheet1.Cells(j, t).Value
                        rs2.open(objsheet1.Cells(j, t + 1).Value, con2)
                        objfields = rs2.Fields
                        Do While Not rs2.eof
                            For i = 0 To (objfields.Count - 1)
                                value = rs2.fields.item(i).Value
                                objsheet.Cells(c + 2, d).Value = value
                                objsheet.Cells(c + 2, (d - 1)).Value = value2
                                c = c + 1
                            Next
                            rs2.movenext
                        Loop
                    End If
                    objsheet1.Calculate()
                    objexcel.Save()
                    rs = Nothing
                    con = Nothing
                    rs2 = Nothing
                    con2 = Nothing
                    c = 0
                    d = d + 2
                Next
                k = k + 9
                For z = 2 To 11
                    objsheet2.Cells(2, 1).Value = "NA"
                    objsheet2.Cells(2, 2).Value = "NA"
                    objsheet2.Cells(z, 3).Value = "NA"
                Next
                objsheet1.Calculate()
                objexcel.Save()
                t = t + 2
            Next
            n = n + 3
        Next
        objsheet3.Cells(2, 9).Value = "Yes"
        objexcel.Save()
        objexcel2.Save()
        Assert.AreEqual(1, 1)

    End Sub

    <TestMethod()> Sub T3ADLSDW()

        Dim con, rs, value, i, j, objfields, c, d, e, f, g, h, k
        Dim f1 As FileInfo = New FileInfo(AssemblyDirectory & "\data\ADLS_DW.xlsx")
        Dim objexcel As ExcelPackage = New ExcelPackage(f1)
        Dim objsheet1 As ExcelWorksheet = objexcel.Workbook.Worksheets("query")
        Dim objsheet As ExcelWorksheet = objexcel.Workbook.Worksheets("summary")
        Dim objsheet5 As ExcelWorksheet = objexcel.Workbook.Worksheets("Final")
        objsheet.Cells(4, 6).Value = objsheet1.Cells(1, 1).Value
        objsheet.Cells(4, 7).Value = "Number of Test Cases"
        objsheet.Cells(4, 8).Value = "Number of Test Cases Passed"
        objsheet.Cells(4, 9).Value = "Number of Test Cases Failed"
        c = 2
        d = 0
        e = 0
        f = 0
        g = 0
        h = 0
        k = 0
        objsheet1.Calculate()
        For j = 2 To 16
            value = "Pass"
            con = CreateObject("adodb.connection")
            rs = CreateObject("adodb.recordset")
            With con
                .ConnectionString = "Driver={SQL Server};server=continuous.database.windows.net;database=Employee;Uid=satish;Pwd=Azure123!;"
                .Open
            End With
            If objsheet1.Cells(j, 5).Value = "Y" Then
                rs.open(objsheet1.Cells(j, 6).Value, con)
                objfields = rs.Fields
                Do While Not rs.eof
                    For i = 0 To (objfields.Count - 1)
                        value = IsNothing(rs.fields.item(i).Value)
                        If value = "False" Then
                            Exit For
                        End If
                    Next
                    rs.movenext
                Loop
                If value = "False" Then
                    objsheet1.Cells(j, 4).Value = "Fail"
                Else
                    objsheet1.Cells(j, 4).Value = "Pass"
                End If
                rs = Nothing
                con = Nothing
            End If
            objsheet.Calculate()
            If objsheet1.Cells(j, 1).Value <> objsheet.Cells(c + 2, 6).Value Then
                objsheet.Cells(c + 3, 6).Value = objsheet1.Cells(j, 1).Value
                c = c + 1
                If objsheet1.Cells(j, 4).Value = "Pass" Then
                    objsheet.Cells(c + 2, 7).Value = d
                    g = g + 1
                    e = 1
                    f = 0
                    h = h + 1
                    objsheet.Cells(c + 2, 8).Value = e
                ElseIf objsheet1.Cells(j, 4).Value = "Fail" Then
                    objsheet.Cells(c + 2, 7).Value = d
                    g = g + 1
                    f = 1
                    e = 0
                    k = k + 1
                    objsheet.Cells(c + 2, 9).Value = f
                Else
                    objsheet.Cells(c + 2, 8).Value = 0
                    objsheet.Cells(c + 2, 9).Value = 0
                End If
            Else

                If objsheet1.Cells(j, 4).Value = "Pass" Then
                    d = d + 1
                    objsheet.Cells(c + 2, 7).Value = d
                    g = g + 1
                    e = e + 1
                    h = h + 1
                    objsheet.Cells(c + 2, 8).Value = e
                ElseIf objsheet1.Cells(j, 4).Value = "Fail" Then
                    d = d + 1
                    objsheet.Cells(c + 2, 7).Value = d
                    g = g + 1
                    f = f + 1
                    k = k + 1
                    objsheet.Cells(c + 2, 9).Value = f
                End If
            End If
        Next
        objsheet.Cells(c + 3, 6).Value = "Total"
        objsheet.Cells(c + 3, 7).Value = g
        objsheet.Cells(c + 3, 8).Value = h
        objsheet.Cells(c + 3, 9).Value = k
        objsheet5.Calculate()
        objexcel.Save()
        Assert.AreEqual(1, 1)

    End Sub
    <TestMethod()> Sub T4Result()
        Dim f1 As FileInfo = New FileInfo(AssemblyDirectory & "\data\ADLS_DW.xlsx")
        Dim objexcel As ExcelPackage = New ExcelPackage(f1)
        Dim objsheet As ExcelWorksheet = objexcel.Workbook.Worksheets("Final")
        Assert.AreEqual(objsheet.Cells(2, 1).Value, "Pass")
    End Sub
    Public ReadOnly Property AssemblyDirectory As String
        Get

            Dim codeBase As String = Assembly.GetExecutingAssembly().CodeBase
            Dim uri As UriBuilder = New UriBuilder(codeBase)
            Dim path As String = uri.Path

            Return System.IO.Path.GetDirectoryName(path)
        End Get
    End Property
End Class
