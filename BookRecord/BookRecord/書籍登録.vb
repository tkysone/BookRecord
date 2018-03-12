Public Class Frn書籍記録

    Dim cn As System.Data.SqlClient.SqlConnection

    'Sql接続
    Dim strConnectSQL As String = ""
    Dim strSQL As String = ""
    Dim SQLDA As SqlClient.SqlDataAdapter
    Dim SQLDS As New DataSet()

    Private Sub btn表示_Click(sender As Object, e As EventArgs) Handles btn表示.Click

        cn = New System.Data.SqlClient.SqlConnection()

        'SQLServer認証
        strConnectSQL =
      "Data Source =TAKUYA;" &
      "Initial Catalog = BookRecord_DB;" &
      "User ID = sa;" &
      "Password = pass"

        'SQL文
        strSQL = "SELECT * FROM TM利用者 "

        SQLDA = New SqlClient.SqlDataAdapter(strSQL, strConnectSQL)

        'データセットに格納
        SQLDA.Fill(SQLDS, "TM利用者")

        Me.dgv登録.DataSource = SQLDS.Tables("TM利用者")

    End Sub

    '//20180312 Add Start 
    Private Sub btn登録_Click(sender As Object, e As EventArgs) Handles btn登録.Click
        cn = New System.Data.SqlClient.SqlConnection()

        'SQLServer認証
        strConnectSQL =
      "Data Source =TAKUYA;" &
      "Initial Catalog = BookRecord_DB;" &
      "User ID = sa;" &
      "Password = pass"

        'データベース接続
        cn.ConnectionString = strConnectSQL
        cn.Open()

        Dim SQLCMD As New SqlClient.SqlCommand
        Dim SQL As String = ""

        SQL = ""
        SQL += "INSERT INTO TD書籍"
        SQL += "("
        SQL += "書籍CD,"
        SQL += "タイトル,"
        SQL += "利用者CD"
        SQL += ")"
        SQL += "VALUES"
        SQL += "("
        SQL += "'1',"
        SQL += "'test',"
        SQL += "'1234'"
        SQL += ")"
        'SQLコマンド設定
        SQLCMD.CommandText = SQL
        SQLCMD.Connection = cn
        SQLCMD.ExecuteNonQuery()
        SQLCMD.Dispose()
        cn.Close()
        cn.Dispose()
    End Sub
    '//20180312 Add End
End Class
