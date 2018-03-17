Imports System.Data.SqlClient

Public Class Frn書籍記録

    Dim cn As System.Data.SqlClient.SqlConnection

    'Sql接続
    Dim strConnectSQL As String = ""
    Dim strSQL As String = ""
    Dim SQLDA As SqlClient.SqlDataAdapter
    Dim SQLDS As New DataSet()

    '利用者CD
    Dim userCD As String = ""

    Private Sub Frn書籍記録_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '//20180313 利用者CD Start
        Dim conStr As String = "Data Source=TAKUYA;Initial Catalog=BookRecord_DB;User ID=sa;Password=pass"
        Dim con As New SqlConnection(conStr)
        con.Open()
        Dim userCDCMD As String = "SELECT 利用者CD FROM TM利用者"
        'コマンドの作成
        Dim cmd As New SqlCommand(userCDCMD, con)
        'コマンドの実行
        Dim myreader As SqlDataReader = cmd.ExecuteReader()
        While myreader.Read()
            userCD = myreader.Item("利用者CD").ToString
        End While
        '//20180313利用者CD End
    End Sub

    Private Sub btn表示_Click(sender As Object, e As EventArgs) Handles btn表示.Click

        ''表示処理
        '  cn = New System.Data.SqlClient.SqlConnection()

        '  'SQLServer認証
        '  strConnectSQL =
        '"Data Source =TAKUYA;" &
        '"Initial Catalog = BookRecord_DB;" &
        '"User ID = sa;" &
        '"Password = pass"

        '  'SQL文
        '  strSQL = "SELECT * FROM TD書籍 "

        '  SQLDA = New SqlClient.SqlDataAdapter(strSQL, strConnectSQL)

        '  'データセットに格納
        '  SQLDA.Fill(SQLDS, "TM書籍")

        '  Me.dgv登録.DataSource = SQLDS.Tables("TM書籍")

        Dim My_TBL As String = Nothing
        Dim My_DBN As String = Nothing
        Dim My_SQL As String = Nothing
        Dim My_DST As DataSet = dsdgv登録
        Dim My_DGV As DataGridView = dgv登録
        Dim My_BDN As BindingNavigator = bndgv登録
        Dim My_BDS As BindingSource = bsdgv登録

        'DataSet内に作るDataTableの名前
        'DataGridViewと同じ名前にすると使い回しする際、分かりやすい
        My_TBL = "TestTable001"

        'SQLServerのデーターベース名
        My_DBN = "TestDB"

        'SQLServerに問い合わせするクエリ
        My_SQL = "SELECT TOP 15 * FROM TD書籍"

        SQLDB_TO_DGV_BNV_DATASETs(My_SQL, My_DGV, My_BDN, My_BDS, My_DST, My_TBL, My_DBN)

        'dgvの制約
        Restrictdgv()
    End Sub

    '//20180312 Add Start 
    Private Sub btn登録_Click(sender As Object, e As EventArgs) Handles btn登録.Click
        ''登録処理
        'Dim registrationValue As Boolean = "False"
        'registrationValue = checkInputValue()

        'If registrationValue = True Then
        '    cn = New System.Data.SqlClient.SqlConnection()
        '    'SQLServer認証
        '    strConnectSQL =
        '  "Data Source =TAKUYA;" &
        '  "Initial Catalog = BookRecord_DB;" &
        '  "User ID = sa;" &
        '  "Password = pass"

        '    'データベース接続
        '    cn.ConnectionString = strConnectSQL
        '    cn.Open()

        '    Dim SQLCMD As New SqlClient.SqlCommand
        '    Dim SQL As String = ""

        '    SQL = ""
        '    SQL += "INSERT INTO TD書籍"
        '    SQL += "("
        '    SQL += "書籍CD,"
        '    SQL += "タイトル,"
        '    SQL += "利用者CD"
        '    SQL += ")"
        '    SQL += "VALUES"
        '    SQL += "("
        '    SQL += "'1',"
        '    SQL += "'" & txtタイトル.Text & "',"
        '    SQL += "'" & userCD & "'"
        '    SQL += ")"
        '    'SQLコマンド設定
        '    SQLCMD.CommandText = SQL
        '    SQLCMD.Connection = cn
        '    SQLCMD.ExecuteNonQuery()
        '    SQLCMD.Dispose()
        '    cn.Close()
        '    cn.Dispose()
        '    MessageBox.Show("登録しました。")
        'Else
        '    MessageBox.Show("タイトルを入力してください。")
        'End If



    End Sub
    '//20180312 Add End

    '登録処理
    Public Sub SQLDB_TO_DGV_BNV_DATASETs(ByVal My_SQL As String, ByVal My_DGV As DataGridView, ByVal My_BNV As BindingNavigator, ByVal My_BDS As BindingSource, ByVal My_DtSet As DataSet, ByVal My_Table As String, ByVal My_DB As String)

        Try

            Dim con = New System.Data.SqlClient.SqlConnection()

            '------ SQL Server認証で接続 ------ 
            con.ConnectionString =
             "Data Source =TAKUYA;" &
              "Initial Catalog = BookRecord_DB;" &
              "User ID = sa;" &
              "Password = pass"

            con.Open()

            My_BDS.DataSource = Nothing
            My_BDS.DataMember = Nothing
            My_DGV.DataSource = Nothing
            My_BNV.BindingSource = Nothing

            For i = 0 To My_DtSet.Tables.Count - 1
                If My_DtSet.Tables(i).TableName = My_Table Then
                    My_DtSet.Tables.Remove(My_Table)
                    Exit For
                End If
            Next

            Dim Sqlda As New SqlClient.SqlDataAdapter
            Sqlda.SelectCommand = New SqlClient.SqlCommand(My_SQL, con)
            Sqlda.SelectCommand.CommandTimeout = 0
            Sqlda.Fill(My_DtSet, My_Table)

            For i = 0 To My_DtSet.Tables.Count - 1
                If My_DtSet.Tables(i).TableName = My_Table Then
                    My_BDS.DataSource = My_DtSet
                    My_BDS.DataMember = My_Table
                    My_DGV.DataSource = My_BDS
                    My_BNV.BindingSource = My_BDS
                    Exit For
                End If
            Next

            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub


