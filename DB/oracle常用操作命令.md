#### oracle常用操作命令



##### 1、创建表空间

```sql
create tablespace tsp_emr
	datafile 'D:\app\zhangsansan\oradata\jhemr\emr.dbf'
	size 20M
	reuse
	autoextend on
	next 5M
	maxsize unlimited;
```



##### 2、创建用户并授权

```sql
create user emr
	identified by emr
	default tablespace tsp_emr
	temporary tablespace temp
	quota unlimited on tsp_emr;
grant connect,resource,dba to emr;
```



##### 3、导出数据库到桌面

```sql
exp emr/emr@192.168.2.214:1521/emr file=C:\Users\wangxiaoming\Desktop\emr.dmp
```



##### 4、导入dmp文件到数据库

```sql
imp medrec/medrec@localemrdb file=C:\Users\wangxiaoming\Desktop\medrec.dmp;
```



##### 5、登录数据库

```sql
sqlplus emr/emr 或 sqlplus /nolog
```



##### 6、级联删除用户所有数据

```
drop user emr cascade;
```



##### 7、删除表空间

```sql
drop tablespace 表空间名称 including contents and datafiles;  
```

