## mysql����������

2019/10/10 

``` sql
drop TRIGGER if EXISTS AutoUpdateTypeText;

delimiter $$

CREATE TRIGGER AutoUpdateTypeText BEFORE UPDATE ON tblaccount for each ROW

BEGIN

	IF new.FType = 0 THEN

		set new.FTypeText='һ���û�';

	ELSE

		SET new.FTypeText='��Ȩ�û�';

	END IF;

END $$

delimiter ;
```