#Region "TextBoxクリア"
    '//20180313 Add Start
    Private Shared Sub ClearTextBox(ByVal hParent As Control)
        'hParent内のすべてのコントロールを列挙する
        For Each cControl As Control In hParent.Controls
            '列挙したコントロールにコントロールが含まれている場合には再起呼び出しする
            If cControl.HasChildren Then
                ClearTextBox(cControl)
            End If

            'コントロールの型がTextboxBaseからの派生型の場合にはTextをクリアする
            If TypeOf cControl Is TextBoxBase Then
                cControl.Text = String.Empty
            End If
        Next cControl
    End Sub
    '//20180313 Add End
#End Region

#Region "入力データの必須チェック"
    Private Function checkInputValue() As Boolean
        Dim chkFlg As Boolean = True

        If Trim(txtタイトル.Text) = "" Then
            chkFlg = False
            txtタイトル.BackColor = Color.PeachPuff
        Else
            txtタイトル.BackColor = Color.White
        End If
        Return chkFlg
    End Function
#End Region

#Region "dgv制約"
    '//20180313 Add Start
    Private Sub Restrictdgv()
        '行ヘッダを非表示にする
        dgv登録.RowHeadersVisible = False
        '列の幅の変更禁止
        dgv登録.AllowUserToResizeColumns = False
        '行の幅の変更禁止
        dgv登録.AllowUserToResizeRows = False
        '並び替え禁止
        For Each c As DataGridViewColumn In dgv登録.Columns
            c.SortMode = DataGridViewColumnSortMode.NotSortable
        Next c
    End Sub
    '//20180313 Add End
#End Region

End Class
