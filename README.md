# WinFormApp

教育用のFormアプリで、PostgreSQL接続とDI(Autofac)を利用できるようにした簡単なサンプルです。

見るところ

・appsettings.json: 設定ファイル。

・Program.cs: プログラムの最初の起動ファイル。その他、DB接続や、DIの設定も行なっています。  

SpringBootの@Controller、@Service、@Repositoryで行っていたことはできないので、このファイルで設定が必要です。

・ Form1、IUserService、UserService、IUserRepository、UserRepository  

DIとして使用したいものは、例のコードのようにコンストラクタインジェクションを使用してください。  

SpringBootのクラスのコンストラクタに@Autowiredを付けて利用するのと同じイメージです。  

・フォルダ構成は自習用にはこんな感じがいいかなという参考例です。  

SpringBootの自習でやっていたのに合わせちゃってください。


・以下のクラスは名前変更したほうがいいかも。

Dao → Repository  
Form → ViewModel  や　Modelなど




