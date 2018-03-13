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

        cn = New System.Data.SqlClient.SqlConnection()

        'SQLServer認証
        strConnectSQL =
      "Data Source =TAKUYA;" &
      "Initial Catalog = BookRecord_DB;" &
      "User ID = sa;" &
      "Password = pass"

        'SQL文
        strSQL = "SELECT * FROM TD書籍 "

        SQLDA = New SqlClient.SqlDataAdapter(strSQL, strConnectSQL)

        'データセットに格納
        SQLDA.Fill(SQLDS, "TM書籍")

        Me.dgv登録.DataSource = SQLDS.Tables("TM書籍")

        'dgvの制約
        Restrictdgv()
    End Sub

    '//20180312 Add Start 
    Private Sub btn登録_Click(sender As Object, e As EventArgs) Handles btn登録.Click
        Dim registrationValue As Boolean = "False"
        registrationValue = checkInputValue()

        If registrationValue = True Then
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
            SQL += "'" & txtタイトル.Text & "',"
            SQL += "'" & userCD & "'"
            SQL += ")"
            'SQLコマンド設定
            SQLCMD.CommandText = SQL
            SQLCMD.Connection = cn
            SQLCMD.ExecuteNonQuery()
            SQLCMD.Dispose()
            cn.Close()
            cn.Dispose()
            MessageBox.Show("登録しました。")
        Else
            MessageBox.Show("タイトルを入力してください。")
        End If

    End Sub
    '//20180312 Add End


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
