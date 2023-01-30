# ゲームの説明
Photonを使った2人対戦ゲーム  
Playerはゴミ箱とおっさんに分かれて戦い、
おっさんはポイ捨てしたゴミを地面に落とせば勝利。  
ゴミ箱はゴミを落とさないように拾い、一定数拾うと反撃可能になる。  
反撃時はゴミ袋を発射でき、おっさんに当ててHPを0にすれば勝利となる。  

# コードの解説
**使用アセット**
* UniRx
* UniTask
* Photon  

**ポイント**
* クラスの責任がはっきりするように意識した
* MVRPパターン
* 参照関係に気を付けた  

Playerの情報はそれぞれ[Model](https://github.com/KanbaraRyusei/MiniGames/tree/main/Assets/Scripts/BattleGame/Model)に持たせ、
[Presenter](https://github.com/KanbaraRyusei/MiniGames/tree/main/Assets/Scripts/BattleGame/Presenter)で繋いで
[View](https://github.com/KanbaraRyusei/MiniGames/tree/main/Assets/Scripts/BattleGame/View)に反映している。  
弾はネットワークオブジェクトにしている。  
クールタイムはUniTaskを雑に投げて実装している（今後修正できたら修正する予定）。  
個人制作だったがチーム制作を意識して設計している。  
そのため、[Manager](https://github.com/KanbaraRyusei/MiniGames/tree/main/Assets/Scripts/Manager)を1つ外のフォルダに置いていたり、
[基底クラス](https://github.com/KanbaraRyusei/MiniGames/tree/main/Assets/Scripts/BattleGame/PlayerController/Base)から派生させたり、  
簡単に違う挙動にできるようにしている。  
（[PlayerControllerBase.cs](https://github.com/KanbaraRyusei/MiniGames/blob/main/Assets/Scripts/BattleGame/PlayerController/Base/PlayerControllerBase.cs) のMoveDirectionとか）  

# 作ってみた感想
Photon勉強用に作ったゲーム。  
シュールなゲームにするつもりだったがシュールすぎて面白くなくなってしまった感があり、  
反省の多い作品になった。  
このREADMEを書いているときは作品制作から3か月くらい経っているので、  
もう少しどうにかなりそうなコードがちらほら見える。  
RPCを使っていなかったり、雑にUniTaskを投げていたり…。  
それでも設計にかなり気を配った（つもりだった）り、  
Photonが初めてで新鮮だったりで、楽しく制作していた記憶がある。  
ここまで読んでくれた人には是非Issueやプルリクでアドバイスなどをいただけるとめっちゃ喜びます。
