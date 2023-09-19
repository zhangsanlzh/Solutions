## mysql创建触发器

2019/10/10 

``` sql
drop TRIGGER if EXISTS AutoUpdateTypeText;

delimiter $$

CREATE TRIGGER AutoUpdateTypeText BEFORE UPDATE ON tblaccount for each ROW

BEGIN

	IF new.FType = 0 THEN

		set new.FTypeText='一般用户';

	ELSE

		SET new.FTypeText='特权用户';

	END IF;

END $$

delimiter ;
```


