# WinFormApp

ここはpublicで一般公開しています。(情報の取り扱い注意)

教育用のFormアプリのデータベース接続の簡単なサンプルです。動作検証はできていません。  
Webで編集したからコンパイルエラーしてたらすみません。

見るところは、Form1とappsettings.jsonを確認してください。

フォルダ構成は自習用にはこんな感じがいいかなという参考例です。SpringBootの自習でやっていたのに合わせちゃってください。



・以下のクラスは名前変更したほうがいいかも。

Dao → Repository  
Form → ViewModel  
※DB操作はRepositoryが一般的。  
Formより、フロント(画面表示用のModel) = ViewModelのほうがいいかな。




.gitignoteで除外設定ができてないので、パッケージとかビルド関係のデータを含んでしまってるかも。
