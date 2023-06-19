# WinFormApp

ここはpublicで一般公開しています。(情報の取り扱い注意)

教育用のFormアプリのデータベース接続の簡単なサンプルです。

見るところ

・appsettings.json: 設定ファイルです。データベース接続情報やその他のデータも記入できます。

・Program.cs: 設定ファイルや、Autofacを利用したDIを使用できるようにしています。(Microsoft純正のDIはFormアプリでは使用できなかった)  
              PostgreSQLもDIで使用できるようにしています。
              
              その他DIとして使用したいForm、Service、Repositoryを追加してください。(SpringBootの@Controller、@Service、@Repositoryのようなものです)  

・画面やService、Repository:  
Form1  
IUserService, UserService  
IUserRepository, UserRepository  

DIとして使用したいものは、例のコードのようにコンストラクタインジェクションを使用してください。  
SpringBootのクラスのコンストラクタに@Autowiredを付けるのと同じです。

ServiceとRepositoryはインタフェースと実装クラスを紐づけています。(これはSpringBootでやってたのとは違うかも)  


・フォルダ構成は自習用にはこんな感じがいいかなという参考例です。  
SpringBootの自習でやっていたのに合わせちゃってください。


・以下のクラスは名前変更したほうがいいかも。

Dao → Repository  
Form → ViewModel  
※DB操作はRepositoryが一般的。  
Formより、フロントモデル(画面表示用のModel)ということで、ViewModelのほうがいいかな。



