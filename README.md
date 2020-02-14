# ORM package comparison

## Dapper

- 把執行 T-SQL 語句後的結果自動 map 成 type / dynamic
- 優點：輕量化，功能簡單，自由度較高（T-SQL)，支援 stored procedure
- 缺點：仍需要自行寫 SQL 及參數化查詢
- 時機：DB 結構複雜 / 存在不少數量 stored procedure 時

## PetaPoco

- 除查詢外支援用 object 自動產生 SQL
- 優點：提供基本的新增更新刪除 function，支援 T4 template 從 DB 產生 model (沒使用)
- 缺點：對資料庫關聯、cache 支援比較少
- 時機：DB 結構相對單純，不使用 stored procedure 時
